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
        public List<Point> Points { get; set; } // Список посещённых точек в текущем "решении"
        public List<Point> AllPoints { get; set; } // Список всех посещённых точек во время решения
        public bool IsBitmapUsed { get; set; }
        public Point CurrentPoint { get; set; }
        public Point Startpoint { get; set; }
        public Point Finishpoint { get; set; }

        private readonly View view; // Ссылка на класс отрисовки

        public AbstractSolver(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featureCode, int sleep, bool isBitmapUsed) {
            IsBitmapUsed = isBitmapUsed;
            Maze = mazeArray;
            Points = new List<Point>();
            AllPoints = new List<Point>();
            Startpoint = startpoint;
            Finishpoint = finishpoint;
            Sleep = sleep;
            FeatureCode = featureCode;
            this.view = view;
        }

        /// <summary>
        /// Инициализация решателей лабиринта
        /// </summary>
        public void SolversInit()
        {
            MazeClear();
            CurrentPoint = Startpoint;
            Points.Clear();
            AllPoints.Clear();
            Points.Add(CurrentPoint);
            AllPoints.Add(CurrentPoint);
            Maze[CurrentPoint.X, CurrentPoint.Y] = (int)PointStatus.alreadyVisited;
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
            for (int i = 0; i < AllPoints.Count; i++)
            {
                if ((AllPoints[i].X != Startpoint.X || AllPoints[i].Y != Startpoint.Y)
                    && (AllPoints[i].X != Finishpoint.X || AllPoints[i].Y != Finishpoint.Y))
                    view.DrawChange(AllPoints[i], FeatureCode);
            }
        }

        /// <summary>
        /// Метод возврата, если зашли в тупик
        /// </summary>
        /// <param name="look">Обновляемое значение взгляда точки-решателя лабиринта</param>
        public void PointRollback(ref int look)
        {
            view.DrawChange(Points.Last(), Color.Coral);
            Point clr = GetClearPoint(CurrentPoint, Points[Points.Count - 2]);
            look = LookUpdate(CurrentPoint, clr);
            view.DrawChange(clr, Color.Coral);
            Points.RemoveAt(Points.Count - 1);
            if (Points.Count != 0)
            {
                CurrentPoint = Points.Last();
                view.DrawChange(CurrentPoint, Color.Red);
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
            if (x1.X == x2.X)
            {
                if (x1.Y > x2.Y)
                    return (int)PointDirections.up;
                else
                    return (int)PointDirections.down;
            }
            else
            {
                if (x1.X > x2.X)
                    return (int)PointDirections.left;
                else
                    return (int)PointDirections.right;
            }
        }

        /// <summary>
        /// Поиск точки, находящейся между двумя точками
        /// </summary>
        /// <param name="x1">Первая точка</param>
        /// <param name="x2">Вторая точка</param>
        /// <returns>Возвращает точку, которая находится между двумя точками</returns>
        private Point GetClearPoint(Point x1, Point x2)
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
            Point clr = GetClearPoint(CurrentPoint, selected);
            if (!IsBitmapUsed)
                AllPoints.Add(clr);
            Maze[clr.X, clr.Y] = (int)PointStatus.alreadyVisited;
            view.DrawChange(clr, Color.SkyBlue);
            view.DrawChange(CurrentPoint, Color.SkyBlue);
            Points.Add(selected);
            if (!IsBitmapUsed)
                AllPoints.Add(selected);
            CurrentPoint = Points.Last();
            view.DrawChange(CurrentPoint, Color.Red);
            Maze[selected.X, selected.Y] = (int)PointStatus.alreadyVisited;
        }

        public abstract void Solve();
    }
}
