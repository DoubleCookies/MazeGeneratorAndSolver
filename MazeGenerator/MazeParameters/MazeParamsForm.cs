using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class MazeParamsForm : Form
    {
        public MazeParamsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Проверяет параметры генерации лабиринта
        /// </summary>
        /// <param name="mazeParams">Обновляемые параметры лабиринта</param>
        /// <returns>Возвращает true, если параметры введены верно</returns>
        public bool FillAndCheckMazeParamsData(ref MazeParamsData mazeParams) 
        {
            mazeParams.IsDrawMethodEnabled = checkBoxFeatureUse.Checked;
            mazeParams.AreAdditionalParamsEnabled = checkBoxAdditionalGeneration.Checked;
            mazeParams.StartPoint = (checkBoxStart.Checked && mazeParams.AreAdditionalParamsEnabled) ? 
                new Point(int.Parse(textBoxStartX.Text) * 2 + 1, int.Parse(textBoxStartY.Text) * 2 + 1) : new Point(1, 1);
            mazeParams.FinishPoint = (checkBoxFinish.Checked && mazeParams.AreAdditionalParamsEnabled) ?
                new Point(int.Parse(textBoxEndX.Text) * 2 + 1, int.Parse(textBoxEndY.Text) * 2 + 1) : new Point(mazeParams.Width * 2 - 1, mazeParams.Height * 2 - 1);
            try
            {
                mazeParams.Sleep = int.Parse(textBoxSleep.Text);
                mazeParams.FeatureCode = mazeParams.IsDrawMethodEnabled ? GetFeatureCode() : 0;
                if (mazeParams.AreAdditionalParamsEnabled) {
                    mazeParams.IsGeneratedFromStart = checkBoxFromBegin.Checked;
                    mazeParams.Prob = double.Parse(textBoxEmptyPlacesProb.Text);
                    mazeParams.WhiteProb = double.Parse(textBoxWhiteSpaceProb.Text);
                }
            }
            catch {
                MessageBox.Show("Неверно введены параметры для генерации лабиринта!");
                return false;
            }
            return true;
        }

        public void getUpdatedSleep(ref MazeParamsData mazeParams) {
            try
            {
                mazeParams.Sleep = int.Parse(textBoxSleep.Text);
            }
            catch
            {
                MessageBox.Show("Неверно введено значение задержки!");
            }
        }

        /// <summary>
        /// Метод получения кода особенности отрисовки
        /// </summary>
        /// <returns>Возвращает числовое представление кода</returns>
        private int GetFeatureCode() {
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

        private void ButtonSaveParams_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void CheckBoxAdditionalGeneration_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxGenerationAdditionalParams.Visible = checkBoxAdditionalGeneration.Checked;
        }

        private void CheckBoxFeatureUse_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxDrawFeatures.Visible = checkBoxFeatureUse.Checked;
        }

        private void CheckBoxStart_CheckedChanged(object sender, EventArgs e)
        {
            label6.Visible = checkBoxStart.Checked;
            label7.Visible = checkBoxStart.Checked;
            textBoxStartX.Visible = checkBoxStart.Checked;
            textBoxStartY.Visible = checkBoxStart.Checked;
        }

        private void CheckBoxFinish_CheckedChanged(object sender, EventArgs e)
        {
            label4.Visible = checkBoxFinish.Checked;
            label5.Visible = checkBoxFinish.Checked;
            textBoxEndX.Visible = checkBoxFinish.Checked;
            textBoxEndY.Visible = checkBoxFinish.Checked;
        }
    }
}
