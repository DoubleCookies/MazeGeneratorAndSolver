using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading;
using MazeGenerator.MazeGenerators;
using MazeGenerator.MazeSolvers;

namespace MazeGenerator
{
    public class MazeMainClass
    {
        private Generators generator; //Ссылка на генераторы лабиринта
        public Generators Generator
        {
            get { return generator; }
            set { generator = value; }
        }
        private Solvers solver; // Ссылка на решатели лабиринта
        public Solvers Solver
        {
            get { return solver; }
            set { solver = value; }
        }
        PointsFounders founders;

        bool gameFinished; //завершён ли поиск пути вручную
        public bool Finish
        {
            get { return gameFinished; }
            set { gameFinished = value; }
        }
        
        View view; // ссылка на класс рисования
        Point startpoint; // Начальная точка
        Point finishpoint; // Конечная точка
        List<Point> points; // Список точек при построении лабиринта
        List<Point> allPoints; // Список всех посещённых точек при поиске решения (нужно только для режима "прямого эфира"

        bool whiteSpaces; //Убирать ли часть стен
        double whiteProb; //Вероятность убрать стену

        Point current; // Текущая точка
        int sleep; // Время для "сна" потока для задержки отрисовки
        public int Sleep
        {
            set { sleep = value; }
        }

        bool fromStart; // Генерация лабиринта со старта или с финиша
        bool result; // Результат поиска пути из начальной точки в конечную
        public bool Result
        {
            get { return result; }
            set { result = value; }
        }

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
        /// <param name="blackUse">Использование генерации пустых мест</param>
        /// <param name="blackProb">Вероятность генерации пустых мест</param>
        /// <param name="start">Точка старта</param>
        /// <param name="finish">Точка финиша</param>
        /// <param name="view">Класс отрисовки</param>
        /// <param name="fromStart">Начинать генерацию с начала (если true)</param>
        /// <param name="feature">Параметр для особой отрисовки лабиринта</param>
        /// <param name="bitmap">Используется ли отрисовка в файл</param>
        public MazeMainClass(int width, int height, Point start, Point finish, bool blackUse, double blackProb, bool white, double whiteProb, bool fromStart, bool bitmap, int feature, int sleep, View view)
        {
            founders = new PointsFounders();
            mazeArray = new int[width * 2 + 1, height * 2 + 1];
            points = new List<Point>();
            allPoints = new List<Point>();
            startpoint = start;
            finishpoint = finish;
            this.whiteProb = 1 - whiteProb;
            this.view = view;
            this.sleep = sleep;
            this.fromStart = fromStart;
            whiteSpaces = white;
            featureCode = feature;
            view.SetStartAndFinish(startpoint, finishpoint);
            result = false;
            gameFinished = false;
            generator = new Generators(mazeArray, startpoint, finishpoint, founders, view, featureCode, sleep);
            Generator.mazeFill(blackUse, 1 - blackProb);
            solver = new Solvers(mazeArray, startpoint, finishpoint, view, feature, sleep, bitmap);
        }

        /// <summary>
        ///  Генерация лабиринта методом рекурсивного бэктрекинга
        /// </summary>
        public void MazeGenerateRec()
        {
            Generator.BackTrackMazeGenerate(fromStart, whiteSpaces, whiteProb);
        }

        /// <summary>
        /// Генерация лабиринта методом Hunt-And-Kill
        /// </summary>
        public void MazeGenerateHuntAndKill()
        {
            Generator.HuntAndKillMazeGenerate(fromStart, whiteSpaces, whiteProb);
        }

        /// <summary>
        /// Метод левых поворотов
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public int LeftRotateSolver()
        {
            ParamsUpdate();
            int stepsCount = solver.LeftRightRotateSolver(true);
            Result = solver.Result;
            return stepsCount;
        }

        /// <summary>
        /// Метод правых поворотов
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public int RightRotateSolver()
        {
            ParamsUpdate();
            int stepsCount = solver.LeftRightRotateSolver(false);
            Result = solver.Result;
            return stepsCount;
        }

        /// <summary>
        /// Метод случайных поворотов (сочетание левых и правых поворотов)
        /// </summary>
        /// <returns>Возвращает количество шагов для прохождения лабиринта</returns>
        public int RandomSolver()
        {
            ParamsUpdate();
            int stepsCount = solver.RandomSolver();
            Result = solver.Result;
            return stepsCount;
        }

        /// <summary>
        /// Обновление параметров для изменения отрисовки решения лабиринта
        /// </summary>
        private void ParamsUpdate()
        {
            solver.FeatureCode = featureCode;
            solver.Sleep = sleep;
        }
        /// <summary>
        /// Инициация игры - поиска решения вручную
        /// </summary>
        public void GameInit()
        {
            MazeRouteClear();
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
                    gameFinished = true;
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

        /// <summary>
        /// Очистка пути после поиска решения
        /// </summary>
        private void MazeRouteClear()
        {
            for (int i = 0; i < allPoints.Count; i++)
            {
                if ((allPoints[i].X != startpoint.X || allPoints[i].Y != startpoint.Y) 
                    && (allPoints[i].X != finishpoint.X || allPoints[i].Y != finishpoint.Y))
                    view.DrawChange(allPoints[i], Color.White);
            }
        }
    }
}