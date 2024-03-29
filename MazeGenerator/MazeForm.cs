﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class MazeForm : Form
    {
        private readonly ProgressForm progressForm = new ProgressForm();
        private readonly FilesGenerationForm filesGenerationForm;

        private readonly MazeParamsForm mazeParamsForm = new MazeParamsForm();
        private MazeParamsData mazeParamsData;

        private View view;
        private Graphics drawingPicturebox; // Объект, на котором может производится отрисовка
        private MazeObject mazeClassObject; // Основной объект лабиринта
        private readonly Random random;

        enum DrawingMethod
        {
            Picturebox,
            Bitmap
        }

        enum ButtonsStatus
        {
            Enabled,
            Disabled
        }

        public MazeForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; // Для дебага из-за многопоточности
            random = new Random();
            filesGenerationForm = new FilesGenerationForm(this);
            mazeParamsData = new MazeParamsData();

            comboBoxGenerationMethods.SelectedIndex = 0;
            comboBoxSolveMethods.SelectedIndex = 0;
        }

        private void buttonMazeGeneration_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(RunMazeGeneration);
            ChangeGenerationAndSolveButtonsStatus(ButtonsStatus.Disabled);
            thread.Start();
        }

        /// <summary>
        /// Генерация лабиринта и вывод информации о ней
        /// </summary>
        /// <param name="isHuntMethod">Используется ли метод Hunt-And-Kill</param>
        private void RunMazeGeneration()
        {
            if (!AreWidthAndHeightCorrect())
            {
                ChangeGenerationAndSolveButtonsStatus(ButtonsStatus.Enabled);
                return;
            }

            bool areParamsCorrect = mazeParamsForm.FillAndCheckMazeParamsData(ref mazeParamsData);
            if (!areParamsCorrect)
            {
                ChangeGenerationAndSolveButtonsStatus(ButtonsStatus.Enabled);
                return;
            }

            CreateViewObject();
            bool isMazeCreated = CheckAndCreateMazeObject(DrawingMethod.Picturebox);
            if (isMazeCreated)
            {
                DrawInitMazeState();
                ProcessMazeGeneration();
            }
            UpdateButtonsAfterMazeGeneration();
        }

        private void UpdateButtonsAfterMazeGeneration()
        {
            if (mazeClassObject != null)
                ChangeGenerationAndSolveButtonsStatus(ButtonsStatus.Enabled);
            else
                buttonMazeGeneration.Enabled = true;
        }

        private void ProcessMazeGeneration()
        {
            switch (comboBoxGenerationMethods.Text)
            {
                case "Backtracking":
                    mazeClassObject.GenerateMazeWithRecursiveBacktracker();
                    break;
                case "Eller":
                    mazeClassObject.GenerateMazeWithEller();
                    break;
                default:
                    mazeClassObject.GenerateMazeWithHuntAndKill();
                    break;
            }
        }

        private void DrawInitMazeState()
        {
            int pixelSize = Math.Min(pictureBoxLabirint.Width / mazeClassObject.Maze.GetLength(0),
                pictureBoxLabirint.Height / mazeClassObject.Maze.GetLength(1));
            view.DrawMazeInitState(mazeClassObject.Maze.GetLength(0), mazeClassObject.Maze.GetLength(1), pixelSize);
        }

        private void CreateViewObject()
        {
            drawingPicturebox = pictureBoxLabirint.CreateGraphics();
            view = new View(drawingPicturebox);
        }

        private bool AreWidthAndHeightCorrect()
        {
            try
            {
                mazeParamsData.Width = int.Parse(textBoxWidth.Text);
                mazeParamsData.Height = int.Parse(textBoxHeight.Text);
            }
            catch
            {
                MessageBox.Show("Неверно введена ширина/высота!");
                return false;
            }
            if (mazeParamsData.Width <= 1 || mazeParamsData.Height <= 1)
            {
                MessageBox.Show("Ширина/высота должны быть больше 1!");
                return false;
            }
            return true;
        }

        private void buttonSolverStart_Click(object sender, EventArgs e)
        {
            SolveMaze();
        }

        /// <summary>
        /// Метод для запуска решателя лабиринтов
        /// </summary>
        private void SolveMaze()
        {
            UpdateParamsBeforeSolve();
            SolverSelectAndStart();
            if (!mazeClassObject.IsSolutionFound)
                MessageBox.Show("У лабиринта отсутствует решение.");
        }

        private void UpdateParamsBeforeSolve()
        {
            mazeParamsForm.getUpdatedSleep(ref mazeParamsData);
            mazeClassObject.Sleep = mazeParamsData.Sleep;
            mazeClassObject.FeatureCode = mazeParamsData.FeatureCode;
        }

        /// <summary>
        /// Включает/выключает кнопки генерации и решения лабиринта
        /// </summary>
        /// <param name="status">Статус кнопок: true - активна, false - выключена</param>
        private void ChangeGenerationAndSolveButtonsStatus(ButtonsStatus buttonsStatus)
        {
            bool status = buttonsStatus == ButtonsStatus.Enabled;

            buttonMazeGeneration.Enabled = status;
            buttonAdditionalParams.Enabled = status;
            buttonSolverStart.Enabled = status;
        }

        /// <summary>
        /// Метод для создания нового объекта лабиринта
        /// </summary>
        /// <param name="isBitmapUsed">Указывает, производится ли отрисовка на форме или в битмапе</param>
        /// <returns>Возвращает true, если был создан новый объект и false - если нет (например, если параметры некорректны)</returns>
        private bool CheckAndCreateMazeObject(DrawingMethod drawingMethod)
        {
            bool isBitmapUsed = drawingMethod == DrawingMethod.Bitmap;

            mazeClassObject?.Clear();
            if (AreMazeParamsValid(isBitmapUsed, mazeParamsData.Width, mazeParamsData.Height))
            {
                return CreateMaze(isBitmapUsed);
            }
            return false;
        }

        private bool AreMazeParamsValid(bool isBitmapUsed, int width, int height)
        {
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
        private bool CreateMaze(bool isBitmapUsed)
        {
            mazeClassObject = null;
            bool isFromStart = mazeParamsData.IsGeneratedFromStart;
            int featureCode = mazeParamsData.FeatureCode;
            int width = mazeParamsData.Width;
            int height = mazeParamsData.Height;
            double prob = mazeParamsData.Prob;
            double whiteProb = mazeParamsData.WhiteProb;
            int sleep = mazeParamsData.Sleep;
            Point startpoint = mazeParamsData.StartPoint;
            Point finishpoint = mazeParamsData.FinishPoint;

            view = new View(pictureBoxLabirint.CreateGraphics(), startpoint, finishpoint);

            try
            {
                mazeClassObject = new MazeObject(width, height, startpoint, finishpoint, prob, whiteProb, isFromStart, isBitmapUsed, featureCode, isBitmapUsed ? 0 : sleep, view, random);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Слишком большой размер массива для лабиринта!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Выбор метода для решения лабиринта в случае отрисовки лабиринта в изображении
        /// </summary>
        private void SolverSelectAndStart()
        {
            if (comboBoxSolveMethods.Text == "Левые повороты")
                mazeClassObject.LeftRotateSolver();
            if (comboBoxSolveMethods.Text == "Правые повороты")
                mazeClassObject.RightRotateSolver();
            if (comboBoxSolveMethods.Text == "Случайные повороты")
                mazeClassObject.RandomSolver();
        }

        private void MazeForm_SizeChanged(object sender, EventArgs e)
        {
            pictureBoxLabirint.Width = Size.Width - 360;
            pictureBoxLabirint.Height = Size.Height - 80;
            drawingPicturebox?.Dispose();
            CreateViewObject();
        }

        private void ButtonFilesGenerate_Click(object sender, EventArgs e)
        {
            filesGenerationForm.Show();
        }

        public void ProcessFiles(FileGenerationData generationData)
        {
            int size = generationData.Size;
            int count = generationData.Count;
            bool isWithSolution = generationData.IsWithSolution;
            string selectedPath = generationData.SelectedPath;

            progressForm.Init(count);
            bool isMazeCreated = CheckAndCreateMazeObject(DrawingMethod.Bitmap);
            if (isMazeCreated)
            {
                progressForm.Show();
                ProcessImagesCreation(size, count, isWithSolution, selectedPath);
                progressForm.Hide();
            }
            progressForm.Dispose();
        }

        private void ProcessImagesCreation(int size, int count, bool isWithSolution, string selectedPath)
        {
            for (int i = 0; i < count; i++)
            {
                view.InitMazeBitmap(mazeClassObject.Maze.GetLength(0) * size, mazeClassObject.Maze.GetLength(1) * size, size);
                //TODO: другие методы при генерации изображений
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
                CheckAndCreateMazeObject(DrawingMethod.Bitmap);
            }
        }

        private void ButtonAdditionalParams_Click(object sender, EventArgs e)
        {
            mazeParamsForm.Show();
        }
    }
}
