using System.Drawing;

namespace MazeGenerator.MazeSolverServices
{
    public class FoundPointsData
    {
        public FoundPointsData(Point point, PointDirections direction) {
            Direction = direction;
            Point = point;
        }

        public PointDirections Direction { get; set; }
        public Point Point { get; set; }
    }
}
