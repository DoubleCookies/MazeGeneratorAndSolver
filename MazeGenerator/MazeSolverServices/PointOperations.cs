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


        /// <summary>
        /// Поиск уже посещённых точек
        /// </summary>
        /// <returns>Возвращает список уже посещённых точек</returns>
        public static List<Point> GetConnectedVisitedPoints(int[,] mazeArray, int x, int y, int cellCount) 
        {
            return PossiblePoints(mazeArray, x, y, cellCount, (int)PointStatus.alreadyVisited);
        }

        /// <summary>
        /// Поиск ещё не посещённых точек на определенной дистанции от текущей точки
        /// </summary>
        /// <returns>Возвращает список ещё не посещённых точек</returns>
        public static List<Point> GetConnectedNotVisitedPoints(int[,] mazeArray, int x, int y, int cellCount)
        {
            return PossiblePoints(mazeArray, x, y, cellCount, (int)PointStatus.canVisit);
        }


        /// <summary>
        /// Поиск возможных точек для посещения на определенной дистанции от текущей точки
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
        /// Поиск возможных точек для посещения с учтом направлений движения
        /// </summary>
        /// <returns>Возвращает список точек, возможных для посещения</returns>
        public static int PossiblePointsWithDirections(int[,] mazeArray, Point current)
        {
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

        /// <summary>
        /// Проверяет, можно ли посетить данную точку
        /// </summary>
        /// <param name="pointValue">Значение точки</param>
        /// <returns>true, если эту точку можно посетить</returns>
        private static bool CanVisitPoint(int pointValue) {
            int pointStatus = (int)PointStatus.canVisit;
            return pointValue == pointStatus;
        }

        /// <summary>
        /// Поиск след. точки (метод левых поворотов)
        /// Проверяет доступные направления для посещения: налево, вперёд, назад, направо
        /// </summary>
        /// <param name="look">Обновляемый "взгляд" точки</param>
        /// <returns>Возвращает новую точку для перехода</returns>
        public static Point SelectedMoveLeft(ref int look)
        {
            Point returnPoint;
            FoundPointsData data;
            do 
            {
                data = FindPointForDirection((PointDirections)((look + 3) % 4));
                if (data != null) break;
                data = FindPointForDirection((PointDirections)look);
                if (data != null) break;
                data = FindPointForDirection((PointDirections)((look + 2) % 4));
                if (data != null) break;
                data = FindPointForDirection((PointDirections)((look + 1) % 4));
            } while (data == null);
            look = (int)data.Direction;
            returnPoint = data.Point;
            return returnPoint;
        }

        /// <summary>
        /// Поиск след. точки (метод правых поворотов)
        /// Проверяет доступные направления для посещения: направо, вперёд, назад, налево
        /// </summary>
        /// <param name="look">Обновляемый "взгляд" точки</param>
        /// <returns>Возвращает новую точку для перехода</returns>
        public static Point SelectedMoveRight(ref int look)
        {
            Point returnPoint;
            FoundPointsData data;
            do
            {
                data = FindPointForDirection((PointDirections)((look + 1) % 4));
                if (data != null) break;
                data = FindPointForDirection((PointDirections)look);
                if (data != null) break;
                data = FindPointForDirection((PointDirections)((look + 2) % 4));
                if (data != null) break;
                data = FindPointForDirection((PointDirections)((look + 3) % 4));
            } while (data == null);
            look = (int)data.Direction;
            returnPoint = data.Point;

            return returnPoint;
        }

        /// <summary>
        /// Поиск след. точки (метод случайных поворотов)
        /// </summary>
        /// <returns>Возвращает случайную точку из доступных для посещения</returns>
        public static Point SelectedMoveRandom() {
            return foundPoints[rand.Next(0, foundPoints.Count)].Point;
        }

        /// <summary>
        /// Поиск точки с нужным направлением среди доступных
        /// </summary>
        /// <param name="direction">Направление точки</param>
        /// <returns>Возвращает точку, если она найдена</returns>
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
