using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MazeGenerator.MazeSolvers.Solvers
{
    public class RandomRotateSolver : AbstractSolver
    {
        public RandomRotateSolver(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featureCode, int sleep, bool isBitmapUsed)
            : base(mazeArray, startpoint, finishpoint, view, featureCode, sleep, isBitmapUsed) { }

        public override void Solve()
        {
            int look = 1;
            Random rand = new Random();
            SolversInit();
            bool solutionFound = false;
            List<Point> pointsMove;
            int count;
            while (!solutionFound)
            {
                Thread.Sleep(Sleep);
                pointsMove = PointOperations.PossiblePointsWithDirections(Maze, current);
                count = pointsMove.Count;
                if (count != 0)
                {
                    int selected = rand.Next(0, count);
                    GoToNewPoint(pointsMove[selected]);
                }
                else
                {
                    if (points.Count > 1)
                        PointRollback(ref look);
                    else
                        break;
                }
                if (current.X == finishpoint.X && current.Y == finishpoint.Y)
                    solutionFound = true;
            }
            Result = solutionFound;
        }
    }
}
