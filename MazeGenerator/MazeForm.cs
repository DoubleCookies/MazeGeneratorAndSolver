using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class MazeForm : Form
    {
        private readonly ProgressForm progressForm = new ProgressForm(); // Форма отображения прогресса при массовой генерации лабиринта
        private View view; // Класс отрисовки
        private Graphics drawingPicturebox; // Объект, на котором может производиться отрисовка
        private MazeMainClass maze; // Основной объект лабиринта
        private delegate int SolverSelect(); // Делегат для методов-решателей
        private readonly Random random;

        int width; // Ширина лабиринта
        int height; // Высота лабиринта
        int sleep; // Тайм-аут отрисовки
        bool gameInit = false; // Решает ли пользователь лабиринт

        public MazeForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //Для дебага из-за многопоточности
            random = new Random();
        }

        private void buttonMazeGeneration_Click(object sender, EventArgs e)
        {
            gameInit = false;
            Thread thread = new Thread(RunMazeGeneration);
            SwitchButtonsStatus(false);
            thread.Start();
        }

        /// <summary>
        /// Генерация лабиринта и вывод информации о ней
        /// </summary>
        /// <param name="isHuntMethod">Используется ли метод Hunt-And-Kill</param>
        private void RunMazeGeneration()
        {
            drawingPicturebox = pictureBoxLabirint.CreateGraphics();
            view = new View(drawingPicturebox);
            bool isMazeValid = CreateMazeObject(false);
            if (isMazeValid)
            {
                view.DrawMazeInitState(maze.Maze.GetLength(0), maze.Maze.GetLength(1), pictureBoxLabirint.Width, pictureBoxLabirint.Height);
                if (radioButtonHuntAndKill.Checked)
                    maze.GenerateMazeWithHuntAndKill();
                else
                    maze.GenerateMazeWithRecursiveBacktracker();
            }
            SwitchButtonsStatus(true);
        }

        private void buttonSolverStart_Click(object sender, EventArgs e)
        {
            if (radioButtonLR.Checked)
                SolveMaze(new SolverSelect(maze.LeftRotateSolver));
            else if (radioButtonRR.Checked)
                SolveMaze(new SolverSelect(maze.RightRotateSolver));
            else
                SolveMaze(new SolverSelect(maze.RandomSolver));
        }

        /// <summary>
        /// Метод для запуска решателя лабиринтов
        /// </summary>
        /// <param name="solverDelegate">Делегат, передающий метод</param>
        /// <param name="method">Название метода</param>
        private void SolveMaze(SolverSelect solverDelegate)
        {
            int sleep;
            try
            {
                sleep = int.Parse(textBoxSleep.Text);
            }
            catch 
            {
                MessageBox.Show("Введите нормальный размер для отрисовки!");
                return;
            }
            maze.Sleep = sleep;
            maze.FeatureCode = checkBoxFeatureUse.Checked ? GetFeatureCode() : 0;
            solverDelegate(); //запуск решателя
            if (!maze.Result)
                MessageBox.Show("У лабиринта отсутствует решение.");
        }

        /// <summary>
        /// Метод меняет состояние некоторых кнопок на форме (вкл/выкл)
        /// </summary>
        /// <param name="status">Статус кнопок: true - активна, false - выключена</param>
        private void SwitchButtonsStatus(bool status)
        {
            buttonMazeGeneration.Enabled = status;
            buttonSolverStart.Enabled = status;
            buttonGame.Enabled = status;
        }

        private void ButtonGame_Click(object sender, EventArgs e)
        {
            if (!gameInit)
            {
                maze.GameInit();
                gameInit = true;
            }
            textBoxWidth.Focus();
            buttonGame.Enabled = false;
        }

        private void ButtonGenPicture_Click(object sender, EventArgs e)
        {
            bool isMazeValid;
            int size;
            try
            {
                size = int.Parse(textBoxSize.Text);
            }
            catch
            {
                MessageBox.Show("Введите нормальный размер для отрисовки!");
                return;
            }
            isMazeValid = CreateMazeObject(true);
            if (isMazeValid)
            {
                view.InitMazeBitmap(maze.Maze, size);
                maze.GenerateMazeWithRecursiveBacktracker();
                if (checkBoxWithSolution.Checked)
                    SolverSelection();
                SaveFileDialog dialog = new SaveFileDialog() { Filter = "Png Files|*.png|All Files (*.*)|*.*" };
                if (dialog.ShowDialog() == DialogResult.OK)
                    view.MazeBitmap.Save(dialog.FileName, ImageFormat.Png);
                view.Dispose();
                drawingPicturebox.Dispose();
            }
        }

        private void ButtonGenerateBatch_Click(object sender, EventArgs e)
        {
            bool isMazeValid;
            int size;
            int count;
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

            progressForm.Init(count);
            isMazeValid = CreateMazeObject(true);
            if (isMazeValid)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog { Description = "Выберите папку для сохранения набора изображений" };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    progressForm.Show();
                    for (int i = 0; i < count; i++)
                    {
                        view.InitMazeBitmap(maze.Maze, size);
                        maze.GenerateMazeWithRecursiveBacktracker();
                        view.MazeBitmap.Save(dialog.SelectedPath + "/maze" + i + ".png", ImageFormat.Png);
                        if (checkBoxWithSolution.Checked)
                        {
                            SolverSelection();
                            view.MazeBitmap.Save(dialog.SelectedPath + "/maze" + i + "solved.png", ImageFormat.Png);
                        }
                        progressForm.ProgressBarUpdate();
                        view.Dispose();
                        drawingPicturebox.Dispose();
                        CreateMazeObject(true);
                    }
                    progressForm.Hide();
                }
            }
        }

        // TODO: Переписать, чтобы проверял, а не создавал (создание - в отдельный метод)
        /// <summary>
        /// Метод для создания нового обхекта лабиринат
        /// </summary>
        /// <param name="isBitmapUsed">Указывает, производится ли отрисовка на форме или в битмапе</param>
        /// <returns>Возвращает true, если был создан новый объект и false - если нет (например, если параметры некорректны)</returns>
        private bool CreateMazeObject(bool isBitmapUsed)
        {
            if (maze != null)
                maze.Clear();
            maze = null;
            bool isFromStart;
            int featureCode = 0;
            double prob;
            double whiteProb;
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
            try
            {
                Point startpoint = checkBoxStart.Checked ?
                    new Point(int.Parse(textBoxStartX.Text) * 2 + 1, int.Parse(textBoxStartY.Text) * 2 + 1) : new Point(1, 1);
                Point finishpoint = checkBoxFinish.Checked ?
                    new Point(int.Parse(textBoxStartX.Text) * 2 + 1, int.Parse(textBoxStartY.Text) * 2 + 1) : new Point(width * 2 - 1, height * 2 - 1);
                isFromStart = checkBoxFromBegin.Checked;
                if (checkBoxFeatureUse.Checked)
                    featureCode = GetFeatureCode();
                view = new View(pictureBoxLabirint.CreateGraphics());
                maze = new MazeMainClass(width, height, startpoint, finishpoint, prob, whiteProb, isFromStart, isBitmapUsed, featureCode, isBitmapUsed ? 0 : sleep, view, random);
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
            pictureBoxLabirint.Width = Size.Width - 360;
            pictureBoxLabirint.Height = Size.Height - 80;
            if (drawingPicturebox != null)
                drawingPicturebox.Dispose();
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
                if (maze.IsMazeFinished)
                {
                    gameInit = false;
                    buttonGame.Enabled = true;
                    maze.IsMazeFinished = false;
                    MessageBox.Show("Лабиринт решён!");
                }
            }
        }

        private void checkBoxFeatureUse_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxDrawFeatures.Visible = checkBoxFeatureUse.Checked;
        }

        private void checkBoxStart_CheckedChanged(object sender, EventArgs e)
        {
            label6.Visible = checkBoxStart.Checked;
            label7.Visible = checkBoxStart.Checked;
            textBoxStartX.Visible = checkBoxStart.Checked;
            textBoxStartY.Visible = checkBoxStart.Checked;
        }

        private void checkBoxFinish_CheckedChanged(object sender, EventArgs e)
        {
            label5.Visible = checkBoxFinish.Checked;
            label4.Visible = checkBoxFinish.Checked;
            textBoxEndX.Visible = checkBoxFinish.Checked;
            textBoxEndY.Visible = checkBoxFinish.Checked;
        }

        private void checkBoxAdditionalGeneration_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxGenerationAdditionalParams.Visible = checkBoxAdditionalGeneration.Checked;
        }
    }
}
