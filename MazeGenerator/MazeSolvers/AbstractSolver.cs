using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MazeGenerator.MazeSolvers
{
    public abstract class AbstractSolver
    {
        public int[,] Maze { get; set; }
        public bool Result { get; set; }
        public int FeatureCode { get; set; }
        public int Sleep { get; set; }

        public readonly List<Point> points; // Список посещённых точек в текущем "решении"
        public readonly List<Point> allPoints; // Список всех посещённых точек во время решения

        public readonly bool isBitmapUsed; // Используется ли запись в файл

        public readonly View view; // Ссылка на класс отрисовки

        public Point current; // Текущая точка
        public Point startpoint; // Стартовая тчока
        public Point finishpoint; // Конечная точка

        public AbstractSolver(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featureCode, int sleep, bool isBitmapUsed) {
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
            MazeClear();
            current = startpoint;
            points.Clear();
            allPoints.Clear();
            points.Add(current);
            allPoints.Add(current);
            Maze[current.X, current.Y] = 2; // Если этого не сделать - будет багофича с пересечением старта один раз (WOW?)
        }

        public void Clear() {
            MazeRouteClear();
        }

        private void MazeClear()
        {
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    if (Maze[i, j] != 0)
                        Maze[i, j] = (int)PointStatus.canVisit;
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
                    view.DrawChange(allPoints[i], FeatureCode);
            }
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
        }

        public abstract void Solve();
    }
}
