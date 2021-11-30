using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class GenerationData
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
