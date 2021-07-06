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
        private View view;
        private Graphics drawingPicturebox; // Объект, на котором может производится отрисовка
        private MazeMainClass mazeClassObject; // Основной объект лабиринта
        private readonly Random random;

        public MazeForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; // Для дебага из-за многопоточности
            random = new Random();
        }

        private void buttonMazeGeneration_Click(object sender, EventArgs e)
        {
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
                int pixelSize = Math.Min(pictureBoxLabirint.Width / mazeClassObject.Maze.GetLength(0), 
                    pictureBoxLabirint.Height / mazeClassObject.Maze.GetLength(1));
                view.DrawMazeInitState(mazeClassObject.Maze.GetLength(0), mazeClassObject.Maze.GetLength(1), pixelSize);
                if (radioButtonHuntAndKill.Checked)
                    mazeClassObject.GenerateMazeWithHuntAndKill();
                else
                    mazeClassObject.GenerateMazeWithRecursiveBacktracker();
            }
            if (mazeClassObject != null)
                SwitchButtonsStatus(true);
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
        /// Метод меняет состояние некоторых кнопок на форме (вкл/выкл)
        /// </summary>
        /// <param name="status">Статус кнопок: true - активна, false - выключена</param>
        private void SwitchButtonsStatus(bool status)
        {
            buttonMazeGeneration.Enabled = status;
            buttonSolverStart.Enabled = status;
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
                MessageBox.Show("Неправильно введён размер!");
                return;
            }
            isMazeValid = CreateMazeObject(true);
            if (isMazeValid)
            {
                view.InitMazeBitmap(mazeClassObject.Maze.GetLength(0) * size, mazeClassObject.Maze.GetLength(1) * size, size);
                mazeClassObject.GenerateMazeWithRecursiveBacktracker();
                if (checkBoxWithSolution.Checked)
                    SolverSelectAndStart();
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
                MessageBox.Show("Неправильно введён размер или кол-во изображений!");
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
                        view.InitMazeBitmap(mazeClassObject.Maze.GetLength(0) * size, mazeClassObject.Maze.GetLength(1) * size, size);
                        mazeClassObject.GenerateMazeWithRecursiveBacktracker();
                        view.MazeBitmap.Save(dialog.SelectedPath + "/maze" + i + ".png", ImageFormat.Png);
                        if (checkBoxWithSolution.Checked)
                        {
                            SolverSelectAndStart();
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
            progressForm.Dispose();
        }

        // TODO: Переписать, чтобы проверял, а не создавал (создание - в отдельный метод)
        /// <summary>
        /// Метод для создания нового обхекта лабиринат
        /// </summary>
        /// <param name="isBitmapUsed">Указывает, производится ли отрисовка на форме или в битмапе</param>
        /// <returns>Возвращает true, если был создан новый объект и false - если нет (например, если параметры некорректны)</returns>
        private bool CreateMazeObject(bool isBitmapUsed)
        {
            if (mazeClassObject != null)
                mazeClassObject.Clear();
            CheckMazeParams();
            mazeClassObject = null;
            bool isFromStart;
            int featureCode = 0;
            int width = int.Parse(textBoxWidth.Text);
            int height = int.Parse(textBoxHeight.Text);
            double prob = double.Parse(textBoxEmptyPlacesProb.Text);
            double whiteProb = double.Parse(textBoxWhiteSpaceProb.Text);
            int sleep = int.Parse(textBoxSleep.Text); ;
            try
            {
                Point startpoint = checkBoxStart.Checked ?
                    new Point(int.Parse(textBoxStartX.Text) * 2 + 1, int.Parse(textBoxStartY.Text) * 2 + 1) : new Point(1, 1);
                Point finishpoint = checkBoxFinish.Checked ?
                    new Point(int.Parse(textBoxEndX.Text) * 2 + 1, int.Parse(textBoxEndY.Text) * 2 + 1) : new Point(width * 2 - 1, height * 2 - 1);
                isFromStart = checkBoxFromBegin.Checked;
                if (checkBoxFeatureUse.Checked)
                    featureCode = GetFeatureCode();
                view = new View(pictureBoxLabirint.CreateGraphics(), startpoint, finishpoint);
                mazeClassObject = new MazeMainClass(width, height, startpoint, finishpoint, prob, whiteProb, isFromStart, isBitmapUsed, featureCode, isBitmapUsed ? 0 : sleep, view, random);
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

        private bool CheckMazeParams()
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
            return true;
        }

        private void createMaze() { 

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
    }
}
