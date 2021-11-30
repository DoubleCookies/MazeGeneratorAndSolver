using System.Drawing;

namespace MazeGenerator
{
    public class MazeParamsData
    {
        int width;
        int height;
        int sleep;

        bool areAdditionalParamsEnabled;
        bool isGeneratedFromStart;
        double prob;
        double whiteProb;
        Point startPoint;
        Point finishPoint;

        bool isDrawMethodEnabled;
        int featureCode;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int Sleep { get => sleep; set => sleep = value; }
        public bool AreAdditionalParamsEnabled { get => areAdditionalParamsEnabled; set => areAdditionalParamsEnabled = value; }
        public bool IsGeneratedFromStart { get => isGeneratedFromStart; set => isGeneratedFromStart = value; }
        public double Prob { get => prob; set => prob = value; }
        public double WhiteProb { get => whiteProb; set => whiteProb = value; }
        public Point StartPoint { get => startPoint; set => startPoint = value; }
        public Point FinishPoint { get => finishPoint; set => finishPoint = value; }
        public bool IsDrawMethodEnabled { get => isDrawMethodEnabled; set => isDrawMethodEnabled = value; }
        public int FeatureCode { get => featureCode; set => featureCode = value; }
    }
}
