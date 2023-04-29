using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Threading;

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
            processLastRow();

            if (whiteProb > 0)
                WhiteGen(whiteProb);
        }

        List<int> currentRow = new List<int>();

        private void ProcessFirstRow()
        {
            if (sleep != 0)
                Thread.Sleep(sleep);
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
            ProcessBottomBorders();
        }

        private void ProcessBottomBorders()
        {
            if (sleep != 0)
                Thread.Sleep(sleep);
            Dictionary<int, bool> isWithBorder = new Dictionary<int, bool>();
            Point newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
            Point belowPoint = new Point(currentPoint.X, currentPoint.Y + 2);
            int currentSet;
            int size = 1;
            for (int i = 0; i < currentRow.Count; i++)
            {
                currentSet = currentRow[i];
                if (!isWithBorder.ContainsKey(currentSet))
                    isWithBorder.Add(currentSet, false);

                if (i != currentRow.Count - 1)
                {
                    int nextSet = currentRow[i + 1];
                    if (currentSet != nextSet)
                    {
                        isWithBorder.TryGetValue(currentSet, out bool isOneBorder);
                        if (isOneBorder == true)
                        {
                            if (random.NextDouble() < 0.5)
                            {
                                createAndDrawWall(belowPoint);
                            }
                        }
                        size = 1;
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
                            isWithBorder[currentSet] = true;
                        }
                    }
                }
                else
                {
                    if (size == 1 || isWithBorder.TryGetValue(currentSet, out _) == false)
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
        }

        private void createAndDrawWall(Point belowPoint)
        {
            Point clr = FoundPointBetweenTwoPoints(currentPoint, belowPoint);
            mazeArray[clr.X, clr.Y] = (int)PointStatus.wall;
            view.DrawChange(clr, Color.FromArgb(60, 60, 60));
        }

        private void processMiddleRows()
        {
            if (sleep != 0)
                Thread.Sleep(sleep);
            for (int j = 2; j < mazeHeight - 2; j += 2) {
                Point tempPoint;
                currentPoint = new Point(1, currentPoint.Y + 2);

                //copy and draw row above
                for (int i = 1; i < mazeWidth - 1; i++) {
                    mazeArray[i, currentPoint.Y] = mazeArray[i, currentPoint.Y - 2];
                    tempPoint = new Point(i, currentPoint.Y);
                    if (mazeArray[i, currentPoint.Y] == (int)PointStatus.wall)
                        view.DrawChange(tempPoint, Color.FromArgb(60, 60, 60));
                    else
                        view.DrawChange(tempPoint, featureCode);

                    mazeArray[i, currentPoint.Y + 1] = mazeArray[i, currentPoint.Y - 1];
                    tempPoint = new Point(i, currentPoint.Y + 1);
                    if (mazeArray[i, currentPoint.Y + 1] == (int)PointStatus.wall)
                        view.DrawChange(tempPoint, Color.FromArgb(60, 60, 60));
                    else
                        view.DrawChange(tempPoint, featureCode);
                }

                //remove right borders
                Point newPoint;
                currentPoint.X = 1;
                for (int i = 0; i < mazeWidth - 2; i += 2)
                {
                    if (i != mazeWidth - 3)
                    {
                        newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
                        Point clr = FoundPointBetweenTwoPoints(currentPoint, newPoint);
                        mazeArray[clr.X, clr.Y] = (int)PointStatus.canVisit;
                        currentPoint = newPoint;
                        view.DrawChange(clr, featureCode);
                    }
                }

                //remove bottom borders if cell has it + remove cell from set after it
                int counter = 0;
                currentPoint.X = 1;
                for (int i = 0; i < mazeWidth - 2; i += 2)
                {
                    tempPoint = new Point(currentPoint.X + i, currentPoint.Y+1);
                    if (mazeArray[tempPoint.X, tempPoint.Y] == (int)PointStatus.wall) {
                        currentRow[counter] = -1;
                        mazeArray[tempPoint.X, tempPoint.Y] = (int)PointStatus.canVisit;
                        view.DrawChange(tempPoint, featureCode);
                    }
                    counter++;
                }

                //fill currentRow with values
                int number = 1;
                for (int i = 0; i < currentRow.Count; i++) {
                    if (currentRow[i] > 0)
                        continue;
                    else {
                        while (currentRow.Contains(number)) {
                            number++;
                        }
                        currentRow[i] = number;
                    }
                }


                //process right borders
                currentPoint.X = 1;
                newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
                for (int i = 0; i < currentRow.Count; i++)
                {
                    if (i != currentRow.Count-1)
                    {
                        if (currentRow[i] == currentRow[i + 1])
                        {
                            // create border
                            Point clr = FoundPointBetweenTwoPoints(currentPoint, newPoint);
                            mazeArray[clr.X, clr.Y] = (int)PointStatus.wall;
                            view.DrawChange(clr, Color.FromArgb(60, 60, 60));
                        }
                        else
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
                        }
                    }
                    currentPoint = newPoint;
                    newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
                }

                //process bottom borders
                currentPoint.X = 1;
                ProcessBottomBorders();
            }
        }

        private void processLastRow()
        {
            if (sleep != 0)
                Thread.Sleep(sleep);
            Point tempPoint;
            currentPoint.X = 1;
            Point newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
            //draw all walls
            for (int i = 0; i < mazeWidth - 1; i++)
            {
                tempPoint = new Point(currentPoint.X + i, currentPoint.Y + 1);
                mazeArray[tempPoint.X, tempPoint.Y] = (int)PointStatus.wall;
                view.DrawChange(tempPoint, Color.FromArgb(60, 60, 60));
            }

            //remove some right borders
            currentPoint.X = 1;
            for (int i = 0; i < currentRow.Count; i++)
            {
                if (i != currentRow.Count - 1)
                {
                    if (currentRow[i] != currentRow[i + 1])
                    {
                        Point clr = FoundPointBetweenTwoPoints(currentPoint, newPoint);
                        mazeArray[clr.X, clr.Y] = (int)PointStatus.canVisit;
                        view.DrawChange(clr, featureCode);
                        currentRow[i + 1] = currentRow[i];
                    }
                }
                currentPoint.X += 2;
                newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
            }

            int a = 0;
        }
    }
}
