using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;

namespace MazeGenerator.MazeSolvers
{
    public class Solvers
    {
        int[,] mazeArray; // Массив, представляющий лабиринт
        public int[,] GetMaze
        {
            get { return mazeArray; }
        }

        public bool Result { get => result; set => result = value; }
        public int FeatureCode { get => featureCode; set => featureCode = value; }
        public int Sleep { get => sleep; set => sleep = value; }

        List<Point> points; // Список посещённых точек в текущем "решении"
        List<Point> allPoints; // Список всех посещённых точек во время решения

        bool isBitmapUsed; // Используется ли запись в файл

        int steps; // Количество сделанных шагов во время решения
        int featureCode; // Код особой отрисовки
        View view; // Ссылка на класс отрисовки
        PointsFounders founders; // Ссылка на класс, позволяющий искать точки

        Point current; // Текущая точка
        Point startpoint; // Стартовая тчока
        Point finishpoint; // Конечная точка

        int sleep; //Тайм-аут для отрисовки
        bool result; //Результат поиска решения лабиринта

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mazeArray">Массив с содержимым лабиринта</param>
        /// <param name="startpoint">Стартовая точка</param>
        /// <param name="finishpoint">Конечная точка</param>
        /// <param name="view">Объект для отрисовки</param>
        /// <param name="featureCode">Код фичи отрисовки</param>
        /// <param name="sleep">Таймаут отрисовки</param>
        public Solvers(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featureCode, int sleep, bool isBitmapUsed)
        {
            this.isBitmapUsed = isBitmapUsed;
            this.mazeArray = mazeArray;
            points = new List<Point>();
            allPoints = new List<Point>();
            this.startpoint = startpoint;
            this.finishpoint = finishpoint;
            this.view = view;
            this.sleep = sleep;
            this.featureCode = featureCode;
            founders = new PointsFounders();
        }

        /// <summary>
        /// Метод очистки лабиринта после его прохождения
        /// </summary>
        public void Clr()
        {
            MazeRouteClear();
            MazeClear();
        }
        /// <summary>
        /// Инициализация решателей лабиринта
        /// </summary>
        public void SolversInit()
        {
            steps = 1;
            MazeRouteClear();
            MazeClear();
            current = startpoint;
            points.Clear();
            allPoints.Clear();
            points.Add(current);
            allPoints.Add(current);
            mazeArray[current.X, current.Y] = 2; // Если этого не сделать - будет багофича с пересечением старта один раз (WOW?)
        }


        /// <summary>
        /// Решение лабиринта методами левых и правых поворотов
        /// </summary>
        /// <param name="LR">True, если левые повороты, иначе - правые повороты</param>
        /// <returns>Возвращает кол-во шагов, затраченное на решение лабиринта</returns>
        public int LeftRightRotateSolver(bool LR)
        {
            Random rand = new Random();
            SolversInit();
            int look = 1; // 0 - право, 1 - низ, 2 - лево, 3 - верх
            int selected = -1; // Выбранный элемент из массива возможных ходов
            bool finFound = false;
            List<Point> pointsMove;
            int count;
            // Пока не найден путь
            while (!finFound)
            {
                Thread.Sleep(sleep);
                pointsMove = founders.possiblePoints2(mazeArray, current);
                count = pointsMove.Count;
                if (count != 0)
                {
                    if (LR)
                        selected = founders.selectedMoveLeft(ref look);
                    else
                        selected = founders.selectedMoveRight(ref look);
                    GoToNewPoint(pointsMove[selected]);
                }
                else
                {// Если некуда идти, то возвращаемся назад
                    if (points.Count > 1)
                        PointRollback(ref look);
                    else // Если кол-во точек в пути < 1 - мы пришли к старту
                        finFound = true;
                }
                if (current.X == finishpoint.X && current.Y == finishpoint.Y) // Если мы в финишной точке, то завершаем цикл
                    finFound = true;
            }
            // Если мы в финишной точке - решение найдено
            if (current.X == finishpoint.X && current.Y == finishpoint.Y)
                result = true;
            else
                result = false;
            return steps;
        }

