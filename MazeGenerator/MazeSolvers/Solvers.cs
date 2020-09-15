using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace MazeGenerator.MazeSolvers
{
    public class Solvers
    {
        private int[,] Maze { get; }
        public bool Result { get; set; }
        public int FeatureCode { get; set; }
        public int Sleep { get; set; }

        private readonly List<Point> points; // Список посещённых точек в текущем "решении"
        private readonly List<Point> allPoints; // Список всех посещённых точек во время решения

        private readonly bool isBitmapUsed; // Используется ли запись в файл

        private int steps; // Количество сделанных шагов во время решения
        private readonly View view; // Ссылка на класс отрисовки

        private Point current; // Текущая точка
        private Point startpoint; // Стартовая тчока
        private Point finishpoint; // Конечная точка

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
            Maze = mazeArray;
            points = new List<Point>();
            allPoints = new List<Point>();
            this.startpoint = startpoint;
            this.finishpoint = finishpoint;
            this.view = view;
            Sleep = sleep;
            FeatureCode = featureCode;
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
            Maze[current.X, current.Y] = 2; // Если этого не сделать - будет багофича с пересечением старта один раз (WOW?)
        }

        /// <summary>
        /// Решение лабиринта методами левых и правых поворотов
        /// </summary>
        /// <param name="leftRotation">True, если левые повороты, иначе - правые повороты</param>
        /// <returns>Возвращает кол-во шагов, затраченное на решение лабиринта</returns>
        public int LeftRightRotateSolver(bool leftRotation)
        {
            SolversInit();
            int look = 1; // 0 - право, 1 - низ, 2 - лево, 3 - верх
            bool solutionFound = false;
            List<Point> pointsMove;
            int count;
            while (!solutionFound)
            {
                Thread.Sleep(Sleep);
                pointsMove = PointsFounders.PossiblePointsWithDirections(Maze, current);
                count = pointsMove.Count;
                if (count != 0)
                {
                    int selected;
                    if (leftRotation)
                        selected = PointsFounders.SelectedMoveLeft(ref look);
                    else
                        selected = PointsFounders.SelectedMoveRight(ref look);
                    GoToNewPoint(pointsMove[selected]);
                }
                else
                {
                    if (points.Count > 1)
                        PointRollback(ref look);
                    else
                        solutionFound = true;
                }
                if (current.X == finishpoint.X && current.Y == finishpoint.Y)
                    solutionFound = true;
            }
            if (current.X == finishpoint.X && current.Y == finishpoint.Y)
                Result = true;
            else
                Result = false;
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
            SolversInit();
            bool finFound = false;
            List<Point> pointsMove;
            int count;
            while (!finFound)
            {
                Thread.Sleep(Sleep);
                pointsMove = PointsFounders.PossiblePointsWithDirections(Maze, current);
                count = pointsMove.Count;
                if (count != 0)
                {
                    int selected = rand.Next(0, count);
                    GoToNewPoint(pointsMove[selected]);
                }
                else
                {
                    if (points.Count > 1)
                        PointRollback(ref placeholder);
                    else
                        finFound = true;
                }
                if (current.X == finishpoint.X && current.Y == finishpoint.Y)
                    finFound = true;
            }
            if (current.X == finishpoint.X && current.Y == finishpoint.Y)
                Result = true;
            else
                Result = false;
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
            if (!isBitmapUsed)
                allPoints.Add(clr);
            Maze[clr.X, clr.Y] = 2;
            view.DrawChange(clr, Color.SkyBlue);
            view.DrawChange(current, Color.SkyBlue);
            points.Add(selected);
            if (!isBitmapUsed)
                allPoints.Add(selected);
            current = points.Last();
            view.DrawChange(current, Color.Red);
            Maze[selected.X, selected.Y] = 2;
            steps++;
        }

        /// <summary>
        /// Метод возврата, если зашли в тупик
        /// </summary>
        /// <param name="look">Обновляемое значение взгляда точки-решателя лабиринта</param>
        public void PointRollback(ref int look)
        {
            view.DrawChange(points.Last(), Color.Coral);
            Point clr = ClearPoint(current, points[points.Count - 2]);
            look = LookUpdate(current, clr);
            view.DrawChange(clr, Color.Coral);
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
        private int LookUpdate(Point x1, Point x2)
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
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    if (Maze[i, j] != 0)
                        Maze[i, j] = 1;
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
                    && (allPoints[i].X != finishpoint.X || allPoints[i].Y != finishpoint.Y)) { 
                    if (FeatureCode == 0)
                        view.DrawChange(allPoints[i], Color.White);
                    else
                        view.DrawChange(allPoints[i], FeatureCode);
                }
                    
            }
        }
    }
}
