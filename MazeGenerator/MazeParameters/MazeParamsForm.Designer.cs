
namespace MazeGenerator
{
    partial class MazeParamsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxAdditionalGeneration = new System.Windows.Forms.CheckBox();
            this.groupBoxGenerationAdditionalParams = new System.Windows.Forms.GroupBox();
            this.textBoxWhiteSpaceProb = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxEmptyPlacesProb = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxEndY = new System.Windows.Forms.TextBox();
            this.textBoxEndX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxFromBegin = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxStartY = new System.Windows.Forms.TextBox();
            this.textBoxStartX = new System.Windows.Forms.TextBox();
            this.checkBoxFinish = new System.Windows.Forms.CheckBox();
            this.checkBoxStart = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSleep = new System.Windows.Forms.TextBox();
            this.groupBoxDrawFeatures = new System.Windows.Forms.GroupBox();
            this.radioButtonFeatureDarkStyle = new System.Windows.Forms.RadioButton();
            this.radioButtonFeatureLightStyle = new System.Windows.Forms.RadioButton();
            this.radioButtonFeatureYellow = new System.Windows.Forms.RadioButton();
            this.radioButtonFeatureTurquoise = new System.Windows.Forms.RadioButton();
            this.radioButtonFeatureViolet = new System.Windows.Forms.RadioButton();
            this.radioButtonFeatureBlue = new System.Windows.Forms.RadioButton();
            this.radioButtonFeatureGreen = new System.Windows.Forms.RadioButton();
            this.radioButtonFeatureRed = new System.Windows.Forms.RadioButton();
            this.radioButtonFeatureFiftyShades = new System.Windows.Forms.RadioButton();
            this.radioButtonFeatureChaos = new System.Windows.Forms.RadioButton();
            this.checkBoxFeatureUse = new System.Windows.Forms.CheckBox();
            this.buttonSaveParams = new System.Windows.Forms.Button();
            this.groupBoxGenerationAdditionalParams.SuspendLayout();
            this.groupBoxDrawFeatures.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxAdditionalGeneration
            // 
            this.checkBoxAdditionalGeneration.AutoSize = true;
            this.checkBoxAdditionalGeneration.Location = new System.Drawing.Point(24, 45);
            this.checkBoxAdditionalGeneration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxAdditionalGeneration.Name = "checkBoxAdditionalGeneration";
            this.checkBoxAdditionalGeneration.Size = new System.Drawing.Size(290, 20);
            this.checkBoxAdditionalGeneration.TabIndex = 61;
            this.checkBoxAdditionalGeneration.Text = "Дополнительные параметры генерации";
            this.checkBoxAdditionalGeneration.UseVisualStyleBackColor = true;
            this.checkBoxAdditionalGeneration.CheckedChanged += new System.EventHandler(this.CheckBoxAdditionalGeneration_CheckedChanged);
            // 
            // groupBoxGenerationAdditionalParams
            // 
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.textBoxWhiteSpaceProb);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.label11);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.textBoxEmptyPlacesProb);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.label10);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.label4);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.label5);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.textBoxEndY);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.textBoxEndX);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.label6);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.checkBoxFromBegin);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.label7);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.textBoxStartY);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.textBoxStartX);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.checkBoxFinish);
            this.groupBoxGenerationAdditionalParams.Controls.Add(this.checkBoxStart);
            this.groupBoxGenerationAdditionalParams.Location = new System.Drawing.Point(21, 65);
            this.groupBoxGenerationAdditionalParams.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxGenerationAdditionalParams.Name = "groupBoxGenerationAdditionalParams";
            this.groupBoxGenerationAdditionalParams.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxGenerationAdditionalParams.Size = new System.Drawing.Size(315, 155);
            this.groupBoxGenerationAdditionalParams.TabIndex = 60;
            this.groupBoxGenerationAdditionalParams.TabStop = false;
            this.groupBoxGenerationAdditionalParams.Visible = false;
            // 
            // textBoxWhiteSpaceProb
            // 
            this.textBoxWhiteSpaceProb.Location = new System.Drawing.Point(228, 69);
            this.textBoxWhiteSpaceProb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxWhiteSpaceProb.Name = "textBoxWhiteSpaceProb";
            this.textBoxWhiteSpaceProb.Size = new System.Drawing.Size(59, 22);
            this.textBoxWhiteSpaceProb.TabIndex = 49;
            this.textBoxWhiteSpaceProb.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 16);
            this.label11.TabIndex = 48;
            this.label11.Text = "Вероятность дырок";
            // 
            // textBoxEmptyPlacesProb
            // 
            this.textBoxEmptyPlacesProb.Location = new System.Drawing.Point(228, 43);
            this.textBoxEmptyPlacesProb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxEmptyPlacesProb.Name = "textBoxEmptyPlacesProb";
            this.textBoxEmptyPlacesProb.Size = new System.Drawing.Size(59, 22);
            this.textBoxEmptyPlacesProb.TabIndex = 36;
            this.textBoxEmptyPlacesProb.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(164, 16);
            this.label10.TabIndex = 35;
            this.label10.Text = "Вероятность пропусков";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Y";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "X";
            this.label5.Visible = false;
            // 
            // textBoxEndY
            // 
            this.textBoxEndY.Location = new System.Drawing.Point(228, 121);
            this.textBoxEndY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxEndY.Name = "textBoxEndY";
            this.textBoxEndY.Size = new System.Drawing.Size(59, 22);
            this.textBoxEndY.TabIndex = 18;
            this.textBoxEndY.Text = "9";
            this.textBoxEndY.Visible = false;
            // 
            // textBoxEndX
            // 
            this.textBoxEndX.Location = new System.Drawing.Point(133, 121);
            this.textBoxEndX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxEndX.Name = "textBoxEndX";
            this.textBoxEndX.Size = new System.Drawing.Size(57, 22);
            this.textBoxEndX.TabIndex = 17;
            this.textBoxEndX.Text = "9";
            this.textBoxEndX.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(205, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Y";
            this.label6.Visible = false;
            // 
            // checkBoxFromBegin
            // 
            this.checkBoxFromBegin.AutoSize = true;
            this.checkBoxFromBegin.Location = new System.Drawing.Point(3, 20);
            this.checkBoxFromBegin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxFromBegin.Name = "checkBoxFromBegin";
            this.checkBoxFromBegin.Size = new System.Drawing.Size(165, 20);
            this.checkBoxFromBegin.TabIndex = 34;
            this.checkBoxFromBegin.Text = "Генерация со старта";
            this.checkBoxFromBegin.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(115, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "X";
            this.label7.Visible = false;
            // 
            // textBoxStartY
            // 
            this.textBoxStartY.Location = new System.Drawing.Point(228, 95);
            this.textBoxStartY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxStartY.Name = "textBoxStartY";
            this.textBoxStartY.Size = new System.Drawing.Size(59, 22);
            this.textBoxStartY.TabIndex = 13;
            this.textBoxStartY.Text = "0";
            this.textBoxStartY.Visible = false;
            // 
            // textBoxStartX
            // 
            this.textBoxStartX.Location = new System.Drawing.Point(133, 95);
            this.textBoxStartX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxStartX.Name = "textBoxStartX";
            this.textBoxStartX.Size = new System.Drawing.Size(57, 22);
            this.textBoxStartX.TabIndex = 12;
            this.textBoxStartX.Text = "0";
            this.textBoxStartX.Visible = false;
            // 
            // checkBoxFinish
            // 
            this.checkBoxFinish.AutoSize = true;
            this.checkBoxFinish.Location = new System.Drawing.Point(3, 121);
            this.checkBoxFinish.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxFinish.Name = "checkBoxFinish";
            this.checkBoxFinish.Size = new System.Drawing.Size(109, 20);
            this.checkBoxFinish.TabIndex = 21;
            this.checkBoxFinish.Text = "Свой финиш";
            this.checkBoxFinish.UseVisualStyleBackColor = true;
            this.checkBoxFinish.CheckedChanged += new System.EventHandler(this.CheckBoxFinish_CheckedChanged);
            // 
            // checkBoxStart
            // 
            this.checkBoxStart.AutoSize = true;
            this.checkBoxStart.Location = new System.Drawing.Point(3, 96);
            this.checkBoxStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxStart.Name = "checkBoxStart";
            this.checkBoxStart.Size = new System.Drawing.Size(102, 20);
            this.checkBoxStart.TabIndex = 16;
            this.checkBoxStart.Text = "Свой старт";
            this.checkBoxStart.UseVisualStyleBackColor = true;
            this.checkBoxStart.CheckedChanged += new System.EventHandler(this.CheckBoxStart_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(235, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 16);
            this.label3.TabIndex = 59;
            this.label3.Text = "Задержка отрисовки";
            // 
            // textBoxSleep
            // 
            this.textBoxSleep.Location = new System.Drawing.Point(394, 12);
            this.textBoxSleep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSleep.Name = "textBoxSleep";
            this.textBoxSleep.Size = new System.Drawing.Size(63, 22);
            this.textBoxSleep.TabIndex = 58;
            this.textBoxSleep.Text = "0";
            // 
            // groupBoxDrawFeatures
            // 
            this.groupBoxDrawFeatures.Controls.Add(this.radioButtonFeatureDarkStyle);
            this.groupBoxDrawFeatures.Controls.Add(this.radioButtonFeatureLightStyle);
            this.groupBoxDrawFeatures.Controls.Add(this.radioButtonFeatureYellow);
            this.groupBoxDrawFeatures.Controls.Add(this.radioButtonFeatureTurquoise);
            this.groupBoxDrawFeatures.Controls.Add(this.radioButtonFeatureViolet);
            this.groupBoxDrawFeatures.Controls.Add(this.radioButtonFeatureBlue);
            this.groupBoxDrawFeatures.Controls.Add(this.radioButtonFeatureGreen);
            this.groupBoxDrawFeatures.Controls.Add(this.radioButtonFeatureRed);
            this.groupBoxDrawFeatures.Controls.Add(this.radioButtonFeatureFiftyShades);
            this.groupBoxDrawFeatures.Controls.Add(this.radioButtonFeatureChaos);
            this.groupBoxDrawFeatures.Location = new System.Drawing.Point(370, 65);
            this.groupBoxDrawFeatures.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxDrawFeatures.Name = "groupBoxDrawFeatures";
            this.groupBoxDrawFeatures.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxDrawFeatures.Size = new System.Drawing.Size(317, 156);
            this.groupBoxDrawFeatures.TabIndex = 63;
            this.groupBoxDrawFeatures.TabStop = false;
            this.groupBoxDrawFeatures.Visible = false;
            // 
            // radioButtonFeatureDarkStyle
            // 
            this.radioButtonFeatureDarkStyle.AutoSize = true;
            this.radioButtonFeatureDarkStyle.Location = new System.Drawing.Point(183, 37);
            this.radioButtonFeatureDarkStyle.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureDarkStyle.Name = "radioButtonFeatureDarkStyle";
            this.radioButtonFeatureDarkStyle.Size = new System.Drawing.Size(120, 20);
            this.radioButtonFeatureDarkStyle.TabIndex = 53;
            this.radioButtonFeatureDarkStyle.TabStop = true;
            this.radioButtonFeatureDarkStyle.Text = "Тёмная гамма";
            this.radioButtonFeatureDarkStyle.UseVisualStyleBackColor = true;
            // 
            // radioButtonFeatureLightStyle
            // 
            this.radioButtonFeatureLightStyle.AutoSize = true;
            this.radioButtonFeatureLightStyle.Location = new System.Drawing.Point(183, 9);
            this.radioButtonFeatureLightStyle.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureLightStyle.Name = "radioButtonFeatureLightStyle";
            this.radioButtonFeatureLightStyle.Size = new System.Drawing.Size(126, 20);
            this.radioButtonFeatureLightStyle.TabIndex = 52;
            this.radioButtonFeatureLightStyle.TabStop = true;
            this.radioButtonFeatureLightStyle.Text = "Светлая гамма";
            this.radioButtonFeatureLightStyle.UseVisualStyleBackColor = true;
            // 
            // radioButtonFeatureYellow
            // 
            this.radioButtonFeatureYellow.AutoSize = true;
            this.radioButtonFeatureYellow.Location = new System.Drawing.Point(183, 65);
            this.radioButtonFeatureYellow.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureYellow.Name = "radioButtonFeatureYellow";
            this.radioButtonFeatureYellow.Size = new System.Drawing.Size(81, 20);
            this.radioButtonFeatureYellow.TabIndex = 51;
            this.radioButtonFeatureYellow.TabStop = true;
            this.radioButtonFeatureYellow.Text = "Желтый";
            this.radioButtonFeatureYellow.UseVisualStyleBackColor = true;
            // 
            // radioButtonFeatureTurquoise
            // 
            this.radioButtonFeatureTurquoise.AutoSize = true;
            this.radioButtonFeatureTurquoise.Location = new System.Drawing.Point(183, 94);
            this.radioButtonFeatureTurquoise.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureTurquoise.Name = "radioButtonFeatureTurquoise";
            this.radioButtonFeatureTurquoise.Size = new System.Drawing.Size(104, 20);
            this.radioButtonFeatureTurquoise.TabIndex = 50;
            this.radioButtonFeatureTurquoise.TabStop = true;
            this.radioButtonFeatureTurquoise.Text = "Бирюзовый";
            this.radioButtonFeatureTurquoise.UseVisualStyleBackColor = true;
            // 
            // radioButtonFeatureViolet
            // 
            this.radioButtonFeatureViolet.AutoSize = true;
            this.radioButtonFeatureViolet.Location = new System.Drawing.Point(183, 122);
            this.radioButtonFeatureViolet.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureViolet.Name = "radioButtonFeatureViolet";
            this.radioButtonFeatureViolet.Size = new System.Drawing.Size(111, 20);
            this.radioButtonFeatureViolet.TabIndex = 49;
            this.radioButtonFeatureViolet.TabStop = true;
            this.radioButtonFeatureViolet.Text = "Фиолетовый";
            this.radioButtonFeatureViolet.UseVisualStyleBackColor = true;
            // 
            // radioButtonFeatureBlue
            // 
            this.radioButtonFeatureBlue.AutoSize = true;
            this.radioButtonFeatureBlue.Location = new System.Drawing.Point(7, 122);
            this.radioButtonFeatureBlue.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureBlue.Name = "radioButtonFeatureBlue";
            this.radioButtonFeatureBlue.Size = new System.Drawing.Size(117, 20);
            this.radioButtonFeatureBlue.TabIndex = 48;
            this.radioButtonFeatureBlue.Text = "В синих тонах";
            this.radioButtonFeatureBlue.UseVisualStyleBackColor = true;
            // 
            // radioButtonFeatureGreen
            // 
            this.radioButtonFeatureGreen.AutoSize = true;
            this.radioButtonFeatureGreen.Location = new System.Drawing.Point(7, 94);
            this.radioButtonFeatureGreen.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureGreen.Name = "radioButtonFeatureGreen";
            this.radioButtonFeatureGreen.Size = new System.Drawing.Size(135, 20);
            this.radioButtonFeatureGreen.TabIndex = 47;
            this.radioButtonFeatureGreen.Text = "В зелёных тонах";
            this.radioButtonFeatureGreen.UseVisualStyleBackColor = true;
            // 
            // radioButtonFeatureRed
            // 
            this.radioButtonFeatureRed.AutoSize = true;
            this.radioButtonFeatureRed.Location = new System.Drawing.Point(8, 65);
            this.radioButtonFeatureRed.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureRed.Name = "radioButtonFeatureRed";
            this.radioButtonFeatureRed.Size = new System.Drawing.Size(133, 20);
            this.radioButtonFeatureRed.TabIndex = 46;
            this.radioButtonFeatureRed.Text = "В красных тонах";
            this.radioButtonFeatureRed.UseVisualStyleBackColor = true;
            // 
            // radioButtonFeatureFiftyShades
            // 
            this.radioButtonFeatureFiftyShades.AutoSize = true;
            this.radioButtonFeatureFiftyShades.Location = new System.Drawing.Point(8, 37);
            this.radioButtonFeatureFiftyShades.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureFiftyShades.Name = "radioButtonFeatureFiftyShades";
            this.radioButtonFeatureFiftyShades.Size = new System.Drawing.Size(154, 20);
            this.radioButtonFeatureFiftyShades.TabIndex = 45;
            this.radioButtonFeatureFiftyShades.Text = "50 оттенков серого";
            this.radioButtonFeatureFiftyShades.UseVisualStyleBackColor = true;
            // 
            // radioButtonFeatureChaos
            // 
            this.radioButtonFeatureChaos.AutoSize = true;
            this.radioButtonFeatureChaos.Checked = true;
            this.radioButtonFeatureChaos.Location = new System.Drawing.Point(8, 9);
            this.radioButtonFeatureChaos.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureChaos.Name = "radioButtonFeatureChaos";
            this.radioButtonFeatureChaos.Size = new System.Drawing.Size(97, 20);
            this.radioButtonFeatureChaos.TabIndex = 44;
            this.radioButtonFeatureChaos.TabStop = true;
            this.radioButtonFeatureChaos.Text = "Цветохаос";
            this.radioButtonFeatureChaos.UseVisualStyleBackColor = true;
            // 
            // checkBoxFeatureUse
            // 
            this.checkBoxFeatureUse.AutoSize = true;
            this.checkBoxFeatureUse.Location = new System.Drawing.Point(377, 45);
            this.checkBoxFeatureUse.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxFeatureUse.Name = "checkBoxFeatureUse";
            this.checkBoxFeatureUse.Size = new System.Drawing.Size(221, 20);
            this.checkBoxFeatureUse.TabIndex = 62;
            this.checkBoxFeatureUse.Text = "Использовать другой покрас";
            this.checkBoxFeatureUse.UseVisualStyleBackColor = true;
            this.checkBoxFeatureUse.CheckedChanged += new System.EventHandler(this.CheckBoxFeatureUse_CheckedChanged);
            // 
            // buttonSaveParams
            // 
            this.buttonSaveParams.Location = new System.Drawing.Point(249, 242);
            this.buttonSaveParams.Name = "buttonSaveParams";
            this.buttonSaveParams.Size = new System.Drawing.Size(208, 42);
            this.buttonSaveParams.TabIndex = 64;
            this.buttonSaveParams.Text = "Сохранить";
            this.buttonSaveParams.UseVisualStyleBackColor = true;
            this.buttonSaveParams.Click += new System.EventHandler(this.ButtonSaveParams_Click);
            // 
            // MazeParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 296);
            this.Controls.Add(this.buttonSaveParams);
            this.Controls.Add(this.groupBoxDrawFeatures);
            this.Controls.Add(this.checkBoxFeatureUse);
            this.Controls.Add(this.checkBoxAdditionalGeneration);
            this.Controls.Add(this.groupBoxGenerationAdditionalParams);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSleep);
            this.Name = "MazeParamsForm";
            this.Text = "MazeParamsForm";
            this.groupBoxGenerationAdditionalParams.ResumeLayout(false);
            this.groupBoxGenerationAdditionalParams.PerformLayout();
            this.groupBoxDrawFeatures.ResumeLayout(false);
            this.groupBoxDrawFeatures.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAdditionalGeneration;
        private System.Windows.Forms.GroupBox groupBoxGenerationAdditionalParams;
        private System.Windows.Forms.TextBox textBoxWhiteSpaceProb;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxEmptyPlacesProb;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxEndY;
        private System.Windows.Forms.TextBox textBoxEndX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxFromBegin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxStartY;
        private System.Windows.Forms.TextBox textBoxStartX;
        private System.Windows.Forms.CheckBox checkBoxFinish;
        private System.Windows.Forms.CheckBox checkBoxStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSleep;
        private System.Windows.Forms.GroupBox groupBoxDrawFeatures;
        private System.Windows.Forms.RadioButton radioButtonFeatureDarkStyle;
        private System.Windows.Forms.RadioButton radioButtonFeatureLightStyle;
        private System.Windows.Forms.RadioButton radioButtonFeatureYellow;
        private System.Windows.Forms.RadioButton radioButtonFeatureTurquoise;
        private System.Windows.Forms.RadioButton radioButtonFeatureViolet;
        private System.Windows.Forms.RadioButton radioButtonFeatureBlue;
        private System.Windows.Forms.RadioButton radioButtonFeatureGreen;
        private System.Windows.Forms.RadioButton radioButtonFeatureRed;
        private System.Windows.Forms.RadioButton radioButtonFeatureFiftyShades;
        private System.Windows.Forms.RadioButton radioButtonFeatureChaos;
        private System.Windows.Forms.CheckBox checkBoxFeatureUse;
        private System.Windows.Forms.Button buttonSaveParams;
    }
}