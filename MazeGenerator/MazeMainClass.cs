using MazeGenerator.MazeGenerators;
using MazeGenerator.MazeSolvers;
using System;
using System.Drawing;

namespace MazeGenerator
{
    public class MazeMainClass
    {
        private Generators Generator { get; set; }
        private Solvers Solver { get; set; }
        public bool Result { get; set; }
        public int[,] Maze { get; set; }
        public int Sleep { get; set; }
        public int FeatureCode { get; set; }

        private Point startpoint; // Начальная точка
        private Point finishpoint; // Конечная точка

        private readonly double blackProb; //Вероятность появления доп. стен
        private readonly double whiteProb; //Вероятность убрать стену

        private readonly bool fromStart; // Генерация лабиринта со старта или с финиша


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
        public MazeMainClass(int width, int height, Point start, Point finish, double blackProb, double whiteProb, bool fromStart, bool bitmap, int feature, int sleep, View view, Random random)
        {
            Maze = new int[width * 2 + 1, height * 2 + 1];
            startpoint = start;
            finishpoint = finish;
            this.whiteProb = whiteProb;
            this.blackProb = blackProb;
            Sleep = sleep;
            this.fromStart = fromStart;
            FeatureCode = feature;
            Result = false;
            Generator = new Generators(Maze, startpoint, finishpoint, view, FeatureCode, sleep, random);
            Generator.FillMazeArray(blackProb);
            Solver = new Solvers(Maze, startpoint, finishpoint, view, feature, sleep, bitmap);
        }

        public void Clear() {
            Generator = null;
            Solver = null;
            Maze = null;
        }

        /// <summary>
        ///  Генерация лабиринта методом рекурсивного бэктрекинга
        /// </summary>
        public void GenerateMazeWithRecursiveBacktracker()
        {
            Generator.BackTrackMazeGenerate(fromStart, whiteProb);
        }

        /// <summary>
        /// Генерация лабиринта методом Hunt-And-Kill
        /// </summary>
        public void GenerateMazeWithHuntAndKill()
        {
            Generator.HuntAndKillMazeGenerate(fromStart, whiteProb);
        }

        /// <summary>
        /// Метод левых поворотов
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public void LeftRotateSolver()
        {
            ParamsUpdate();
            Solver.LeftRightRotateSolver(true);
            Result = Solver.Result;
        }

        /// <summary>
        /// Метод правых поворотов
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public void RightRotateSolver()
        {
            ParamsUpdate();
            Solver.LeftRightRotateSolver(false);
            Result = Solver.Result;
        }

        /// <summary>
        /// Метод случайных поворотов (сочетание левых и правых поворотов)
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public void RandomSolver()
        {
            ParamsUpdate();
            Solver.RandomSolver();
            Result = Solver.Result;
        }

        /// <summary>
        /// Обновление параметров для изменения отрисовки решения лабиринта
        /// </summary>
        private void ParamsUpdate()
        {
            Solver.FeatureCode = FeatureCode;
            Solver.Sleep = Sleep;
        }
    }
}