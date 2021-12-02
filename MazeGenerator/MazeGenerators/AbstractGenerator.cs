using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator.MazeGenerators
{
    public abstract class AbstractGenerator
    {
        public readonly View view;
        public readonly int[,] mazeArray;
        public readonly List<Point> points;
        public readonly List<Point> blackPoints;
        public readonly Random random;
        public Point startpoint;
        public Point finishpoint;
        public Point currentPoint;
        public int lastX; // Точка последней посещённой вертикали для повышения эффективности алгоритма Hunt-And-Kill
        public readonly int featureCode; // Код особенности отрисовки
        public readonly int sleep; // Время остановки потока при отрисовке
        public bool ignored; // Были ли проигнорированные точки в алгоритме Hunt-And-Kill (если к ним не было прямого доступа)
        public int ignoredCount; // Счётчик циклов игнорирования


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
        public AbstractGenerator(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featurecode, int sleep, Random random)
        {
            this.mazeArray = mazeArray;
            this.random = random;
            this.startpoint = startpoint;
            this.finishpoint = finishpoint;
            points = new List<Point>();
            blackPoints = new List<Point>();
            this.sleep = sleep;
            this.view = view;
            featureCode = featurecode;
        }

        /// <summary>
        /// Метод для заполнения массива исходными данными
        /// </summary>
        /// <param name="noPointProbability">Вероятность того, что точка будет проигнорирована</param>
        public void FillMazeArray(double noPointProbability)
        {
            for (int i = 1; i < mazeArray.GetLength(0); i += 2)
            {
                for (int j = 1; j < mazeArray.GetLength(1); j += 2)
                {
                    if (random.NextDouble() < noPointProbability
                        && !(i == startpoint.X && j == startpoint.Y) && !(i == finishpoint.X && j == finishpoint.Y))
                        blackPoints.Add(new Point(i, j));
                    else
                        mazeArray[i, j] = (int)PointStatus.canVisit;
                }
            }
        }

        /// <summary>
        /// Метод "удаления" части стен в лабиринте.
        /// Удаление происходит только в том случае, если после удаления стены можно будет пройти дальше.
        /// </summary>
        public void WhiteGen(double whiteProb)
        {
            for (int i = 0; i < mazeArray.GetLength(0); i++)
            {
                for (int j = 0; j < mazeArray.GetLength(1); j++)
                {
                    if (((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))
                        && i != 0 && j != 0 && i != mazeArray.GetLength(0) - 1 && j != mazeArray.GetLength(1) - 1)
                    {
                        if (mazeArray[i, j] == 0 && random.NextDouble() < whiteProb)
                        {
                            List<Point> pointToConnect = PointOperations.PossiblePoints(mazeArray, i, j, 1, (int)PointStatus.alreadyVisited);
                            if (pointToConnect.Count > 1)
                            {// Делаем тоннель так, чтобы можно было связать две точки
                                mazeArray[i, j] = 1;
                                if (featureCode == 0)
                                    view.DrawChange(new Point(i, j), Color.White);
                                else
                                    view.DrawChange(new Point(i, j), featureCode);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Поиск точки, находящейся между двумя точками
        /// </summary>
        /// <param name="x1">Первая точка</param>
        /// <param name="x2">Вторая точка</param>
        /// <returns>Возвращает точку, которая находится между двумя точками</returns>
        public Point FoundPointBetweenTwoPoints(Point x1, Point x2)
        {
            return new Point((x1.X + x2.X) / 2, (x1.Y + x2.Y) / 2);
        }

        /// <summary>
        /// Очистка "чёрных" точек
        /// </summary>
        public void ClearBlackPointsList()
        {
            blackPoints.Clear();
        }

        public abstract void Generate(bool fromStart, double whiteProb);
    }
}
