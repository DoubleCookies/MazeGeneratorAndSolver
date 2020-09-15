using System.Collections.Generic;
using System.Drawing;

namespace MazeGenerator
{
    public enum PointStatus
    {
        canVisit = 1,
        alreadyVisited = 2
    }
    public static class PointsFounders
    {
        static List<Point> possPoints = new List<Point>();
        static List<int> directions = new List<int>();

        /// <summary>
        /// Поиск возможных точек для посещения
        /// </summary>
        /// <returns>Возвращает список точек, возможных для посещения</returns>
        public static List<Point> PossiblePoints(int[,] mazeArray, int x, int y, int cellCount, int eq)
        {
            int mazeWidth = mazeArray.GetLength(0);
            int mazeHeight = mazeArray.GetLength(1);
            possPoints.Clear();
            if (x - cellCount > 0 && mazeArray[x - cellCount, y] == eq)
                possPoints.Add(new Point(x - cellCount, y));
            if (x + cellCount < mazeWidth && mazeArray[x + cellCount, y] == eq)
                possPoints.Add(new Point(x + cellCount, y));
            if (y - cellCount > 0 && mazeArray[x, y - cellCount] == eq)
                possPoints.Add(new Point(x, y - cellCount));
            if (y + cellCount < mazeHeight && mazeArray[x, y + cellCount] == eq)
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
            if (x + 2 < mazeWidth && mazeArray[x + 2, y] == 1 && mazeArray[x + 1, y] == 1)
            {
                possPoints.Add(new Point(x + 2, y));
                directions.Add(0);
            }
            if (y + 2 < mazeHeight && mazeArray[x, y + 2] == 1 && mazeArray[x, y + 1] == 1)
            {
                possPoints.Add(new Point(x, y + 2));
                directions.Add(1);
            }
            if (x - 2 > 0 && mazeArray[x - 2, y] == 1 && mazeArray[x - 1, y] == 1)
            {
                possPoints.Add(new Point(x - 2, y));
                directions.Add(2);
            }
            if (y - 2 > 0 && mazeArray[x, y - 2] == 1 && mazeArray[x, y - 1] == 1)
            {
                possPoints.Add(new Point(x, y - 2));
                directions.Add(3);
            }
            return possPoints;
        }

        /// <summary>
        /// Поиск направления для перехода к след. точки (метод левых поворотов)
        /// </summary>
        /// <param name="look">Текущий "взгляд" точки</param>
        /// <returns>Возвращает индекс точки из списка доступных точек</returns>
        public static int SelectedMoveLeft(ref int look)
        {
            int selected = -1;
            //взгляд точки - 0 - право, 1 - низ, 2 - лево, 3 - верх
            switch (look)
            {
                case 0:
                    {
                        if (directions.Contains(3))
                            selected = directions.BinarySearch(3);
                        else if (directions.Contains(0))
                            selected = directions.BinarySearch(0);
                        else if (directions.Contains(2))
                            selected = directions.BinarySearch(2);
                        else
                            selected = directions.BinarySearch(1);
                        break;
                    }
                case 1:
                    {
                        if (directions.Contains(0))
                            selected = directions.BinarySearch(0);
                        else if (directions.Contains(1))
                            selected = directions.BinarySearch(1);
                        else if (directions.Contains(3))
                            selected = directions.BinarySearch(3);
                        else
                            selected = directions.BinarySearch(2);
                        break;
                    }
                case 2:
                    {
                        if (directions.Contains(1))
                            selected = directions.BinarySearch(1);
                        else if (directions.Contains(2))
                            selected = directions.BinarySearch(2);
                        else if (directions.Contains(0))
                            selected = directions.BinarySearch(0);
                        else
                            selected = directions.BinarySearch(3);
                        break;
                    }
                case 3:
                    {
                        if (directions.Contains(2))
                            selected = directions.BinarySearch(2);
                        else if (directions.Contains(3))
                            selected = directions.BinarySearch(3);
                        else if (directions.Contains(1))
                            selected = directions.BinarySearch(1);
                        else
                            selected = directions.BinarySearch(0);
                        break;
                    }
            }
            look = FindDirection(selected);
            return selected;
        }

        /// <summary>
        /// Поиск направления для перехода к след. точки (метод правых поворотов)
        /// </summary>
        /// <param name="look">Текущий "взгляд" точки</param>
        /// <returns>Возвращает индекс точки из списка доступных точек</returns>
        public static int SelectedMoveRight(ref int look)
        {
            int selected = -1;
            // Взгляд точки - 0 - право, 1 - низ, 2 - лево, 3 - верх
            switch (look)
            {
                case 0:
                    {
                        if (directions.Contains(1))
                            selected = directions.BinarySearch(1);
                        else if (directions.Contains(0))
                            selected = directions.BinarySearch(0);
                        else if (directions.Contains(2))
                            selected = directions.BinarySearch(2);
                        else
                            selected = directions.BinarySearch(3);
                        break;
                    }
                case 1:
                    {
                        if (directions.Contains(2))
                            selected = directions.BinarySearch(2);
                        else if (directions.Contains(1))
                            selected = directions.BinarySearch(1);
                        else if (directions.Contains(3))
                            selected = directions.BinarySearch(3);
                        else
                            selected = directions.BinarySearch(0);
                        break;
                    }
                case 2:
                    {
                        if (directions.Contains(3))
                            selected = directions.BinarySearch(3);
                        else if (directions.Contains(2))
                            selected = directions.BinarySearch(2);
                        else if (directions.Contains(0))
                            selected = directions.BinarySearch(0);
                        else
                            selected = directions.BinarySearch(1);
                        break;
                    }
                case 3:
                    {
                        if (directions.Contains(0))
                            selected = directions.BinarySearch(0);
                        else if (directions.Contains(3))
                            selected = directions.BinarySearch(3);
                        else if (directions.Contains(1))
                            selected = directions.BinarySearch(1);
                        else
                            selected = directions.BinarySearch(2);
                        break;
                    }
            }
            look = FindDirection(selected);
            return selected;
        }

        /// <summary>
        /// Обновление "взгляда" точки-решателя лабиринта
        /// </summary>
        /// <param name="selected">Выбранный индекс в списке направлений</param>
        /// <returns></returns>
        public static int FindDirection(int selected)
        {
            return directions[selected];
        }
    }
}
