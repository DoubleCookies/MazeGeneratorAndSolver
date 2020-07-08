using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading;

namespace MazeGenerator.MazeGenerators
{
    public class Generators
    {
        PointsFounders founders; // Ссылка на класс, позволяющий искать точки
        View view; // Ссылка на класс отрисовки

        int[,] mazeArray; // Массив, представляющий лабиринт
        List<Point> points; // Список точек при построении лабиринта
        Random random;
        Point startpoint; // Начальная точка
        Point finishpoint; // Конечная точка
        Point current; // Текущая точка
        int lastX; // Точка последней посещённой вертикали для повышения эффективности алгоритма Hunt-And-Kill

        public List<Point> BlackPoints { get; set; }

        int featureCode; // Код особенности отрисовки
        int sleep; // Время остановки потока при отрисовке
        public int Sleep
        {
            set { sleep = value; }
        }

        bool ignored; // Были ли проигнорированные точки в алгоритме Hunt-And-Kill (если к ним не было прямого доступа)
        int ignoredCount; // Счётчик циклов игнорирования

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
        public Generators(int[,] mazeArray, Point startpoint, Point finishpoint, PointsFounders founders, View view, int featurecode, int sleep)
        {
            this.mazeArray = mazeArray;
            random = new Random();
            this.startpoint = startpoint;
            this.finishpoint = finishpoint;
            this.founders = founders;
            points = new List<Point>();
            BlackPoints = new List<Point>();
            this.sleep = sleep;
            this.view = view;
            featureCode = featurecode;
        }


