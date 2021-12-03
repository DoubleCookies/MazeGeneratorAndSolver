using System.Collections.Generic;
using System.Drawing;

namespace MazeGenerator
{
    public enum PointStatus
    {
        canVisit = 1,
        alreadyVisited = 2
    }

    public enum PointDirections
    { 
        right = 0,
        down = 1,
        left = 2,
        up = 3
    }

    public static class PointOperations
    {
        static readonly List<Point> possPoints = new List<Point>();
        static readonly List<int> directions = new List<int>();


        public static List<Point> GetConnectedVisitedPoints(int[,] mazeArray, int x, int y, int cellCount) {
            return PossiblePoints(mazeArray, x, y, cellCount, (int)PointStatus.alreadyVisited);
        }

        public static List<Point> GetConnectedNotVisitedPoints(int[,] mazeArray, int x, int y, int cellCount)
        {
            return PossiblePoints(mazeArray, x, y, cellCount, (int)PointStatus.canVisit);
        }


        /// <summary>
        /// Поиск возможных точек для посещения
        /// </summary>
        /// <returns>Возвращает список точек, возможных для посещения</returns>
        private static List<Point> PossiblePoints(int[,] mazeArray, int x, int y, int cellCount, int pointStatus)
        {
            int mazeWidth = mazeArray.GetLength(0);
            int mazeHeight = mazeArray.GetLength(1);
            possPoints.Clear();
            if (x - cellCount > 0 && mazeArray[x - cellCount, y] == pointStatus)
                possPoints.Add(new Point(x - cellCount, y));
            if (x + cellCount < mazeWidth && mazeArray[x + cellCount, y] == pointStatus)
                possPoints.Add(new Point(x + cellCount, y));
            if (y - cellCount > 0 && mazeArray[x, y - cellCount] == pointStatus)
                possPoints.Add(new Point(x, y - cellCount));
            if (y + cellCount < mazeHeight && mazeArray[x, y + cellCount] == pointStatus)
                possPoints.Add(new Point(x, y + cellCount));
            return possPoints;
        }

        /// <summary>
        /// Поиск возможных точек для посещения + поиск возможных направлений движения
        /// </summary>
        /// <returns>Возвращает список точек, возможных для посещения</returns>
        public static List<Point> PossiblePointsWithDirections(int[,] mazeArray, Point current)
        {
            directions.Clear();
            possPoints.Clear();
            int x = current.X;
            int y = current.Y;
            int mazeWidth = mazeArray.GetLength(0);
            int mazeHeight = mazeArray.GetLength(1);

            if (x + 2 < mazeWidth && CanVisitPoint(mazeArray[x + 2, y]) && CanVisitPoint(mazeArray[x + 1, y]))
            {
                possPoints.Add(new Point(x + 2, y));
                directions.Add(0);
            }
            if (y + 2 < mazeHeight && CanVisitPoint(mazeArray[x, y + 2]) && CanVisitPoint(mazeArray[x, y + 1]))
            {
                possPoints.Add(new Point(x, y + 2));
                directions.Add(1);
            }
            if (x - 2 > 0 && CanVisitPoint(mazeArray[x - 2, y]) && CanVisitPoint(mazeArray[x - 1, y]))
            {
                possPoints.Add(new Point(x - 2, y));
                directions.Add(2);
            }
            if (y - 2 > 0 && CanVisitPoint(mazeArray[x, y - 2]) && CanVisitPoint(mazeArray[x, y - 1]))
            {
                possPoints.Add(new Point(x, y - 2));
                directions.Add(3);
            }
            return possPoints;
        }

        private static bool IsPointVisited(int pointValue)
        {
            int pointStatus = (int)PointStatus.alreadyVisited;
            return pointValue == pointStatus;
        }

        private static bool CanVisitPoint(int pointValue) {
            int pointStatus = (int)PointStatus.canVisit;
            return pointValue == pointStatus;
        }

        /// <summary>
        /// Поиск направления для перехода к след. точки (метод левых поворотов)
        /// Сначала проверяет взгляд (по очереди) налево, вперёд, назад, направо
        /// </summary>
        /// <param name="look">Текущий "взгляд" точки (0 - право, 1 - низ, 2 - лево, 3 - верх)</param>
        /// <returns>Возвращает индекс точки из списка доступных точек</returns>
        public static Point SelectedMoveLeft(ref int look)
        {
            int selected;
            if (directions.Contains((look + 3) % 4))
                selected = directions.BinarySearch((look + 3) % 4);
            else if (directions.Contains(look))
                selected = directions.BinarySearch(look);
            else if (directions.Contains((look + 2) % 4))
                selected = directions.BinarySearch((look + 2) % 4);
            else
                selected = directions.BinarySearch((look + 1) % 4);
            look = directions[selected];
            return possPoints[selected];
        }

        /// <summary>
        /// Поиск направления для перехода к след. точки (метод правых поворотов)
        /// Сначала проверяет взгляд (по очереди) направо, вперёд, назад, налево
        /// </summary>
        /// <param name="look">Текущий "взгляд" точки (0 - право, 1 - низ, 2 - лево, 3 - верх)</param>
        /// <returns>Возвращает индекс точки из списка доступных точек</returns>
        public static Point SelectedMoveRight(ref int look)
        {
            int selected;
            if (directions.Contains((look + 1) % 4))
                selected = directions.BinarySearch((look + 1) % 4);
            else if (directions.Contains(look))
                selected = directions.BinarySearch(look);
            else if (directions.Contains((look + 2) % 4))
                selected = directions.BinarySearch((look + 2) % 4);
            else
                selected = directions.BinarySearch((look + 3) % 4);
            look = directions[selected];
            return possPoints[selected];
        }
    }
}
