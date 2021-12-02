using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MazeGenerator.MazeGenerators
{
    public class BacktrackingGenerator : AbstractGenerator
    {
        public BacktrackingGenerator(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featurecode, int sleep, Random random) : base(mazeArray, startpoint, finishpoint, view, featurecode, sleep, random) {

        }

        public override void Generate(bool fromStart, double whiteProb)
        {
            BackTrackMazeGenerate(fromStart, whiteProb);
        }

        /// <summary>
        /// Генерация лабиринта BackTracking'ом
        /// </summary>
        /// <param name="fromStart">Начинать ли генерацию со старта</param>

        /// <param name="whiteProb">Вероятность убрать стену</param>
        public void BackTrackMazeGenerate(bool fromStart, double whiteProb)
        {
            view.DrawBlackPoints(blackPoints);
            ClearBlackPointsList();

            currentPoint = fromStart ? startpoint : finishpoint;
            mazeArray[currentPoint.X, currentPoint.Y] = (int)PointStatus.alreadyVisited;
            points.Add(currentPoint);
            while (points.Count != 0)
            {
                if (sleep != 0)
                    Thread.Sleep(sleep);
                List<Point> possiblePoints = PointOperations.PossiblePoints(mazeArray, currentPoint.X, currentPoint.Y, 2, (int)PointStatus.canVisit);
                int pointsToVisit = possiblePoints.Count;
                if (pointsToVisit != 0)
                    GoToNewPoint(possiblePoints[random.Next(0, pointsToVisit)]);
                else
                    PointRollback();
            }
            if (whiteProb > 0)
                WhiteGen(whiteProb);
        }

        /// <summary>
        /// Переход в новую точку при построении лабиринта
        /// </summary>
        /// <param name="selected">Выбранная точка</param>
        /// <param name="pointsMove">Список точек</param>
        private void GoToNewPoint(Point newPoint)
        {
            Point clr = FoundPointBetweenTwoPoints(currentPoint, newPoint);
            mazeArray[clr.X, clr.Y] = (int)PointStatus.alreadyVisited;
            if (featureCode == 0)
                view.DrawChange(clr, Color.White);
            else
                view.DrawChange(clr, featureCode);
            view.DrawChange(currentPoint, Color.FromArgb(255, 170, 102));
            points.Add(newPoint);
            currentPoint = points.Last();
            view.DrawChange(currentPoint, Color.Red);
            mazeArray[newPoint.X, newPoint.Y] = (int)PointStatus.alreadyVisited;
        }

        /// <summary>
        /// Возвращение в уже посещённые точки
        /// </summary>
        private void PointRollback()
        {
            if (featureCode == 0)
                view.DrawChange(points.Last(), Color.White);
            else
                view.DrawChange(points.Last(), featureCode);
            points.RemoveAt(points.Count - 1);
            if (points.Count != 0)
            {
                currentPoint = points.Last();
                view.DrawChange(currentPoint, Color.Red);
            }
        }
    }
}
