using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeGenerator
{
    //TODO: Может сделать статическим?
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
        /// <param name="pixelSize">Размер пикселя</param>
        public void DrawMazeInitState(int mazeWidth, int mazeHeight, int pixelSize)
        {
            GraphicsObject.Clear(defaultWhite);
            this.mazeWidth = mazeWidth;
            this.mazeHeight = mazeHeight;
            this.pixelSize = pixelSize;
            FillMazePicture();
        }

        /// <summary>
        /// Отрисовка лабиринта в bitmap
        /// </summary>
        /// <param name="Maze">Лабиринт</param>
        /// <param name="size">Размер для отрисовки квадрата</param>
        public void InitMazeBitmap(int mazeWidth, int mazeHeight, int pixelSize)
        {
            this.mazeWidth = mazeWidth;
            this.mazeHeight = mazeHeight;
            this.pixelSize = pixelSize;
            MazeBitmap = new Bitmap(mazeWidth, mazeHeight);
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
        /// Отрисовка чёрных квадратов при генерации пустых областей
        /// </summary>
        /// <param name="blackPoints">Список точек</param>
        public void DrawBlackPoints(List<Point> blackPoints)
        {
            for (int i = 0; i < blackPoints.Count; i++)
                DrawChange(blackPoints[i], defaultBlack);
        }

        /// <summary>
        /// Перегруженный метод для "фич" при отрисовке
        /// </summary>
        /// <param name="point">Точка, которую перерисовывают</param>
        /// <param name="code">Код, позволяющий определить тип цвета</param>
        public void DrawChange(Point point, int code)
        {
            Color newColor = getColorFromCode(code);
            DrawChange(point, newColor);
        }

        /// <summary>
        /// Отрисовка изменений при построении лабиринта
        /// </summary>
        /// <param name="point">Точка, которую перерисовывают</param>
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
        /// Проверка, является ли точка стартом лабиринта
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>true, если точка не является стартом или финишем лабиринта</returns>
        private bool IsNotStartOrFinishPoint(Point point)
        {
            return (point.X != start.X || point.Y != start.Y) && (point.X != finish.X || point.Y != finish.Y);
        }

        private Color getColorFromCode(int code)
        {
            switch (code)
            {
                case 1:
                    {// Почти полный рандом
                        return Color.FromArgb(rnd.Next(30, 256), rnd.Next(30, 256), rnd.Next(30, 256));
                    }
                case 2:
                    {// Красный
                        return Color.FromArgb(rnd.Next(190, 256), rnd.Next(60, 120), rnd.Next(80, 150));
                    }
                case 3:
                    {// Зелёный
                        return Color.FromArgb(rnd.Next(80, 120), rnd.Next(200, 250), rnd.Next(80, 150));
                    }
                case 4:
                    {// Синий
                        return Color.FromArgb(rnd.Next(50, 130), rnd.Next(60, 130), rnd.Next(190, 256));
                    }
                case 50:
                    {// 50 оттенков серого (orly)
                        int a = rnd.Next(150, 200);
                        return Color.FromArgb(a, a, a);
                    }
                case 23:
                    {// Желтый
                        return Color.FromArgb(rnd.Next(180, 230), rnd.Next(180, 240), rnd.Next(40, 120));
                    }
                case 34:
                    {// Бирюзовый
                        return Color.FromArgb(rnd.Next(40, 120), rnd.Next(175, 250), rnd.Next(180, 230));
                    }
                case 42:
                    {// Фиолетовый
                        return Color.FromArgb(rnd.Next(170, 220), rnd.Next(40, 120), rnd.Next(175, 250));
                    }
                case 48:
                    {// Тёмная цветовая гамма
                        return Color.FromArgb(rnd.Next(80, 140), rnd.Next(80, 140), rnd.Next(80, 140));
                    }
                case 49:
                    {// Светлая цветовая гамма
                        return Color.FromArgb(rnd.Next(190, 256), rnd.Next(190, 256), rnd.Next(190, 256));
                    }
                default:
                    {// персиковый, если код указан неверно
                        return Color.FromArgb(255, 170, 102);
                    }
            }
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
