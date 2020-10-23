using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeGenerator
{
    public class View
    {
        private static readonly Color defaultBlack = Color.FromArgb(60, 60, 60);
        private static readonly Color defaultWhite = Color.FromArgb(240, 240, 240);

        // Набор кистей
        private readonly Brush brushBlack = new SolidBrush(defaultBlack);
        private readonly Brush brushWhite = new SolidBrush(Color.White);
        private readonly Brush brushLimeGreen = new SolidBrush(Color.LimeGreen);
        private readonly Brush brushViolet = new SolidBrush(Color.Violet);

        private Point start;
        private Point finish;
        private int pixelSize;
        private int mazeWidth;
        private int mazeHeight;
        private readonly Random rnd = new Random();

        // Объект для отрисовки
        public Graphics GraphicsObject { get; set; }
        // Битмап
        public Bitmap MazeBitmap { get; set; }

        /// <summary>
        /// Конструктор, принимающий объект для рисования
        /// </summary>
        /// <param name="draw">Объект, на котором производится отрисовка</param>
        public View(Graphics draw)
        {
            GraphicsObject = draw;
        }


        /// <summary>
        /// Конструктор, принимающий объект для рисования, а также начальную и конечную точку лабиринта
        /// </summary>
        /// <param name="draw">Объект, на котором производится отрисовка</param>
        /// <param name="start">Начальная точка лабиринта</param>
        /// <param name="finish">Конечная точка лабиринта</param>
        public View(Graphics draw, Point start, Point finish)
        {
            GraphicsObject = draw;
            this.start = start;
            this.finish = finish;
        }

        /// <summary>
        /// Метод изначальной отрисовки лабиринта
        /// </summary>
        /// <param name="mazewidth">Ширина лабиринта</param>
        /// <param name="mazeheight">Высота лабиринта</param>
        /// <param name="pictureboxWidth">Ширина picturebox'a</param>
        /// <param name="pictureboxHeight">Высота picturebox'a</param>
        public void DrawMazeInitState(int mazewidth, int mazeheight, int pictureboxWidth, int pictureboxHeight)
        {
            GraphicsObject.Clear(defaultWhite);
            mazeWidth = mazewidth;
            mazeHeight = mazeheight;
            pixelSize = Math.Min(pictureboxWidth / mazeWidth, pictureboxHeight / mazeHeight);
            FillMazePicture();
        }

        /// <summary>
        /// Отрисовка изменений при построении лабиринта
        /// </summary>
        /// <param name="point">Точка изменения</param>
        /// <param name="color">Цвет изменения</param>
        public void DrawChange(Point point, Color color)
        {
            if (IsNotStartOrFinishPoint(point))
            {
                SolidBrush brush = new SolidBrush(color);
                GraphicsObject.FillRectangle(brush, point.X * pixelSize, point.Y * pixelSize, pixelSize, pixelSize);
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
            Color newColor;
            switch (code)
            {
                case 1:
                    {// Почти полный рандом
                        newColor = Color.FromArgb(rnd.Next(30, 256), rnd.Next(30, 256), rnd.Next(30, 256));
                        break;
                    }
                case 2:
                    {// Красный
                        newColor = Color.FromArgb(rnd.Next(190, 256), rnd.Next(60, 120), rnd.Next(80, 150));
                        break;
                    }
                case 3:
                    {// Зелёный
                        newColor = Color.FromArgb(rnd.Next(80, 120), rnd.Next(200, 250), rnd.Next(80, 150));
                        break;
                    }
                case 4:
                    {// Синий
                        newColor = Color.FromArgb(rnd.Next(50, 130), rnd.Next(60, 130), rnd.Next(190, 256));
                        break;
                    }
                case 50:
                    {// 50 оттенков серого (orly)
                        int a = rnd.Next(150, 200);
                        newColor = Color.FromArgb(a, a, a);
                        break;
                    }
                case 23:
                    {// Желтый
                        newColor = Color.FromArgb(rnd.Next(180, 230), rnd.Next(180, 240), rnd.Next(40, 120));
                        break;
                    }
                case 34:
                    {// Бирюзовый
                        newColor = Color.FromArgb(rnd.Next(40, 120), rnd.Next(175, 250), rnd.Next(180, 230));
                        break;
                    }
                case 42:
                    {// Фиолетовый
                        newColor = Color.FromArgb(rnd.Next(170, 220), rnd.Next(40, 120), rnd.Next(175, 250));
                        break;
                    }
                case 48:
                    {// Тёмная цветовая гамма
                        newColor = Color.FromArgb(rnd.Next(80, 140), rnd.Next(80, 140), rnd.Next(80, 140));
                        break;
                    }
                case 49:
                    {// Светлая цветовая гамма
                        newColor = Color.FromArgb(rnd.Next(190, 256), rnd.Next(190, 256), rnd.Next(190, 256));
                        break;
                    }
                default:
                    {
                        newColor = Color.FromArgb(255, 170, 102);
                        break;
                    }
            }
            DrawChange(change, newColor);
        }

        /// <summary>
        /// Отрисовка чёрных квадратов при генерации пустых областей
        /// </summary>
        /// <param name="blackPoints">Список точек</param>
        public void DrawBlackPoints(List<Point> blackPoints)
        {
            for (int i = 0; i < blackPoints.Count; i++)
                DrawChange(blackPoints[i], defaultBlack);
        }

        /// <summary>
        /// Отрисовка лабиринта в bitmap
        /// </summary>
        /// <param name="Maze">Лабиринт</param>
        /// <param name="size">Размер для отрисовки квадрата</param>
        public void InitMazeBitmap(int width, int height, int size)
        {
            mazeWidth = width;
            mazeHeight = height;
            pixelSize = size;
            int w = mazeWidth * pixelSize;
            int h = mazeHeight * pixelSize;
            MazeBitmap = new Bitmap(w, h);
            GraphicsObject = Graphics.FromImage(MazeBitmap);
            GraphicsObject.Clear(defaultWhite);
            FillMazePicture();
        }

        /// <summary>
        /// Метод для стартовой отрисовки лабиринта
        /// </summary>
        private void FillMazePicture()
        {
            // Отрисовка всего поля, старта и финиша
            GraphicsObject.FillRectangle(brushWhite, 0, 0, mazeWidth * pixelSize, mazeHeight * pixelSize);
            GraphicsObject.FillRectangle(brushViolet, pixelSize * start.X, pixelSize * start.Y, pixelSize, pixelSize);
            GraphicsObject.FillRectangle(brushLimeGreen, finish.X * pixelSize, finish.Y * pixelSize, pixelSize, pixelSize);

            //Отрисовка чёрных полос для создания поля
            for (int i = 0; i < mazeHeight; i++)
            {
                if (i % 2 == 0)
                    GraphicsObject.FillRectangle(brushBlack, 0, i * pixelSize, mazeWidth * pixelSize, pixelSize);
            }
            for (int j = 0; j < mazeWidth; j++)
            {
                if (j % 2 == 0)
                    GraphicsObject.FillRectangle(brushBlack, j * pixelSize, 0, pixelSize, mazeHeight * pixelSize);
            }
        }

        /// <summary>
        /// Проверка, является ли точка стартом лабиринта
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>true, если точка не является стартом или финишем лабиринта</returns>
        private bool IsNotStartOrFinishPoint(Point point)
        {
            return (point.X != start.X || point.Y != start.Y) && (point.X != finish.X || point.Y != finish.Y);
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            GraphicsObject.Dispose();
            MazeBitmap.Dispose();
        }
    }
}
