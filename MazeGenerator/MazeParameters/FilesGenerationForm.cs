﻿using System;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class FilesGenerationForm : Form
    {

        private readonly MazeForm mazeForm;

        public FilesGenerationForm(MazeForm mazeForm)
        {
            this.mazeForm = mazeForm;
            InitializeComponent();
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            FileGenerationData generationData = new FileGenerationData();
            generationData.IsWithSolution = checkBoxWithSolution.Checked;
            try
            {
                generationData.Count = int.Parse(textBoxCount.Text);
                generationData.Size = int.Parse(textBoxSize.Text);
            }
            catch
            {
                MessageBox.Show("Неправильно введён размер или кол-во изображений!");
                return;
            }
            FolderBrowserDialog dialog = new FolderBrowserDialog { Description = "Выберите папку для сохранения набора изображений" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                generationData.SelectedPath = dialog.SelectedPath;
            }
            Hide();
            mazeForm.ProcessFiles(generationData);
        }
    }
}
