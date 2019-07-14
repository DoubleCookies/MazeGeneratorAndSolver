using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace MazeGenerator
{
    public partial class MazeForm : Form
    {
        Stopwatch timer = new Stopwatch(); // Таймер общего назначения
        Stopwatch gameTimer = new Stopwatch(); // Таймер для игрока
        View view; // Класс отрисовки
        Graphics drawingPicturebox; // Объект, на котором может производиться отрисовка
        MazeMainClass maze; // Основной объект лабиринта
        delegate int SolverSelect(); // Делагет для методов-решателей
        ProgressForm progressform = new ProgressForm();

        int width; // Ширина лабиринта
        int height; // Высота лабиринта
        int sleep; // Тайм-аут отрисовки
        bool black = false; // Использование генерации пустых мест
        bool white = false; // Убирать ли часть стен
        bool gameInit = false; // Решает ли пользователь лабиринт


        public MazeForm()
        {
            InitializeComponent();
            pictureBoxLabirint.Width = this.Size.Width - 360;
            pictureBoxLabirint.Height = this.Size.Height - 80;
            drawingPicturebox = pictureBoxLabirint.CreateGraphics();
            view = new View(drawingPicturebox);
        }

        private void ButtonGenMaze_Click(object sender, EventArgs e)
        {
            gameInit = false;
            Thread thr = new Thread(RunBacktracker);
            SwitchButtonStatus(false);
            thr.Start();
            //RunBacktracker();
        }

        private void buttongenMazeHunt_Click(object sender, EventArgs e)
        {
            gameInit = false;
            Thread thr = new Thread(RunHunt);
            SwitchButtonStatus(false);
            thr.Start();
            //RunHunt();
        }

        /// <summary>
        /// Метод для запуска генерации алгоритма методом бэктрекинга
        /// </summary>
        private void RunBacktracker()
        {
            Run(false);
        }

        /// <summary>
        /// Метод для запуска генерации алгоритма методом Hunt-And-Kill
        /// </summary>
        private void RunHunt()
        {
            Run(true);
        }

        /// <summary>
        /// Генерация лабиринта и вывод информации о ней
        /// </summary>
        /// <param name="isHuntMethod">Используется ли метод Hunt-And-Kill</param>
        private void Run(bool isHuntMethod)
        {
            drawingPicturebox = pictureBoxLabirint.CreateGraphics();
            view = new View(drawingPicturebox);
            bool success;
            timer.Restart();
            success = MazeOp(false);
            if (success)
            {
                view.MazeDraw(maze.GetMaze.GetLength(0), maze.GetMaze.GetLength(1), pictureBoxLabirint.Width, pictureBoxLabirint.Height);
                if (black)
                {
                    view.DrawBlackPoints(maze.Generator.BlackPoints);
                    maze.Generator.BlackClear();
                }
                if (isHuntMethod)
                    maze.MazeGenerateHuntAndKill();
                else
                    maze.MazeGenerateRec();
                timer.Stop();
                double time = timer.ElapsedMilliseconds;
                richTextBoxInfo.AppendText("Затраченное время на генерацию лабиринта размером " 
                    + width + " x " + height + " = " + time/1000.0 + " с.\r\n");
            }
            SwitchButtonStatus(true);
        }

        private void buttonSolverStart_Click(object sender, EventArgs e)
        {
            timer.Restart();
            if (radioButtonLR.Checked)
            {
                SolverSelect s = new SolverSelect(maze.LeftRotateSolver);
                SolverRunner(s, "левых поворотов");
            }
            else if (radioButtonRR.Checked)
            {
                SolverSelect s = new SolverSelect(maze.RightRotateSolver);
                SolverRunner(s, "правых поворотов");
            }
            else
            {
                SolverSelect s = new SolverSelect(maze.RandomSolver);
                SolverRunner(s, "случайных поворотов");
            }
        }

        /// <summary>
        /// Метод для запуска решателя лабиринтов
        /// </summary>
        /// <param name="s">Делегат, передающий метод</param>
        /// <param name="method">Название метода</param>
        private void SolverRunner(SolverSelect s, string method)
        {
            timer.Restart();
            maze.Sleep = int.Parse(textBoxSleep.Text);
            if (checkBoxFeatureUse.Checked)
                maze.FeatureCode = GetFeatureCode();
            else
                maze.FeatureCode = 0;
            int steps;
            steps = s(); //запуск решателя
            double time = timer.ElapsedMilliseconds;
            if (maze.Result)
                richTextBoxInfo.AppendText("Затраченное время на решение лабиринта размером " + width + " x " + height 
                    + " методом " + method + " = " + time/1000.0 + " с.\r\nКоличество шагов - " + steps + ".\r\n");
            else
                richTextBoxInfo.AppendText("Лабиринт размером " + width + " x " 
                    + height + " не содержит решения. Затраченное время - " + time/1000.0 + " с.\r\n");
        }

        /// <summary>
        /// Метод меняет состояние некоторых кнопок на форме (вкл/выкл)
        /// </summary>
        /// <param name="status">Статус кнопок: true - включить</param>
        private void SwitchButtonStatus(bool status)
        {
            buttonGenMaze.Enabled = status;
            buttonSolverStart.Enabled = status;
            buttonGame.Enabled = status;
            buttongenMazeHunt.Enabled = status;
        }

        private void ButtonGame_Click(object sender, EventArgs e)
        {
            if (!gameInit)
            {
                maze.GameInit();
                gameInit = true;
                gameTimer.Restart();
            }
            textBoxWidth.Focus();
            buttonGame.Enabled = false;
        }

        private void ButtonGenPicture_Click(object sender, EventArgs e)
        {
            bool success;
            int size = 0;
            try
            {
                size = int.Parse(textBoxSize.Text);
            }
            catch
            {
                MessageBox.Show("Введите нормальный размер для отрисовки!");
                return;
            }
            success = MazeOp(true);
            if (success)
            {
                view.MazeDrawBitmap(maze.GetMaze, size);
                if (black)
                {
                    view.DrawBlackPoints(maze.Generator.BlackPoints);
                    maze.Generator.BlackClear();
                }
                maze.MazeGenerateRec();
                if (checkBoxWithSolution.Checked)
                    SolverSelection();
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    Filter = "Png Files|*.png|All Files (*.*)|*.*"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                    view.bit.Save(dialog.FileName, ImageFormat.Png);
                view.Dispose();
                drawingPicturebox.Dispose();
            }
        }

        private void ButtonGenerateBatch_Click(object sender, EventArgs e)
        {
            bool success;
            int size = 0;
            int count = 0;
            try
            {
                size = int.Parse(textBoxSize.Text);
                count = int.Parse(textBoxCount.Text);
            }
            catch
            {
                MessageBox.Show("Введите нормальный размер для отрисовки!");
                return;
            }

            progressform.Init(count);
            success = MazeOp(true);
            if (success)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Выберите папку для сохранения набора изображений";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    progressform.Show();
                    for (int i = 0; i < count; i++)
                    {
                        view.MazeDrawBitmap(maze.GetMaze, size);
                        if (black)
                        {
                            view.DrawBlackPoints(maze.Generator.BlackPoints);
                            maze.Generator.BlackClear();
                        }
                        //pictureBoxLabirint.Image = view.bit;
                        maze.MazeGenerateRec();
                        view.bit.Save(dialog.SelectedPath + "/maze" + i + ".png", ImageFormat.Png);
                        if (checkBoxWithSolution.Checked)
                        {
                            SolverSelection();
                            view.bit.Save(dialog.SelectedPath + "/maze" + i + "solved.png", ImageFormat.Png);
                        }
                        progressform.ProgressBarUpdate();
                        Thread.Sleep(25);
                        view.Dispose();
                        drawingPicturebox.Dispose();
                        MazeOp(true);
                    }
                    progressform.Hide();
                }
            }
        }

        /// <summary>
        /// Метод обработки характеристик для генерации лабиринта
        /// </summary>
        /// <param name="isBitmapUsed">Указывает, производится ли отрисовка на форме или в битмапе</param>
        /// <returns>Возвращает успешность операции</returns>
        private bool MazeOp(bool isBitmapUsed)
        {
            maze = null;
            drawingPicturebox = pictureBoxLabirint.CreateGraphics();
            view = new View(drawingPicturebox);
            bool fromStart;
            double prob = 0;
            double whiteProb = 0;
            int featureCode = 0;
            try
            {
                width = int.Parse(textBoxWidth.Text);
                height = int.Parse(textBoxHeight.Text);
                sleep = int.Parse(textBoxSleep.Text);
                prob = double.Parse(textBoxEmptyPlacesProb.Text);
                whiteProb = double.Parse(textBoxWhiteSpaceProb.Text);
            }
            catch
            {
                MessageBox.Show("Неверно введены параметры для генерации лабиринта!");
                return false;
            }
            if (width <= 1 || height <= 1)
            {
                MessageBox.Show("Неверно задана размерность!");
                return false;
            }
            Point startpoint = new Point(1, 1);
            Point finishpoint = new Point(width * 2 - 1, height * 2 - 1);
            try
            {
                if (checkBoxStart.Checked)
                    startpoint = new Point(int.Parse(textBoxStartX.Text) * 2 + 1, int.Parse(textBoxStartY.Text) * 2 + 1);
                if (checkBoxFinish.Checked)
                    finishpoint = new Point(int.Parse(textBoxEndX.Text) * 2 + 1, int.Parse(textBoxEndY.Text) * 2 + 1);
                black = checkBoxBlack.Checked;
                fromStart = checkBoxFromBegin.Checked;
                if (checkBoxFeatureUse.Checked)
                    featureCode = GetFeatureCode();
                white = checkBoxWhiteSpaces.Checked;
                if (!isBitmapUsed)
                    maze = new MazeMainClass(width, height, startpoint, finishpoint, black, prob, white, whiteProb, fromStart, false, featureCode, sleep, view);
                else
                    maze = new MazeMainClass(width, height, startpoint, finishpoint, black, prob, white, whiteProb, fromStart, true, featureCode, 0, view);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Слишком большой размер массива для лабиринта!");
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Неверно введены параметры для начальной и конечной точек!");
                return false;
            }
            if (!isBitmapUsed)
            {
                if (width * 2 + 1 > pictureBoxLabirint.Width || height * 2 + 1 > pictureBoxLabirint.Height)
                {
                    MessageBox.Show("Лабиринт с заданными размерами не помещается на форме - сделайте его меньше!");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Метод получения кода особенности отрисовки
        /// </summary>
        /// <returns>Возвращает числовое представление кода</returns>
        private int GetFeatureCode()
        {
            if (radioButtonFeatureChaos.Checked)
                return 1;
            if (radioButtonFeatureRed.Checked)
                return 2;
            if (radioButtonFeatureGreen.Checked)
                return 3;
            if (radioButtonFeatureBlue.Checked)
                return 4;
            if (radioButtonFeatureYellow.Checked)
                return 23;
            if (radioButtonFeatureTurquoise.Checked)
                return 34;
            if (radioButtonFeatureViolet.Checked)
                return 42;
            if (radioButtonFeatureDarkStyle.Checked)
                return 48;
            if (radioButtonFeatureLightStyle.Checked)
                return 49;
            else
                return 50;
        }

        /// <summary>
        /// Выбор метода для решения лабиринта в случае отрисовки лабиринта в изображении
        /// </summary>
        private void SolverSelection()
        {
            if (radioButtonLR.Checked)
                maze.LeftRotateSolver();
            if (radioButtonRR.Checked)
                maze.RightRotateSolver();
            if (radioButtonRandR.Checked)
                maze.RandomSolver();
        }

        private void MazeForm_SizeChanged(object sender, EventArgs e)
        {
            pictureBoxLabirint.Width = this.Size.Width - 360;
            pictureBoxLabirint.Height = this.Size.Height - 80;
            drawingPicturebox = pictureBoxLabirint.CreateGraphics();
            view = new View(drawingPicturebox);
        }

        private void MazeForm_KeyDown(object sender, KeyEventArgs e)
        { keyDown(sender, e); }

        private void textBoxWidth_KeyDown(object sender, KeyEventArgs e)
        { keyDown(sender, e); }

        private void textBoxHeight_KeyDown(object sender, KeyEventArgs e)
        { keyDown(sender, e); }

        private void textBoxSleep_KeyDown(object sender, KeyEventArgs e)
        { keyDown(sender, e); }

        private void textBoxSize_KeyDown(object sender, KeyEventArgs e)
        { keyDown(sender, e); }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        { keyDown(sender, e); }

        private void textBoxProb_KeyDown(object sender, KeyEventArgs e)
        { keyDown(sender, e); }

        /// <summary>
        /// Обработка нажатия на стрелки во время прохождения лабиринта игроком
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyDown(object sender, KeyEventArgs e)
        {
            if (gameInit)
            {
                switch (e.KeyData)
                {
                    case Keys.Down: { maze.Game(0, 1); break; }
                    case Keys.Up: { maze.Game(0, -1); break; }
                    case Keys.Left: { maze.Game(-1, 0); break; }
                    case Keys.Right: { maze.Game(1, 0); break; }
                }
                if (maze.Finish)
                {
                    gameInit = false;
                    buttonGame.Enabled = true;
                    maze.Finish = false;
                    gameTimer.Stop();
                    richTextBoxInfo.AppendText("Затраченное время на прохождение лабиринта - " 
                        + gameTimer.ElapsedMilliseconds/1000.0 + " с.\r\n");
                    MessageBox.Show("Лабиринт решён!");
                }
            }
        }

        private void checkBoxBlack_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBlack.Checked)
            {
                label10.Visible = true;
                textBoxEmptyPlacesProb.Visible = true;
            }
            else
            {
                label10.Visible = false;
                textBoxEmptyPlacesProb.Visible = false;
            }
        }

        private void checkBoxFeatureUse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFeatureUse.Checked)
                groupBoxDrawFeatures.Visible = true;
            else
                groupBoxDrawFeatures.Visible = false;
        }

        private void checkBoxWhiteSpaces_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWhiteSpaces.Checked)
            {
                label11.Visible = true;
                textBoxWhiteSpaceProb.Visible = true;
            }
            else
            {
                label11.Visible = false;
                textBoxWhiteSpaceProb.Visible = false;            
            }
        }

        private void checkBoxStart_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStart.Checked)
            {
                label6.Visible = true;
                label7.Visible = true;
                textBoxStartX.Visible = true;
                textBoxStartY.Visible = true;
            }
            else
            {
                label6.Visible = false;
                label7.Visible = false;
                textBoxStartX.Visible = false;
                textBoxStartY.Visible = false;
            }
        }

        private void checkBoxFinish_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFinish.Checked)
            {
                label5.Visible = true;
                label4.Visible = true;
                textBoxEndX.Visible = true;
                textBoxEndY.Visible = true;
            }
            else
            {
                label5.Visible = false;
                label4.Visible = false;
                textBoxEndX.Visible = false;
                textBoxEndY.Visible = false;
            }
        }
    }
}
