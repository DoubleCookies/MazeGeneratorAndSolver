using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MazeGenerator.MazeSolvers.Solvers
{
    public class RightRotateSolver : AbstractSolver
    {
        public RightRotateSolver(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featureCode, int sleep, bool isBitmapUsed)
            : base(mazeArray, startpoint, finishpoint, view, featureCode, sleep, isBitmapUsed) { }

        public override void Solve()
        {
            SolversInit();
            int look = 1; // 0 - право, 1 - низ, 2 - лево, 3 - верх
            bool solutionFound = false;
            List<Point> pointsToMove;
            int count;
            while (!solutionFound)
            {
                Thread.Sleep(Sleep);
                pointsToMove = PointOperations.PossiblePointsWithDirections(Maze, current);
                count = pointsToMove.Count;
                if (count != 0)
                {
                    Point newPoint = PointOperations.SelectedMoveRight(ref look);
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
