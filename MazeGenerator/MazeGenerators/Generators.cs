using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace MazeGenerator.MazeGenerators
{
    //TODO: абстрактный класс
    public class Generators
    {
        private readonly View view;
        private readonly int[,] mazeArray;
        private readonly List<Point> points;
        private readonly List<Point> blackPoints;
        private readonly Random random;
        private Point startpoint;
        private Point finishpoint;
        private Point currentPoint;
        private int lastX; // Точка последней посещённой вертикали для повышения эффективности алгоритма Hunt-And-Kill
        private readonly int featureCode; // Код особенности отрисовки
        private readonly int sleep; // Время остановки потока при отрисовке
        private bool ignored; // Были ли проигнорированные точки в алгоритме Hunt-And-Kill (если к ним не было прямого доступа)
        private int ignoredCount; // Счётчик циклов игнорирования

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mazeArray">Представление лабиринта в виде двумерного массива</param>
        /// <param name="startpoint">Стартовая точка</param>
        /// <param name="finishpoint">Конечная точка</param>
        /// <param name="founders">Ссылка на вспомогательный класс нахождения точек</param>
        /// <param name="view">Ссылка на объект отрисовки</param>
        /// <param name="featurecode">Код особенности отрисовки</param>
        /// <param name="sleep">Время остановки потока при отрисовке</param>
        public Generators(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featurecode, int sleep, Random random)
        {
            this.mazeArray = mazeArray;
            this.random = random;
            this.startpoint = startpoint;
            this.finishpoint = finishpoint;
            points = new List<Point>();
            blackPoints = new List<Point>();
            this.sleep = sleep;
            this.view = view;
            featureCode = featurecode;
        }


        /// <summary>
        /// Метод для заполнения массива исходными данными
        /// </summary>
        /// <param name="noPointProbability">Вероятность того, что точка будет проигнорирована</param>
        public void FillMazeArray(double noPointProbability)
        {
            for (int i = 1; i < mazeArray.GetLength(0); i+=2)
            {
                for (int j = 1; j < mazeArray.GetLength(1); j+=2)
                {
                    if (random.NextDouble() < noPointProbability 
                        && !(i == startpoint.X && j == startpoint.Y) && !(i == finishpoint.X && j == finishpoint.Y))
                        blackPoints.Add(new Point(i, j));
                    else
                        mazeArray[i, j] = (int)PointStatus.canVisit;
                }
            }
        }


        /// <summary>
        /// Генерация лабиринта BackTracking'ом
        /// </summary>
        /// <param name="fromStart">Начинать ли генерацию со старта</param>

        /// <param name="whiteProb">Вероятность убрать стену</param>
        public void BackTrackMazeGenerate(bool fromStart, double whiteProb)
        {
            view.DrawBlackPoints(blackPoints);
            ClearBlackPointsList();

            currentPoint = fromStart ? startpoint : finishpoint;
            mazeArray[currentPoint.X, currentPoint.Y] = (int)PointStatus.alreadyVisited;
            points.Add(currentPoint);
            while (points.Count != 0)
            {
                if (sleep != 0)
                    Thread.Sleep(sleep);
                List<Point> possiblePoints = PointOperations.PossiblePoints(mazeArray, currentPoint.X, currentPoint.Y, 2, (int)PointStatus.canVisit);
                int pointsToVisit = possiblePoints.Count;
                if (pointsToVisit != 0)
                    GoToNewPoint(possiblePoints[random.Next(0, pointsToVisit)]);
                else
                    PointRollback();
            }
            if (whiteProb > 0)
                WhiteGen(whiteProb);
        }

        /// <summary>
        /// Метод генерации лабиринта Hunt-And-Kill
        /// </summary>
        /// <param name="fromStart">Начинать ли генерацию со старта</param>
        /// <param name="whiteSpaces">Убирать ли часть стен</param>
        /// <param name="whiteProb">Вероятность убрать стену</param>
        public void HuntAndKillMazeGenerate(bool fromStart, double whiteProb)
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
                List<Point> possPoints = PointOperations.PossiblePoints(mazeArray, currentPoint.X, currentPoint.Y, 2, (int)PointStatus.canVisit);
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
        /// Метод "удаления" части стен в лабиринте.
        /// Удаление происходит только в том случае, если после удаления стены можно будет пройти дальше.
        /// </summary>
        private void WhiteGen(double whiteProb)
        {
            for (int i = 0; i < mazeArray.GetLength(0); i++)
            {
                for (int j = 0; j < mazeArray.GetLength(1); j++)
                {
                    if (((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))
                        && i != 0 && j != 0 && i != mazeArray.GetLength(0) - 1 && j != mazeArray.GetLength(1) - 1)
                    {
                        if (mazeArray[i, j] == 0 && random.NextDouble() < whiteProb)
                        {
                            List<Point> pointToConnect = PointOperations.PossiblePoints(mazeArray, i, j, 1, (int)PointStatus.alreadyVisited);
                            if (pointToConnect.Count > 1)
                            {// Делаем тоннель так, чтобы можно было связать две точки
                                mazeArray[i, j] = 1;
                                if (featureCode == 0)
                                    view.DrawChange(new Point(i, j), Color.White);
                                else
                                    view.DrawChange(new Point(i, j), featureCode);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Переход в новую точку при построении лабиринта
        /// </summary>
        /// <param name="selected">Выбранная точка</param>
        /// <param name="pointsMove">Список точек</param>
        private void GoToNewPoint(Point newPoint)
        {
            Point clr = FoundPointBetweenTwoPoints(currentPoint, newPoint);
            mazeArray[clr.X, clr.Y] = (int)PointStatus.alreadyVisited;
            if (featureCode == 0)
                view.DrawChange(clr, Color.White);
            else
                view.DrawChange(clr, featureCode);
            view.DrawChange(currentPoint, Color.FromArgb(255, 170, 102));
            points.Add(newPoint);
            currentPoint = points.Last();
            view.DrawChange(currentPoint, Color.Red);
            mazeArray[newPoint.X, newPoint.Y] = (int)PointStatus.alreadyVisited;
        }

        /// <summary>
        /// Возвращение в уже посещённые точки
        /// </summary>
        private void PointRollback()
        {
            if (featureCode == 0)
                view.DrawChange(points.Last(), Color.White);
            else
                view.DrawChange(points.Last(), featureCode);
            points.RemoveAt(points.Count - 1);
            if (points.Count != 0)
            {
                currentPoint = points.Last();
                view.DrawChange(currentPoint, Color.Red);
            }
        }

        /// <summary>
        /// Переход в новую точку в алгоритме Hunt-And-Kill
        /// </summary>
        /// <param name="newPoint">Новая точка для перехода</param>
        private void GoToNewPointHunt(Point newPoint)
        {
            Point clr = FoundPointBetweenTwoPoints(currentPoint, newPoint);
            mazeArray[clr.X, clr.Y] = 2;
            if (featureCode == 0)
            {
                view.DrawChange(clr, Color.White);
                view.DrawChange(currentPoint, Color.White);
            }
            else
            {
                view.DrawChange(clr, featureCode);
                view.DrawChange(currentPoint, featureCode);
            }
            currentPoint.X = newPoint.X;
            currentPoint.Y = newPoint.Y;
            view.DrawChange(currentPoint, Color.Red);
            mazeArray[newPoint.X, newPoint.Y] = 2;
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
            if (featureCode == 0)
                view.DrawChange(currentPoint, Color.White);
            else
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
                List<Point> possPoints = PointOperations.PossiblePoints(mazeArray, currentPoint.X, currentPoint.Y, 2, (int)PointStatus.canVisit);
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
            if (featureCode == 0)
                view.DrawChange(currentPoint, Color.White);
            else
                view.DrawChange(currentPoint, featureCode);
            currentPoint.X = i;
            currentPoint.Y = j;
            List<Point> possibleToConnect = PointOperations.PossiblePoints(mazeArray, currentPoint.X, currentPoint.Y, 2, (int)PointStatus.alreadyVisited);
            if (possibleToConnect.Count == 0)
                return false;
            int selected = random.Next(0, possibleToConnect.Count);
            Point clr = FoundPointBetweenTwoPoints(currentPoint, possibleToConnect[selected]);
            if (featureCode == 0)
            {
                view.DrawChange(clr, Color.White);
                view.DrawChange(currentPoint, Color.White);
            }
            else
            {
                view.DrawChange(clr, featureCode);
                view.DrawChange(currentPoint, featureCode);
            }
            mazeArray[clr.X, clr.Y] = 2;
            mazeArray[currentPoint.X, currentPoint.Y] = 2;
            return true;
        }

        /// <summary>
        /// Поиск точки, находящейся между двумя точками
        /// </summary>
        /// <param name="x1">Первая точка</param>
        /// <param name="x2">Вторая точка</param>
        /// <returns>Возвращает точку, которая находится между двумя точками</returns>
        private Point FoundPointBetweenTwoPoints(Point x1, Point x2)
        {
            return new Point((x1.X + x2.X) / 2, (x1.Y + x2.Y) / 2);
        }


        /// <summary>
        /// Очистка "чёрных" точек
        /// </summary>
        public void ClearBlackPointsList()
        {
            blackPoints.Clear();
        }
    }
}
