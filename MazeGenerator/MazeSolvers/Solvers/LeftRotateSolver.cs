using System.Drawing;
using System.Threading;

namespace MazeGenerator.MazeSolvers.Solvers
{
    public class LeftRotateSolver : AbstractSolver
    {
        public LeftRotateSolver(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featureCode, int sleep, bool isBitmapUsed)
            : base(mazeArray, startpoint, finishpoint, view, featureCode, sleep, isBitmapUsed) { }

        public override void Solve()
        {
            SolversInit();
            int look = (int)PointDirections.down;
            bool solutionFound = false;
            int count;
            while (!solutionFound)
            {
                Thread.Sleep(Sleep);
                count = PointOperations.PossiblePointsWithDirections(Maze, current);
                if (count != 0)
                {
                    Point newPoint = PointOperations.SelectedMoveLeft(ref look);
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
