using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace MazeGenerator.MazeGenerators.Generators
{
    public class EllerGenerator : AbstractGenerator
    {
        int mazeWidth;
        int mazeHeight;

        public EllerGenerator(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featurecode, int sleep, Random random)
            : base(mazeArray, startpoint, finishpoint, view, featurecode, sleep, random)
        {
            mazeWidth = mazeArray.GetLength(0);
            mazeHeight = mazeArray.GetLength(1);
        }

        public override void Generate(bool fromStart, double whiteProb)
        {
            GenerateMaze(whiteProb);
        }

        private void GenerateMaze(double whiteProb)
        {
            view.DrawBlackPoints(blackPoints);
            ClearBlackPointsList();

            ProcessFirstRow();
            processMiddleRows();

            if (whiteProb > 0)
                WhiteGen(whiteProb);
        }

        List<int> currentRow = new List<int>();
        List<int> previousRow = new List<int>();

        private void ProcessFirstRow()
        {
            currentPoint = startpoint;
            // clear row from borders (in row and below) and assign sets
            int counter = 1;
            Point newPoint;
            Point belowPoint;
            for (int i = 0; i < mazeWidth - 2; i += 2)
            {
                belowPoint = new Point(currentPoint.X, currentPoint.Y + 2);
                Point clr2 = FoundPointBetweenTwoPoints(currentPoint, belowPoint);
                mazeArray[clr2.X, clr2.Y] = (int)PointStatus.canVisit;
                view.DrawChange(clr2, featureCode);
                if (i != mazeWidth - 3)
                {
                    newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
                    Point clr = FoundPointBetweenTwoPoints(currentPoint, newPoint);
                    mazeArray[clr.X, clr.Y] = (int)PointStatus.canVisit;
                    currentPoint = newPoint;
                    view.DrawChange(clr, featureCode);
                }
                currentRow.Add(counter);
                counter++;
            }

            // right borders
            currentPoint = startpoint;
            newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
            for (int i = 0; i < currentRow.Count; i++)
            {
                if (random.NextDouble() < 0.5)
                {
                    // create border
                    Point clr = FoundPointBetweenTwoPoints(currentPoint, newPoint);
                    mazeArray[clr.X, clr.Y] = (int)PointStatus.wall;
                    view.DrawChange(clr, Color.FromArgb(60, 60, 60));
                }
                else
                {
                    // don't create it
                    if (i != currentRow.Count - 1)
                    {
                        currentRow[i + 1] = currentRow[i];
                    }
                }
                currentPoint = newPoint;
                newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
            }

            // bottom borders
            currentPoint = startpoint;
            newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
            belowPoint = new Point(currentPoint.X, currentPoint.Y + 2);
            int currentSet;
            bool hasOneNonBorder = false;
            int size = 1;
            for (int i = 0; i < currentRow.Count; i++)
            {
                currentSet = currentRow[i];
                if (i != currentRow.Count - 1)
                {
                    int nextSet = currentRow[i + 1];
                    if (currentSet != nextSet)
                    {
                        if (hasOneNonBorder == true)
                        {
                            if (random.NextDouble() < 0.5)
                            {
                                createAndDrawWall(belowPoint);
                            }
                        }
                        size = 1;
                        hasOneNonBorder = false;
                    }
                    else
                    {
                        size++;
                        if (random.NextDouble() < 0.5)
                        {
                            createAndDrawWall(belowPoint);
                        }
                        else
                        {
                            hasOneNonBorder = true;
                        }
                    }
                }
                else
                {
                    if (size == 1 || hasOneNonBorder == false)
                    {
                        continue;
                    }
                    else
                    {
                        if (random.NextDouble() < 0.5)
                        {
                            createAndDrawWall(belowPoint);
                        }
                    }
                }
                currentPoint = newPoint;
                newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
                belowPoint = new Point(currentPoint.X, currentPoint.Y + 2);
            }

            int a = 0;
        }

        private void createAndDrawWall(Point belowPoint)
        {
            Point clr = FoundPointBetweenTwoPoints(currentPoint, belowPoint);
            mazeArray[clr.X, clr.Y] = (int)PointStatus.wall;
            view.DrawChange(clr, Color.FromArgb(60, 60, 60));
        }

        private void processMiddleRows()
        {
            for (int j = 2; j < mazeHeight - 4; j += 2) {

            }
        }

        private void processLastRow()
        {

        }
    }
}