        /// <summary>
        /// Метод случайных поворотов - поворачивает куда хочет
        /// </summary>
        /// <returns>Возвращает кол-во шагов, затраченное на решение лабиринта</returns>
        public int RandomSolver()
        {
            int placeholder = 1;
            Random rand = new Random();
            int selected = -1; // Выбранный элемент из массива возможных ходов
            SolversInit();
            bool finFound = false;
            List<Point> pointsMove;
            int count;
            while (!finFound)
            {
                Thread.Sleep(sleep);
                pointsMove = founders.possiblePoints2(mazeArray, current);
                count = pointsMove.Count;
                if (count != 0)
                {
                    selected = rand.Next(0, count);
                    GoToNewPoint(pointsMove[selected]);
                }
                else
                {
                    if (points.Count > 1)
                        PointRollback(ref placeholder);
                    else // Если кол-во точек в пути < 1 - мы пришли к старту
                        finFound = true;
                }
                if (current.X == finishpoint.X && current.Y == finishpoint.Y) //Если мы в финишной точке, то завершаем цикл
                    finFound = true;
            }
            if (current.X == finishpoint.X && current.Y == finishpoint.Y)
                result = true;
            else
                result = false;
            return steps;
        }

        /// <summary>
        /// Метод перехода в новую точку лабиринта (плюс вызов отрисовки)
        /// </summary>
        /// <param name="selected">Выбранная точка</param>
        /// <param name="pointsMove">Список возможных для посещения точек</param>
        public void GoToNewPoint(Point selected)
        {
            Point clr = ClearPoint(current, selected);
            if(!isBitmapUsed)
                allPoints.Add(clr);
            mazeArray[clr.X, clr.Y] = 2;
            if (featureCode == 0)
            {
                view.DrawChange(clr, Color.SkyBlue);
                view.DrawChange(current, Color.SkyBlue);
            }
            else
            {
                view.DrawChange(clr, featureCode);
                view.DrawChange(current, featureCode);
            }
            points.Add(selected);
            if(!isBitmapUsed)
                allPoints.Add(selected);
            current = points.Last();
            view.DrawChange(current, Color.Red);
            mazeArray[selected.X, selected.Y] = 2;
            steps++;
        }

        /// <summary>
        /// Метод возврата, если зашли в тупик
        /// </summary>
        /// <param name="look">Обновляемое значение взгляда точки-решателя лабиринта</param>
        public void PointRollback(ref int look)
        {
            if (featureCode == 0)
            {
                view.DrawChange(points.Last(), Color.Coral);
                Point clr = ClearPoint(current, points[points.Count - 2]);
                look = lookUpdate(current, clr);
                view.DrawChange(clr, Color.Coral);
            }
            else
            {
                view.DrawChange(points.Last(), featureCode);
                Point clr = ClearPoint(current, points[points.Count - 2]);
                look = lookUpdate(current, clr);
                view.DrawChange(clr, featureCode);
            }
            points.RemoveAt(points.Count - 1);
            if (points.Count != 0)
            {
                current = points.Last();
                view.DrawChange(current, Color.Red);
            }
            steps++;
        }

        /// <summary>
        /// Обновление взгляда точки после возврата из "тупика"
        /// </summary>
        /// <param name="x1">Текущая точка</param>
        /// <param name="x2">Промежуточная точка перед точкой возврата</param>
        /// <returns>Возвращает направление "взгляда" решателя</returns>
        private int lookUpdate(Point x1, Point x2)
        {
            // 0 - право, 1 - низ, 2 - лево, 3 - верх
            if (x1.X == x2.X)
            {
                if (x1.Y > x2.Y)
                    return 3;
                else
                    return 1;
            }
            else
            {
                if (x1.X > x2.X)
                    return 2;
                else
                    return 0;
            }
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
        /// Очистка лабиринта
        /// </summary>
        private void MazeClear()
        {
            for (int i = 0; i < mazeArray.GetLength(0); i++)
            {
                for (int j = 0; j < mazeArray.GetLength(1); j++)
                {
                    if (mazeArray[i, j] != 0)
                        mazeArray[i, j] = 1;
                }
            }
        }

        /// <summary>
        /// Очистка пути после поиска решения
        /// </summary>
        private void MazeRouteClear()
        {
            for (int i = 0; i < allPoints.Count; i++)
            {
                if ((allPoints[i].X != startpoint.X || allPoints[i].Y != startpoint.Y)
                    && (allPoints[i].X != finishpoint.X || allPoints[i].Y != finishpoint.Y))
                    view.DrawChange(allPoints[i], Color.White);
            }
        }
    }
}
