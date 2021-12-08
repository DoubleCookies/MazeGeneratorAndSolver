using System.Drawing;

namespace MazeGenerator
{
    public class MazeParamsData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Sleep { get; set; }
        public bool AreAdditionalParamsEnabled { get; set; }
        public bool IsGeneratedFromStart { get; set; }
        public double Prob { get; set; }
        public double WhiteProb { get; set; }
        public Point StartPoint { get; set; }
        public Point FinishPoint { get; set; }
        public bool IsDrawMethodEnabled { get; set; }
        public int FeatureCode { get; set; }
    }
}