        /// <summary>
        /// Заполнение лабиринта
        /// </summary>
        /// <param name="rand">Использовать ли случайные пропуски</param>
        public void mazeFill(bool rand, double prob)
        {
            for (int i = 0; i < mazeArray.GetLength(0); i++)
            {
                for (int j = 0; j < mazeArray.GetLength(1); j++)
                {
                    if (i % 2 != 0 && j % 2 != 0) // Пропускаем чётные элементы
                    {
                        if (!rand)
                            mazeArray[i, j] = 1; // 1 - клетки, по которым можно ходить
                        else
                        {
                            // Есть вероятность создания пустой клетки
                            if (random.NextDouble() < prob || (i == startpoint.X && j == startpoint.Y) || (i == finishpoint.X && j == finishpoint.Y))
                                mazeArray[i, j] = 1;
                            else
                                BlackPoints.Add(new Point(i, j));
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Генерация лабиринта BackTracking'ом
        /// </summary>
        /// <param name="fromStart">Начинать ли генерацию со старта</param>
        
        /// <param name="whiteProb">Вероятность убрать стену</param>
        public void BackTrackMazeGenerate(bool fromStart, double blackProb, double whiteProb)
        {
            if (blackProb > 0) {
                view.DrawBlackPoints(BlackPoints);
                BlackClear();
            }
            // 2 - уже посещенная вершина
            current = finishpoint;
            if (fromStart)
                current = startpoint;
            mazeArray[current.X, current.Y] = 2;
            points.Add(current);
            // Цикл идёт, пока кол-во точек, где можно куда-то пойти, не равно 0
            while (points.Count != 0)
            {
                Thread.Sleep(sleep);
                List<Point> possPoints = founders.possiblePoints(mazeArray, current.X, current.Y, 2, 1);
                int count = possPoints.Count;
                if (count != 0)
                {// Если есть куда идти
                    int selected = random.Next(0, count);
                    GoToNewPoint(possPoints[selected]);
                }
                else
                {// Иначе, если некуда идти, надо вернуться к предыдущей точке
                    PointRollback();
                }
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
        public void HuntAndKillMazeGenerate(bool fromStart, double blackProb, double whiteProb)
        {
            if (blackProb > 0)
            {
                view.DrawBlackPoints(BlackPoints);
                BlackClear();
            }
            ignored = false;
            ignoredCount = 0;
            current = finishpoint;
            bool fSCopy = fromStart;
            bool isHereToGo = true;
            if (fromStart)
                current = startpoint;
            if (fromStart)
                lastX = 1;
            else
                lastX = mazeArray.GetLength(0) - 2;
            mazeArray[current.X, current.Y] = 2;
            // Цикл идёт, пока кол-во точек, где можно куда-то пойти, не равно 0, а также
            // (если были проигнорированы некоторые точки) количество проигнорированных циклов < 3
            while (isHereToGo)
            {
                Thread.Sleep(sleep);
                List<Point> possPoints = founders.possiblePoints(mazeArray, current.X, current.Y, 2, 1);
                int count = possPoints.Count;
                if (count != 0)
                {// Если есть куда идти
                    int selected = random.Next(0, count);
                    GoToNewPointHunt(possPoints[selected]);
                }
                else
                {// Иначе, если некуда идти, надо найти новую точку для продолжения отрисовки
                    isHereToGo = FindNewPoint(fSCopy);
                }
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
                        && (i != 0 && j != 0 && i != mazeArray.GetLength(0) - 1 && j != mazeArray.GetLength(1) - 1))
                    {
                        if (mazeArray[i, j] == 0)
                        {
                            if (random.NextDouble() > whiteProb)
                            {
                                List<Point> poss = founders.possiblePoints(mazeArray, i, j, 1, 2);
                                if (poss.Count > 1)
                                {// Делаем тоннель так, чтобы можно было свзяать две точки
                                    mazeArray[i, j] = 1;
                                    if(featureCode == 0)
                                        view.DrawChange(new Point(i, j), Color.White);
                                    else
                                        view.DrawChange(new Point(i, j), featureCode);
                                }
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
            Point clr = ClearPoint(current, newPoint);
            mazeArray[clr.X, clr.Y] = 2;
            if (featureCode == 0)
            {
                view.DrawChange(clr, Color.White);
                view.DrawChange(current, Color.FromArgb(255, 170, 102));
            }
            else
            {
                view.DrawChange(clr, featureCode);
                view.DrawChange(current, featureCode);
            }
            points.Add(newPoint);
            current = points.Last();
            view.DrawChange(current, Color.Red);
            mazeArray[newPoint.X, newPoint.Y] = 2;
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
                current = points.Last();
                view.DrawChange(current, Color.Red);
            }
        }

        /// <summary>
        /// Переход в новую точку в алгоритме Hunt-And-Kill
        /// </summary>
        /// <param name="newPoint">Новая точка для перехода</param>
        private void GoToNewPointHunt(Point newPoint)
        {
            Point clr = ClearPoint(current, newPoint);
            mazeArray[clr.X, clr.Y] = 2;
            if (featureCode == 0)
            {
                view.DrawChange(clr, Color.White);
                view.DrawChange(current, Color.White);
            }
            else
            {
                view.DrawChange(clr, featureCode);
                view.DrawChange(current, featureCode);
            }
            current.X = newPoint.X;
            current.Y = newPoint.Y;
            view.DrawChange(current, Color.Red);
            mazeArray[newPoint.X, newPoint.Y] = 2;
        }

        /// <summary>
        /// Метод поиска новой точки для соединения с уже проложенным маршрутом
        /// </summary>
        /// <param name="fromStart">Определяет, происходит ли поиск новой точки сверху или снизу лабиринта</param>
        /// <returns>Возвращает true, если новая точка найдена и к ней проложен путь</returns>
        public bool FindNewPoint(bool fromStart)
        {
            int i;
            int j;
            if (fromStart)
            {
                for (i = lastX; i < mazeArray.GetLength(0); i = i + 2)
                {
                    lastX = i;
                    for (j = 1; j < mazeArray.GetLength(1); j = j + 2)
                    {
                        if (mazeArray[i, j] == 1)
                        {
                            if (SelectNewPointOperations(i, j))
                                return true;
                            else
                                ignored = true;
                        }
                        if (i == mazeArray.GetLength(0) - 2 && j == mazeArray.GetLength(1) - 2 && ignored && ignoredCount < 3)
                        { lastX = mazeArray.GetLength(0) - 2; j = mazeArray.GetLength(1) - 2; ignored = true; SecondCycle(fromStart); }
                    }
                }
            }
            else
            {
                for (i = lastX; i > 0; i = i - 2)
                {
                    for (j = mazeArray.GetLength(1) - 2; j > 0; j = j - 2)
                    {
                        if (mazeArray[i,j] == 1)
                        {
                            lastX = i;
                            if (SelectNewPointOperations(i, j))
                                return true;
                            else
                                ignored = true;
                        }
                        if (i == 1 && j == 1 && ignored && ignoredCount < 3)
                        { lastX = 1; j = 1; SecondCycle(fromStart); }
                    }
                }
            }
            if (ignored && ignoredCount < 3)
            { lastX = 1; ignored = true; SecondCycle(fromStart); }
            if (featureCode == 0)
                view.DrawChange(current, Color.White);
            else
                view.DrawChange(current, featureCode);
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
            fromStart = !fromStart;
            ignored = false;
            ignoredCount++;
            bool isHereToGo = true;
            while (isHereToGo)
            {
                Thread.Sleep(sleep);
                List<Point> possPoints = founders.possiblePoints(mazeArray, current.X, current.Y, 2, 1); //possiblePoints(current.X, current.Y, 2,1); //поиск возможных для движения точек
                int count = possPoints.Count;
                if (count != 0)
                {// Если есть куда идти
                    int selected = random.Next(0, count);
                    GoToNewPointHunt(possPoints[selected]);
                }
                else
                {// Иначе, если некуда идти, надо найти новую точку для продолжения отрисовки
                    isHereToGo = FindNewPoint(fromStart);
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
                    view.DrawChange(current, Color.White);
                else
                    view.DrawChange(current, featureCode);
                current.X = i;
                current.Y = j;
                List<Point> possibleToConnect = founders.possiblePoints(mazeArray, current.X, current.Y, 2, 2);
                if (possibleToConnect.Count == 0)
                    return false;
                int selected = random.Next(0, possibleToConnect.Count);
                Point clr = ClearPoint(current, possibleToConnect[selected]);
                if (featureCode == 0)
                {
                    view.DrawChange(clr, Color.White);
                    view.DrawChange(current, Color.White);
                }
                else
                {
                    view.DrawChange(clr, featureCode);
                    view.DrawChange(current, featureCode);
                }
                mazeArray[clr.X, clr.Y] = 2;
                mazeArray[current.X, current.Y] = 2;
                return true;
        }

        /// <summary>
        /// Поиск точки, находящейся между двумя точками
        /// </summary>
        /// <param name="x1">Первая точка</param>
        /// <param name="x2">Вторая точка</param>
        /// <returns>Возвращает точку, которая находится между двумя точками</returns>
        private Point ClearPoint(Point x1, Point x2)
        {
            return new Point((x1.X + x2.X) / 2, (x1.Y + x2.Y) / 2);
        }


        /// <summary>
        /// Очистка "чёрных" точек
        /// </summary>
        public void BlackClear()
        {
            BlackPoints.Clear();
        }
    }
}
