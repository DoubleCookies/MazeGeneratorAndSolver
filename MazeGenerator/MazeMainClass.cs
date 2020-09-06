using System.Collections.Generic;
using System.Drawing;
using MazeGenerator.MazeGenerators;
using MazeGenerator.MazeSolvers;

namespace MazeGenerator
{
    public class MazeMainClass
    {
        public Generators Generator { get; set; }
        public Solvers Solver { get; set; }

        readonly PointsFounders pointsFounders;

        public bool IsMazeFinished { get; set; }

        View view; // ссылка на класс рисования
        Point startpoint; // Начальная точка
        Point finishpoint; // Конечная точка

        readonly double blackProb; //Вероятность появления доп. стен
        readonly double whiteProb; //Вероятность убрать стену

        Point current; // Текущая точка
        int sleep; // Время для "сна" потока для задержки отрисовки
        public int Sleep
        {
            set { sleep = value; }
        }

        bool fromStart; // Генерация лабиринта со старта или с финиша

        public bool Result { get; set; }

        int[,] mazeArray; //Массив, отображающий лабиринт
        public int[,] GetMaze
        {
            get { return mazeArray; }
        }
        int featureCode = 0; //Код для особой отрисовки
        public int FeatureCode
        {
            set { featureCode = value; }
        }

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
        public MazeMainClass(int width, int height, Point start, Point finish, double blackProb, double whiteProb, bool fromStart, bool bitmap, int feature, int sleep, View view)
        {
            pointsFounders = new PointsFounders();
            mazeArray = new int[width * 2 + 1, height * 2 + 1];
            startpoint = start;
            finishpoint = finish;
            this.whiteProb = whiteProb;
            this.blackProb = blackProb;
            this.view = view;
            this.sleep = sleep;
            this.fromStart = fromStart;
            featureCode = feature;
            view.SetStartAndFinish(startpoint, finishpoint);
            Result = false;
            IsMazeFinished = false;
            Generator = new Generators(mazeArray, startpoint, finishpoint, pointsFounders, view, featureCode, sleep);
            Generator.FillMazeArray(blackProb > 0, blackProb);
            Solver = new Solvers(mazeArray, startpoint, finishpoint, view, feature, sleep, bitmap);
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
            Solver.FeatureCode = featureCode;
            Solver.Sleep = sleep;
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
            if (mazeArray[next.X, next.Y] != 0)
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
            for (int i = 0; i < mazeArray.GetLength(0); i++)
            {
                for (int j = 0; j < mazeArray.GetLength(1); j++)
                {
                    if (mazeArray[i, j] != 0)
                        mazeArray[i, j] = 1;
                }
            }
        }
    }
}