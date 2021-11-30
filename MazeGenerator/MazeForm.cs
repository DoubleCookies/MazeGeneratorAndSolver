using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class MazeForm : Form
    {
        private readonly ProgressForm progressForm = new ProgressForm();
        private FilesGenerationForm filesGenerationForm;
        private View view;
        private Graphics drawingPicturebox; // Объект, на котором может производится отрисовка
        private MazeMainClass mazeClassObject; // Основной объект лабиринта
        private readonly Random random;

        public MazeForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; // Для дебага из-за многопоточности
            random = new Random();
            filesGenerationForm = new FilesGenerationForm(this);
        }

        //TODO: Перенос доп. параметров в форму
        private void buttonMazeGeneration_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(RunMazeGeneration);
            EnableGenerationAndSolveButtons(false);
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
            bool isMazeCreated = CreateMazeObject(false);
            if (isMazeCreated)
            {
                int pixelSize = Math.Min(pictureBoxLabirint.Width / mazeClassObject.Maze.GetLength(0), 
                    pictureBoxLabirint.Height / mazeClassObject.Maze.GetLength(1));
                view.DrawMazeInitState(mazeClassObject.Maze.GetLength(0), mazeClassObject.Maze.GetLength(1), pixelSize);
                if (radioButtonHuntAndKill.Checked)
                    mazeClassObject.GenerateMazeWithHuntAndKill();
                else
                    mazeClassObject.GenerateMazeWithRecursiveBacktracker();
            }
            if (mazeClassObject != null)
                EnableGenerationAndSolveButtons(true);
            else
                buttonMazeGeneration.Enabled = true;
        }

        private void buttonSolverStart_Click(object sender, EventArgs e)
        {
            SolveMaze();
        }

        /// <summary>
        /// Метод для запуска решателя лабиринтов
        /// </summary>
        /// <param name="launchMazeSolve">Делегат, передающий метод</param>
        /// <param name="method">Название метода</param>
        private void SolveMaze()
        {
            int sleep;
            try
            {
                sleep = int.Parse(textBoxSleep.Text);
            }
            catch 
            {
                MessageBox.Show("Введите правильное значение паузы!");
                return;
            }
            mazeClassObject.Sleep = sleep;
            mazeClassObject.FeatureCode = checkBoxFeatureUse.Checked ? GetFeatureCode() : 0;
            SolverSelectAndStart();
            if (!mazeClassObject.IsSolutionFound)
                MessageBox.Show("У лабиринта отсутствует решение.");
        }

        /// <summary>
        /// Включает/выключает кнопки генерации и решения лабиринта
        /// </summary>
        /// <param name="status">Статус кнопок: true - активна, false - выключена</param>
        private void EnableGenerationAndSolveButtons(bool status)
        {
            buttonMazeGeneration.Enabled = status;
            buttonSolverStart.Enabled = status;
        }

        //TODO: Перенос в отдельные формы для генерации файлов
        private void ButtonGenPicture_Click(object sender, EventArgs e)
        {
            
        }

        private void ButtonGenerateBatch_Click(object sender, EventArgs e)
        {
            filesGenerationForm.Show();
        }

        /// <summary>
        /// Метод для создания нового объекта лабиринта
        /// </summary>
        /// <param name="isBitmapUsed">Указывает, производится ли отрисовка на форме или в битмапе</param>
        /// <returns>Возвращает true, если был создан новый объект и false - если нет (например, если параметры некорректны)</returns>
        private bool CreateMazeObject(bool isBitmapUsed)
        {
            if (mazeClassObject != null)
                mazeClassObject.Clear();
            if (AreMazeParamsValid(isBitmapUsed)) { 
                return CreateMaze(isBitmapUsed);
            }
            return false;
        }

        private bool AreMazeParamsValid(bool isBitmapUsed)
        {

            double prob;
            double whiteProb;
            int width;
            int height;
            int sleep;
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
        /// Метод для создания объекта лабиринта
        /// </summary>
        /// <param name="isBitmapUsed">Указывает, производится ли отрисовка на форме или в битмапе</param>
        /// <returns>Возвращает true, если объект создан успешно (иначе - false)</returns>
        private bool CreateMaze(bool isBitmapUsed) {
            mazeClassObject = null;
            bool isFromStart;
            int featureCode = 0;
            int width = int.Parse(textBoxWidth.Text);
            int height = int.Parse(textBoxHeight.Text);
            double prob = double.Parse(textBoxEmptyPlacesProb.Text);
            double whiteProb = double.Parse(textBoxWhiteSpaceProb.Text);
            int sleep = int.Parse(textBoxSleep.Text);

            Point startpoint = checkBoxStart.Checked ? 
                new Point(int.Parse(textBoxStartX.Text) * 2 + 1, int.Parse(textBoxStartY.Text) * 2 + 1) : new Point(1, 1);
            Point finishpoint = checkBoxFinish.Checked ?
                new Point(int.Parse(textBoxEndX.Text) * 2 + 1, int.Parse(textBoxEndY.Text) * 2 + 1) : new Point(width * 2 - 1, height * 2 - 1);
            isFromStart = checkBoxFromBegin.Checked;
            if (checkBoxFeatureUse.Checked)
                featureCode = GetFeatureCode();
            view = new View(pictureBoxLabirint.CreateGraphics(), startpoint, finishpoint);

            try
            {
                mazeClassObject = new MazeMainClass(width, height, startpoint, finishpoint, prob, whiteProb, isFromStart, isBitmapUsed, featureCode, isBitmapUsed ? 0 : sleep, view, random);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Слишком большой размер массива для лабиринта!");
                return false;
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
        private void SolverSelectAndStart()
        {
            if (radioButtonLR.Checked)
                mazeClassObject.LeftRotateSolver();
            if (radioButtonRR.Checked)
                mazeClassObject.RightRotateSolver();
            if (radioButtonRandR.Checked)
                mazeClassObject.RandomSolver();
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

        private void ButtonFilesGenerate_Click(object sender, EventArgs e)
        {
            filesGenerationForm.Show();
        }

        public void ProcessFiles(GenerationData generationData)
        {
            int size = generationData.Size;
            int count = generationData.Count;
            bool isWithSolution = generationData.IsWithSolution;
            string selectedPath = generationData.SelectedPath;

            progressForm.Init(count);
            bool isMazeCreated = CreateMazeObject(true);
            if (isMazeCreated)
            {
                progressForm.Show();
                for (int i = 0; i < count; i++)
                {
                    view.InitMazeBitmap(mazeClassObject.Maze.GetLength(0) * size, mazeClassObject.Maze.GetLength(1) * size, size);
                    mazeClassObject.GenerateMazeWithRecursiveBacktracker();
                    view.MazeBitmap.Save(selectedPath + "/maze" + i + ".png", ImageFormat.Png);
                    if (isWithSolution)
                    {
                        SolverSelectAndStart();
                        view.MazeBitmap.Save(selectedPath + "/maze" + i + "solved.png", ImageFormat.Png);
                    }
                    progressForm.ProgressBarUpdate();
                    view.Dispose();
                    drawingPicturebox.Dispose();
                    CreateMazeObject(true);
                }
                progressForm.Hide();
            }
            progressForm.Dispose();
        }
    }
}
