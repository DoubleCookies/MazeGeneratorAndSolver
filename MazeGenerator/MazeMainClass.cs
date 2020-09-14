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
        public bool IsMazeFinished { get; set; }
        public bool Result { get; set; }
        public int[,] Maze { get; }
        public int Sleep { get; set; }
        public int FeatureCode { get; set; }

        private readonly View view; // ссылка на класс рисования
        private Point startpoint; // Начальная точка
        private Point finishpoint; // Конечная точка

        private readonly double blackProb; //Вероятность появления доп. стен
        private readonly double whiteProb; //Вероятность убрать стену

        private Point current; // Текущая точка

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
            this.view = view;
            Sleep = sleep;
            this.fromStart = fromStart;
            FeatureCode = feature;
            view.SetStartAndFinish(startpoint, finishpoint);
            Result = false;
            IsMazeFinished = false;
            Generator = new Generators(Maze, startpoint, finishpoint, view, FeatureCode, sleep, random);
            Generator.FillMazeArray(blackProb > 0, blackProb);
            Solver = new Solvers(Maze, startpoint, finishpoint, view, feature, sleep, bitmap);
        }

        /// <summary>
        ///  Генерация лабиринта методом рекурсивного бэктрекинга
        /// </summary>
        public void GenerateMazeWithRecursiveBacktracker()
        {
            Generator.BackTrackMazeGenerate(fromStart, blackProb, whiteProb);
        }

        /// <summary>
        /// Генерация лабиринта методом Hunt-And-Kill
        /// </summary>
        public void GenerateMazeWithHuntAndKill()
        {
            Generator.HuntAndKillMazeGenerate(fromStart, blackProb, whiteProb);
        }

        /// <summary>
        /// Метод левых поворотов
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public int LeftRotateSolver()
        {
            ParamsUpdate();
            int stepsCount = Solver.LeftRightRotateSolver(true);
            Result = Solver.Result;
            return stepsCount;
        }

        /// <summary>
        /// Метод правых поворотов
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public int RightRotateSolver()
        {
            ParamsUpdate();
            int stepsCount = Solver.LeftRightRotateSolver(false);
            Result = Solver.Result;
            return stepsCount;
        }

        /// <summary>
        /// Метод случайных поворотов (сочетание левых и правых поворотов)
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public int RandomSolver()
        {
            ParamsUpdate();
            int stepsCount = Solver.RandomSolver();
            Result = Solver.Result;
            return stepsCount;
        }

        /// <summary>
        /// Обновление параметров для изменения отрисовки решения лабиринта
        /// </summary>
        private void ParamsUpdate()
        {
            Solver.FeatureCode = FeatureCode;
            Solver.Sleep = Sleep;
        }
        /// <summary>
        /// Инициация игры - поиска решения вручную
        /// </summary>
        public void GameInit()
        {
            MazeClear();
            current = startpoint;
            view.DrawCircle(startpoint, Color.Violet);
            view.DrawCircle(current, Color.Red);
            view.DrawCircle(finishpoint, Color.LimeGreen);
        }

        /// <summary>
        /// Метод обработки "игры"
        /// </summary>
        /// <param name="x">Направление движения по оси Х</param>
        /// <param name="y">Направление движения по оси Y</param>
        public void Game(int x, int y)
        {
            Point next = new Point(current.X + x, current.Y + y);
            if (Maze[next.X, next.Y] != 0)
            {
                if (current != startpoint)
                    view.DrawCircle(current, Color.White);
                else
                    view.DrawCircle(current, Color.Violet);
                current = next;
                if (current == finishpoint)
                    IsMazeFinished = true;
                view.DrawCircle(current, Color.Red);
            }
        }

        /// <summary>
        /// Очистка лабиринта
        /// </summary>
        private void MazeClear()
        {
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    if (Maze[i, j] != 0)
                        Maze[i, j] = 1;
                }
            }
        }
    }
}