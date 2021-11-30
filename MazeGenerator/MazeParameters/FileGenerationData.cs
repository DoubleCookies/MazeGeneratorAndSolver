namespace MazeGenerator
{
    public class FileGenerationData
    {
        private int count;
        private int size;
        private bool isWithSolution;
        private string selectedPath;

        public int Count { get => count; set => count = value; }
        public int Size { get => size; set => size = value; }
        public bool IsWithSolution { get => isWithSolution; set => isWithSolution = value; }
        public string SelectedPath { get => selectedPath; set => selectedPath = value; }
    }
}
