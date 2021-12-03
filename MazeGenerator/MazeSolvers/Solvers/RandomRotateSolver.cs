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
            // List<Point> pointsMove;
            int count;
            while (!solutionFound)
            {
                Thread.Sleep(Sleep);
                count = PointOperations.PossiblePointsWithDirections(Maze, current);
                if (count != 0)
                {
                    Point newPoint = PointOperations.SelectedMoveRandom();
                    GoToNewPoint(newPoint);
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
