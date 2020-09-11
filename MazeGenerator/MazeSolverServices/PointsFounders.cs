using System.Collections.Generic;
using System.Drawing;

namespace MazeGenerator
{
    public enum PointStatus
    {
        canVisit = 1,
        alreadyVisited = 2
    }
    public class PointsFounders
    {
        List<Point> possPoints; // Список для записи точек, которые можно посетить из определённой точки
        List<int> directions; // Список для записи направлений, куда будет смотреть "решатель"

        public PointsFounders()
        {
            possPoints = new List<Point>();
            directions = new List<int>();
        }

        /// <summary>
        /// Поиск возможных точек для посещения
        /// </summary>
        /// <returns>Возвращает список точек, возможных для посещения</returns>
        public List<Point> possiblePoints(int[,] mazeArray, int x, int y, int cellCount, int eq)
        {
            possPoints.Clear();
            try
            {
                if (mazeArray[x - cellCount, y] == eq)
                    possPoints.Add(new Point(x - cellCount, y));
            }
            catch
            { }
            try
            {
                if (mazeArray[x + cellCount, y] == eq)
                    possPoints.Add(new Point(x + cellCount, y));
            }
            catch
            { }
            try
            {
                if (mazeArray[x, y - cellCount] == eq)
                    possPoints.Add(new Point(x, y - cellCount));
            }
            catch
            { }
            try
            {
                if (mazeArray[x, y + cellCount] == eq)
                    possPoints.Add(new Point(x, y + cellCount));
            }
            catch
            { }
            return possPoints;
        }

        /// <summary>
        /// Поиск возможных точек для посещения + поиск возможных направлений движения
        /// </summary>
        /// <returns>Возвращает список точек, возможных для посещения</returns>
        public List<Point> possiblePoints2(int[,] mazeArray, Point current)
        {
            directions.Clear();
            possPoints.Clear();
            int x = current.X;
            int y = current.Y;
            try
            {
                if (mazeArray[x + 2, y] == 1 && mazeArray[x + 1, y] == 1)
                {
                    possPoints.Add(new Point(x + 2, y));
                    directions.Add(0);
                }
            }
            catch
            { }
            try
            {
                if (mazeArray[x, y + 2] == 1 && mazeArray[x, y + 1] == 1)
                {
                    possPoints.Add(new Point(x, y + 2));
                    directions.Add(1);
                }
            }
            catch
            { }
            try
            {
                if (mazeArray[x - 2, y] == 1 && mazeArray[x - 1, y] == 1)
                {
                    possPoints.Add(new Point(x - 2, y));
                    directions.Add(2);
                }
            }
            catch
            { }
            try
            {
                if (mazeArray[x, y - 2] == 1 && mazeArray[x, y - 1] == 1)
                {
                    possPoints.Add(new Point(x, y - 2));
                    directions.Add(3);
                }
            }
            catch
            { }
            return possPoints;
        }

        /// <summary>
        /// Поиск направления для перехода к след. точки (метод левых поворотов)
        /// </summary>
        /// <param name="look">Текущий "взгляд" точки</param>
        /// <returns>Возвращает индекс точки из списка доступных точек</returns>
        public int selectedMoveLeft(ref int look)
        {
            int selected = -1;
            //взгляд точки - 0 - право, 1 - низ, 2 - лево, 3 - верх относительно
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
        public int selectedMoveRight(ref int look)
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
        public int FindDirection(int selected)
        {
            return directions[selected];
        }
    }
}
