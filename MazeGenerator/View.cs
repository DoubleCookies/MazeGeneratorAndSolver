using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeGenerator
{
    public class View
    {
        public Graphics gr; // Объект для отрисовки
        public Bitmap bit; // Битмап
        // Набор кистей
        Brush brushBlack = new SolidBrush(Color.FromArgb(255, 60, 60, 60));
        Brush brushWhite = new SolidBrush(Color.White);
        Brush brushOrange = new SolidBrush(Color.Orange);
        Brush brushLimeGreen = new SolidBrush(Color.LimeGreen);
        Brush brushViolet = new SolidBrush(Color.Violet);
        Point start; // Стартовая тчока
        Point finish; // Конечная точка
        int mult; // Множитель размера одного квадрата отрисовки
        int mazeWidth; // Ширина лабиринта
        int mazeHeight; // Высота лабиринта
        Random rnd = new Random();

        /// <summary>
        /// Конструктор, принимающий объект для рисования
        /// </summary>
        /// <param name="draw">Объект, на котором производится отрисовка</param>
        public View(Graphics draw)
        {
            gr = draw;
        }

        /// <summary>
        /// Метод изначальной отрисовки лабиринта
        /// </summary>
        /// <param name="Maze">Массив лабиринта</param>
        /// <param name="width">Ширина picturebox'a</param>
        /// <param name="height">Высота picturebox'a</param>
        public void MazeDraw(int mazewidth, int mazeheight, int width, int height)
        {
            gr.Clear(Color.FromArgb(255,240,240,240));
            mazeWidth = mazewidth;
            mazeHeight = mazeheight;

            int b1 = width / mazeWidth;
            int b2 = height / mazeHeight;
            mult = Math.Min(b1,b2);
            FillMazePicture();
        }

        /// <summary>
        /// Отрисовка изменений при построении лабиринта
        /// </summary>
        /// <param name="change">Точка изменения</param>
        /// <param name="color">Цвет изменения</param>
        public void DrawChange(Point change, Color color)
        {
            // Отрисовка идёт, если точка не является начальной или конечной
            if (isNotStartPoint(change) && isNotFinishPoint(change))
            {
                SolidBrush brush = new SolidBrush(color);
                gr.FillRectangle(brush, change.X * mult, change.Y * mult, mult, mult);
                brush.Dispose();
            }          
        }
        /// <summary>
        /// Перегруженный метод для "фич" при отрисовке
        /// </summary>
        /// <param name="change">Точка изменения</param>
        /// <param name="code">Код фичи</param>
        public void DrawChange(Point change, int code)
        {

            // Отрисовка идёт, если точка не является начальной или конечной
            if (isNotStartPoint(change) && isNotFinishPoint(change))
            {
                Color newColor;
                SolidBrush brush;
                switch (code)
                {
                    case 1:
                        {// Почти полный рандом
                            newColor = Color.FromArgb(255, rnd.Next(30, 256), rnd.Next(30, 256), rnd.Next(30, 256));
                            brush = new SolidBrush(newColor);
                            break;
                        }
                    case 2:
                        {// Красный
                            newColor = Color.FromArgb(255, rnd.Next(190, 256), rnd.Next(60, 120), rnd.Next(80, 150));
                            brush = new SolidBrush(newColor);
                            break;
                        }
                    case 3:
                        {// Зелёный
                            newColor = Color.FromArgb(255, rnd.Next(80, 120), rnd.Next(200, 250), rnd.Next(80, 150));
                            brush = new SolidBrush(newColor);
                            break;
                        }
                    case 4:
                        {// Синий
                            newColor = Color.FromArgb(255, rnd.Next(50, 130), rnd.Next(60, 130), rnd.Next(190, 256));
                            brush = new SolidBrush(newColor);
                            break;
                        }
                    case 50:
                        {// 50 оттенков серого (wat)
                            int a = rnd.Next(150, 200);
                            newColor = Color.FromArgb(255, a, a, a);
                            brush = new SolidBrush(newColor);
                            break; }
                    case 23:
                        {// Желтый
                            newColor = Color.FromArgb(255, rnd.Next(180, 230), rnd.Next(180, 240), rnd.Next(40, 120));
                            brush = new SolidBrush(newColor);
                            break;
                        }
                    case 34:
                        {// Бирюзовый
                            newColor = Color.FromArgb(255, rnd.Next(40, 120), rnd.Next(175, 250), rnd.Next(180, 230));
                            brush = new SolidBrush(newColor);
                            break;
                        }
                    case 42:
                        {// Фиолетовый
                            newColor = Color.FromArgb(255, rnd.Next(170, 220), rnd.Next(40, 120), rnd.Next(175, 250));
                            brush = new SolidBrush(newColor);
                            break;
                        }
                    case 48:
                        {// тёмная цветовая гамма
                            newColor = Color.FromArgb(255, rnd.Next(80, 140), rnd.Next(80, 140), rnd.Next(80, 140));
                            brush = new SolidBrush(newColor);
                            break;
                        }
                    case 49:
                        {// Светлая цветовая гамма
                            newColor = Color.FromArgb(255, rnd.Next(190, 256), rnd.Next(190, 256), rnd.Next(190, 256));
                            brush = new SolidBrush(newColor);
                            break;
                        }
                    default:
                        {
                            newColor = Color.FromArgb(255, 255, 170, 102);
                            brush = new SolidBrush(newColor);
                            break;
                        }
                }
                gr.FillRectangle(brush, change.X * mult, change.Y * mult, mult, mult);
                brush.Dispose();
               
            }
        }

        /// <summary>
        /// Отрисовка чёрных квадратов при генерации пустых областей
        /// </summary>
        /// <param name="black">Список точек</param>
        public void DrawBlackPoints(List<Point> black)
        {
            for (int i = 0; i < black.Count; i++)
                DrawChange(black[i], Color.FromArgb(255, 60, 60, 60));
        }

        /// <summary>
        /// Отрисовка белых квадратов при генерации новых проходов
        /// </summary>
        /// <param name="white">Список точек</param>
        public void DrawWhitePoints(List<Point> white)
        {
            for (int i = 0; i < white.Count; i++)
                DrawChange(white[i], Color.White);
        }

        /// <summary>
        /// Метод, задающий точки старта и финиша
        /// </summary>
        /// <param name="st">Точка старта</param>
        /// <param name="fin">Точка финиша</param>
        public void SetStartAndFinish(Point st, Point fin)
        {
            start = st;
            finish = fin;
        }

        /// <summary>
        /// Отрисовка лабиринта в bitmap
        /// </summary>
        /// <param name="Maze">Лабиринт</param>
        /// <param name="size">Размер для отрисовки квадрата</param>
        public void MazeDrawBitmap(int[,] Maze, int size)
        {
            mazeWidth = Maze.GetLength(0);
            mazeHeight = Maze.GetLength(1);
            mult = size;
            int w = mazeWidth * mult;
            int h = mazeHeight * mult;
            bit = new Bitmap(w, h);
            gr = Graphics.FromImage(bit);
            gr.Clear(Color.FromArgb(255, 240, 240, 240));
            FillMazePicture();
        }

        private void FillMazePicture()
        {
            // Отрисовка всего поля, старта и финиша
            gr.FillRectangle(brushWhite, 0, 0, mazeWidth * mult, mazeHeight * mult);
            gr.FillRectangle(brushViolet, mult * start.X, mult * start.Y, mult, mult);
            gr.FillRectangle(brushLimeGreen, finish.X * mult, finish.Y * mult, mult, mult);

            //Отрисовка чёрных полос для создания поля
            for (int i = 0; i < mazeHeight; i++)
            {
                if (i % 2 == 0)
                    gr.FillRectangle(brushBlack, 0, i * mult, mazeWidth * mult, mult);
            }
            for (int j = 0; j < mazeWidth; j++)
            {
                if (j % 2 == 0)
                    gr.FillRectangle(brushBlack, j * mult, 0, mult, mazeHeight * mult);
            }
        }

        /// <summary>
        /// Отрисовка круга при самостоятельном прохождении лабиринта
        /// </summary>
        /// <param name="change">Точка для отрисовки</param>
        /// <param name="color">Цвет точки</param>
        public void DrawCircle(Point change, Color color)
        {
            SolidBrush brush = new SolidBrush(color);
            gr.FillEllipse(brush, change.X * mult, change.Y * mult, mult, mult);
            brush.Dispose();
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            gr.Dispose();
            bit.Dispose();
        }

        private bool isNotStartPoint(Point change)
        {
            return change.X != start.X || change.Y != start.Y;
        }

        private bool isNotFinishPoint(Point change)
        {
            return change.X != finish.X || change.Y != finish.Y;
        }
    }
}
