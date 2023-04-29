using MazeGenerator.MazeGenerators;
using MazeGenerator.MazeGenerators.Generators;
using MazeGenerator.MazeSolvers;
using MazeGenerator.MazeSolvers.Solvers;
using System;
using System.Drawing;

namespace MazeGenerator
{
    public class MazeObject
    {
        public bool IsSolutionFound { get; set; }
        public int[,] Maze { get; set; }
        public int Sleep { get; set; }
        public int FeatureCode { get; set; }

        private Point startpoint;
        private Point finishpoint;

        private readonly View view;
        private readonly Random random;

        private readonly double blackProb; //Вероятность появления доп. стен
        private readonly double whiteProb; //Вероятность убрать стену

        private readonly bool fromStart; // Генерация лабиринта со старта или с финиша
        private readonly bool bitmap;

        private AbstractSolver solver;


        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="sleep">Задержка отрисовки</param>
        /// <param name="blackProb">Вероятность генерации пустых мест</param>
        /// <param name="start">Точка старта</param>
        /// <param name="finish">Точка финиша</param>
        /// <param name="view">Класс отрисовки</param>
        /// <param name="fromStart">Начинать генерацию с начала (если true)</param>
        /// <param name="feature">Параметр для особой отрисовки лабиринта</param>
        /// <param name="bitmap">Используется ли отрисовка в файл</param>
        public MazeObject(int width, int height, Point start, Point finish, double blackProb, double whiteProb, bool fromStart, bool bitmap, int feature, int sleep, View view, Random random)
        {
            Maze = new int[width * 2 + 1, height * 2 + 1];
            startpoint = start;
            finishpoint = finish;
            this.whiteProb = whiteProb;
            this.blackProb = blackProb;
            Sleep = sleep;
            this.fromStart = fromStart;
            FeatureCode = feature;
            IsSolutionFound = false;

            this.random = random;
            this.view = view;

            this.bitmap = bitmap;
        }

        public void Clear()
        {
            //Solver = null;
            Maze = null;
        }

        /// <summary>
        ///  Генерация лабиринта методом рекурсивного бэктрекинга
        /// </summary>
        public void GenerateMazeWithRecursiveBacktracker()
        {
            BacktrackingGenerator generator = new BacktrackingGenerator(Maze, startpoint, finishpoint, view, FeatureCode, Sleep, random);
            generator.FillMazeArray(blackProb);
            generator.Generate(fromStart, whiteProb);
        }

        /// <summary>
        /// Генерация лабиринта методом Hunt-And-Kill
        /// </summary>
        public void GenerateMazeWithHuntAndKill()
        {
            HuntAndKillGenerator generator = new HuntAndKillGenerator(Maze, startpoint, finishpoint, view, FeatureCode, Sleep, random);
            generator.FillMazeArray(blackProb);
            generator.Generate(fromStart, whiteProb);
        }

        /// <summary>
        /// Генерация лабиринта методом Eller
        /// </summary>
        public void GenerateMazeWithEller()
        {
            EllerGenerator generator = new EllerGenerator(Maze, startpoint, finishpoint, view, FeatureCode, Sleep, random);
            generator.FillMazeArray(blackProb);
            generator.Generate(fromStart, whiteProb);
        }

        /// <summary>
        /// Метод левых поворотов
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public void LeftRotateSolver()
        {
            //ParamsUpdate();
            if (solver != null)
                solver.Clear();
            solver = new LeftRotateSolver(Maze, startpoint, finishpoint, view, FeatureCode, Sleep, bitmap);
            solver.Solve();
            IsSolutionFound = solver.Result;
        }

        /// <summary>
        /// Метод правых поворотов
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public void RightRotateSolver()
        {
            //ParamsUpdate();
            if (solver != null)
                solver.Clear();
            solver = new RightRotateSolver(Maze, startpoint, finishpoint, view, FeatureCode, Sleep, bitmap);
            solver.Solve();
            IsSolutionFound = solver.Result;
        }

        /// <summary>
        /// Метод случайных поворотов (сочетание левых и правых поворотов)
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public void RandomSolver()
        {
            if (solver != null)
                solver.Clear();
            solver = new RandomRotateSolver(Maze, startpoint, finishpoint, view, FeatureCode, Sleep, bitmap);
            solver.Solve();
            IsSolutionFound = solver.Result;
        }
    }
}