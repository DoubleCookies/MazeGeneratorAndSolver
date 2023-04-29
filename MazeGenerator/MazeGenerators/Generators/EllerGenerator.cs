using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace MazeGenerator.MazeGenerators.Generators
{
    public class EllerGenerator : AbstractGenerator
    {
        readonly int mazeWidth;
        readonly int mazeHeight;

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
            ProcessMiddleRows();
            ProcessLastRow();

            if (whiteProb > 0)
                WhiteGen(whiteProb);
        }

        readonly List<int> currentRow = new List<int>();

        private void ProcessFirstRow()
        {
            if (sleep != 0)
                Thread.Sleep(sleep);
            currentPoint = startpoint;

            ClearStartRowBorders();

            currentPoint = startpoint;
            ProcessFirstRowRightBorders();

            currentPoint = startpoint;
            ProcessBottomBorders();
        }

        private void ClearStartRowBorders()
        {
            int counter = 1;
            Point newPoint;
            Point belowPoint;
            for (int i = 0; i < mazeWidth - 2; i += 2)
            {
                if (featureCode > 0) {
                    view.DrawChange(currentPoint, featureCode);
                }
                belowPoint = new Point(currentPoint.X, currentPoint.Y + 2);
                CreateAndDrawPointForVisit(belowPoint);
                if (i != mazeWidth - 3)
                {
                    newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
                    CreateAndDrawPointForVisit(newPoint);
                    currentPoint = newPoint;

                }
                currentRow.Add(counter);
                counter++;
            }
        }

        private void ProcessFirstRowRightBorders()
        {
            Point newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
            for (int i = 0; i < currentRow.Count; i++)
            {
                if (random.NextDouble() < 0.5)
                {
                    CreateAndDrawWall(newPoint);
                }
                else
                {
                    //don't create wall and merge sets
                    if (i != currentRow.Count - 1)
                    {
                        currentRow[i + 1] = currentRow[i];
                    }
                }
                currentPoint = newPoint;
                newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
            }
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
                                CreateAndDrawWall(belowPoint);
                            }
                        }
                        size = 1;
                    }
                    else
                    {
                        size++;
                        if (random.NextDouble() < 0.5)
                        {
                            CreateAndDrawWall(belowPoint);
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
                            CreateAndDrawWall(belowPoint);
                        }
                    }
                }
                currentPoint = newPoint;
                newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
                belowPoint = new Point(currentPoint.X, currentPoint.Y + 2);
            }
        }

        private void CreateAndDrawWall(Point secondPoint)
        {
            Point clr = FoundPointBetweenTwoPoints(currentPoint, secondPoint);
            mazeArray[clr.X, clr.Y] = (int)PointStatus.wall;
            view.DrawChange(clr, Color.FromArgb(60, 60, 60));
        }

        private void CreateAndDrawPointForVisit(Point secondPoint)
        {
            Point clr = FoundPointBetweenTwoPoints(currentPoint, secondPoint);
            mazeArray[clr.X, clr.Y] = (int)PointStatus.canVisit;
            view.DrawChange(clr, featureCode);
        }

        private void ProcessMiddleRows()
        {
            if (sleep != 0)
                Thread.Sleep(sleep);
            for (int j = 2; j < mazeHeight - 2; j += 2)
            {
                Point tempPoint;
                currentPoint = new Point(1, currentPoint.Y + 2);

                //copy and draw row above (and its borders)
                for (int i = 1; i < mazeWidth - 1; i++)
                {
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
                        CreateAndDrawPointForVisit(newPoint);
                        currentPoint = newPoint;
                    }
                }

                //remove bottom borders if cell has it + remove cell from set after it
                int counter = 0;
                currentPoint.X = 1;
                for (int i = 0; i < mazeWidth - 2; i += 2)
                {
                    tempPoint = new Point(currentPoint.X + i, currentPoint.Y + 1);
                    if (mazeArray[tempPoint.X, tempPoint.Y] == (int)PointStatus.wall)
                    {
                        currentRow[counter] = -1;
                        mazeArray[tempPoint.X, tempPoint.Y] = (int)PointStatus.canVisit;
                        view.DrawChange(tempPoint, featureCode);
                    }
                    counter++;
                }

                FillCurrentRow();

                //process right borders
                currentPoint.X = 1;
                newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
                for (int i = 0; i < currentRow.Count; i++)
                {
                    if (i != currentRow.Count - 1)
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
                                CreateAndDrawWall(newPoint);
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

        private void FillCurrentRow()
        {
            int number = 1;
            for (int i = 0; i < currentRow.Count; i++)
            {
                if (currentRow[i] > 0)
                    continue;
                else
                {
                    while (currentRow.Contains(number))
                    {
                        number++;
                    }
                    currentRow[i] = number;
                }
            }
        }

        private void ProcessLastRow()
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
                        CreateAndDrawPointForVisit(newPoint);
                        currentRow[i + 1] = currentRow[i];
                    }
                }
                currentPoint.X += 2;
                newPoint = new Point(currentPoint.X + 2, currentPoint.Y);
            }
        }
    }
}
