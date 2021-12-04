using System.Drawing;
using System.Threading;

namespace MazeGenerator.MazeSolvers.Solvers
{
    public class RightRotateSolver : AbstractSolver
    {
        public RightRotateSolver(int[,] mazeArray, Point startpoint, Point finishpoint, View view, int featureCode, int sleep, bool isBitmapUsed)
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
                count = PointOperations.PossiblePointsWithDirections(Maze, CurrentPoint);
                if (count != 0)
                {
                    Point newPoint = PointOperations.SelectedMoveRight(ref look);
                    GoToNewPoint(newPoint);
                }
                else
                {
                    if (Points.Count > 1)
                        PointRollback(ref look);
                    else
                        break;
                }
                if (CurrentPoint.X == Finishpoint.X && CurrentPoint.Y == Finishpoint.Y)
                    solutionFound = true;
            }
            Result = solutionFound;
        }
    }
}
