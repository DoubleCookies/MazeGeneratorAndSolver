using MazeGenerator.MazeSolverServices;
using System;
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
        static readonly Random rand = new Random();

        static readonly List<FoundPointsData> foundPoints = new List<FoundPointsData>();


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
        public static int PossiblePointsWithDirections(int[,] mazeArray, Point current)
        {
            possPoints.Clear();
            foundPoints.Clear();

            int x = current.X;
            int y = current.Y;
            int mazeWidth = mazeArray.GetLength(0);
            int mazeHeight = mazeArray.GetLength(1);

            if (x + 2 < mazeWidth && CanVisitPoint(mazeArray[x + 2, y]) && CanVisitPoint(mazeArray[x + 1, y]))
                foundPoints.Add(new FoundPointsData(new Point(x + 2, y), PointDirections.right));
            if (y + 2 < mazeHeight && CanVisitPoint(mazeArray[x, y + 2]) && CanVisitPoint(mazeArray[x, y + 1]))
                foundPoints.Add(new FoundPointsData(new Point(x, y + 2), PointDirections.down));
            if (x - 2 > 0 && CanVisitPoint(mazeArray[x - 2, y]) && CanVisitPoint(mazeArray[x - 1, y]))
                foundPoints.Add(new FoundPointsData(new Point(x - 2, y), PointDirections.left));
            if (y - 2 > 0 && CanVisitPoint(mazeArray[x, y - 2]) && CanVisitPoint(mazeArray[x, y - 1]))
                foundPoints.Add(new FoundPointsData(new Point(x, y - 2), PointDirections.up));
            return foundPoints.Count;
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
            Point returnPoint;
            FoundPointsData data;
            do {
                data = FindPointForDirection(PointDirections.left);
                if (data != null) break;
                data = FindPointForDirection(PointDirections.up);
                if (data != null) break;
                data = FindPointForDirection(PointDirections.down);
                if (data != null) break;
                data = FindPointForDirection(PointDirections.right);

            } while (data == null);
            look = (int)data.Direction;
            returnPoint = data.Point;
            return returnPoint;
        }


        /// <summary>
        /// Поиск направления для перехода к след. точки (метод правых поворотов)
        /// Сначала проверяет взгляд (по очереди) направо, вперёд, назад, налево
        /// </summary>
        /// <param name="look">Текущий "взгляд" точки (0 - право, 1 - низ, 2 - лево, 3 - верх)</param>
        /// <returns>Возвращает индекс точки из списка доступных точек</returns>
        public static Point SelectedMoveRight(ref int look)
        {
            Point returnPoint;
            FoundPointsData data;
            do
            {
                data = FindPointForDirection(PointDirections.right);
                if (data != null) break;
                data = FindPointForDirection(PointDirections.up);
                if (data != null) break;
                data = FindPointForDirection(PointDirections.down);
                if (data != null) break;
                data = FindPointForDirection(PointDirections.left);

            } while (data == null);
            look = (int)data.Direction;
            returnPoint = data.Point;

            return returnPoint;
        }

        public static Point SelectedMoveRandom() {
            return foundPoints[rand.Next(0, foundPoints.Count)].Point;
        }

        private static FoundPointsData FindPointForDirection(PointDirections direction)
        {
            foreach (FoundPointsData data in foundPoints)
            {
                if (data.Direction == direction)
                    return data;
            }
            return null;
        }
    }
}
