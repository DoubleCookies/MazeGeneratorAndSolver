using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace MazeGenerator.MazeGenerators
{
    public class HuntAndKillGenerator : AbstractGenerator
    {
        bool ignored;
        int ignoredCount;
        public int lastX; // Точка последней посещённой вертикали для повышения эффективности алгоритма Hunt-And-Kill

        public HuntAndKillGenerator(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featurecode, int sleep, Random random) 
            : base(mazeArray, startpoint, finishpoint, view, featurecode, sleep, random) {
            ignored = false;
            ignoredCount = 0;
        }

        public override void Generate(bool fromStart, double whiteProb)
        {
            view.DrawBlackPoints(blackPoints);
            ClearBlackPointsList();

            ignored = false;
            ignoredCount = 0;
            bool hasPointsToVisit = true;
            currentPoint = fromStart ? startpoint : finishpoint;
            lastX = fromStart ? 1 : mazeArray.GetLength(0) - 2;
            mazeArray[currentPoint.X, currentPoint.Y] = 2;
            // Цикл идёт, пока кол-во точек, где можно куда-то пойти, не равно 0, а также
            // (если были проигнорированы некоторые точки) количество проигнорированных циклов < 3
            while (hasPointsToVisit)
            {
                if (sleep != 0)
                    Thread.Sleep(sleep);
                List<Point> possPoints = PointOperations.GetConnectedNotVisitedPoints(mazeArray, currentPoint.X, currentPoint.Y, 2);
                int pointsToVisit = possPoints.Count;
                if (pointsToVisit != 0)
                    GoToNewPointHunt(possPoints[random.Next(0, pointsToVisit)]);
                else
                    hasPointsToVisit = FindNewPoint(fromStart);
            }
            if (whiteProb > 0)
                WhiteGen(whiteProb);
        }

        /// <summary>
        /// Переход в новую точку в алгоритме Hunt-And-Kill
        /// </summary>
        /// <param name="newPoint">Новая точка для перехода</param>
        private void GoToNewPointHunt(Point newPoint)
        {
            Point clr = FoundPointBetweenTwoPoints(currentPoint, newPoint);
            mazeArray[clr.X, clr.Y] = (int)PointStatus.alreadyVisited;
            view.DrawChange(clr, featureCode);
            view.DrawChange(currentPoint, featureCode);

            currentPoint.X = newPoint.X;
            currentPoint.Y = newPoint.Y;
            view.DrawChange(currentPoint, Color.Red);
            mazeArray[newPoint.X, newPoint.Y] = (int)PointStatus.alreadyVisited;
        }

        /// <summary>
        /// Метод поиска новой точки для соединения с уже проложенным маршрутом
        /// </summary>
        /// <param name="fromStart">Определяет, происходит ли поиск новой точки сверху или снизу лабиринта</param>
        /// <returns>Возвращает true, если новая точка найдена и к ней проложен путь</returns>
        public bool FindNewPoint(bool fromStart)
        {
            int i, j;
            if (fromStart)
            {
                for (i = lastX; i < mazeArray.GetLength(0); i += 2)
                {
                    lastX = i;
                    for (j = 1; j < mazeArray.GetLength(1); j += 2)
                    {
                        if (mazeArray[i, j] == 1)
                        {
                            if (SelectNewPointOperations(i, j))
                                return true;
                            else
                                ignored = true;
                        }
                        if (i == mazeArray.GetLength(0) - 2 && j == mazeArray.GetLength(1) - 2 && ignored && ignoredCount < 3)
                        {
                            lastX = mazeArray.GetLength(0) - 2;
                            j = mazeArray.GetLength(1) - 2;
                            ignored = true; SecondCycle(fromStart);
                        }
                    }
                }
            }
            else
            {
                for (i = lastX; i > 0; i -= 2)
                {
                    for (j = mazeArray.GetLength(1) - 2; j > 0; j -= 2)
                    {
                        if (mazeArray[i, j] == 1)
                        {
                            lastX = i;
                            if (SelectNewPointOperations(i, j))
                                return true;
                            else
                                ignored = true;
                        }
                        if (i == 1 && j == 1 && ignored && ignoredCount < 3)
                        {
                            lastX = 1;
                            j = 1;
                            SecondCycle(fromStart);
                        }
                    }
                }
            }
            if (ignored && ignoredCount < 3)
            { lastX = 1; ignored = true; SecondCycle(fromStart); }
            view.DrawChange(currentPoint, featureCode);
            return false;
        }

        /// <summary>
        /// Дополнительные циклы в алгоритме Hunt-And-Kill (при наличии непосещённых точек мы просматриваем их). 
        /// Дополнительно происходит смена начала поиска точки, которую можно добавить к уже имеющемуся маршруту.
        /// Если лабиринт генерируется со старта - сначала проход идёт справа налево, затем - слева направо.
        /// Если со старта - наоборот
        /// </summary>
        /// <param name="fromStart">Производится ли создание лабиринта со старта</param>
        private void SecondCycle(bool fromStart)
        {
            ignored = false;
            ignoredCount++;
            bool hasPointToVisit = true;
            while (hasPointToVisit)
            {
                Thread.Sleep(sleep);
                List<Point> possPoints = PointOperations.GetConnectedNotVisitedPoints(mazeArray, currentPoint.X, currentPoint.Y, 2);
                int possiblePointsCount = possPoints.Count;
                if (possiblePointsCount != 0)
                {// Если есть куда идти
                    int selected = random.Next(0, possiblePointsCount);
                    GoToNewPointHunt(possPoints[selected]);
                }
                else
                {// Иначе, если некуда идти, надо найти новую точку для продолжения отрисовки
                    hasPointToVisit = FindNewPoint(!fromStart);
                }
            }
        }

        /// <summary>
        /// Метод для отрисовки, сдвига и создания туннеля для новой точки
        /// </summary>
        /// <param name="i">Координата X точки</param>
        /// <param name="j">Координата Y точки</param>
        private bool SelectNewPointOperations(int i, int j)
        {
            view.DrawChange(currentPoint, featureCode);
            currentPoint.X = i;
            currentPoint.Y = j;
            List<Point> possibleToConnect = PointOperations.GetConnectedVisitedPoints(mazeArray, currentPoint.X, currentPoint.Y, 2);
            if (possibleToConnect.Count == 0)
                return false;
            int selected = random.Next(0, possibleToConnect.Count);
            Point clr = FoundPointBetweenTwoPoints(currentPoint, possibleToConnect[selected]);
            view.DrawChange(clr, featureCode);
            view.DrawChange(currentPoint, featureCode);
            mazeArray[clr.X, clr.Y] = (int)PointStatus.alreadyVisited;
            mazeArray[currentPoint.X, currentPoint.Y] = (int)PointStatus.alreadyVisited;
            return true;
        }
    }
}
