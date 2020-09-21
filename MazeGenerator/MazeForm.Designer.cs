namespace MazeGenerator
{
    partial class MazeForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MazeForm));
            this.pictureBoxLabirint = new System.Windows.Forms.PictureBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.buttonMazeGeneration = new System.Windows.Forms.Button();
            this.panelMazeGenerationMethod = new System.Windows.Forms.Panel();
            this.radioButtonHuntAndKill = new System.Windows.Forms.RadioButton();
            this.radioButtonBackTracking = new System.Windows.Forms.RadioButton();
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
            this.radioButtonRandR = new System.Windows.Forms.RadioButton();
            this.radioButtonRR = new System.Windows.Forms.RadioButton();
            this.radioButtonLR = new System.Windows.Forms.RadioButton();
            this.buttonSolverStart = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.buttonGenerateBatch = new System.Windows.Forms.Button();
            this.checkBoxWithSolution = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.buttonGenPictire = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSleep = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLabirint)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBoxGenerationAdditionalParams.SuspendLayout();
            this.panelMazeGenerationMethod.SuspendLayout();
            this.groupBoxDrawFeatures.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxLabirint
            // 
            this.pictureBoxLabirint.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxLabirint.Location = new System.Drawing.Point(5, 12);
            this.pictureBoxLabirint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxLabirint.Name = "pictureBoxLabirint";
            this.pictureBoxLabirint.Size = new System.Drawing.Size(1207, 889);
            this.pictureBoxLabirint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLabirint.TabIndex = 0;
            this.pictureBoxLabirint.TabStop = false;
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(71, 18);
            this.textBoxWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(59, 22);
            this.textBoxWidth.TabIndex = 1;
            this.textBoxWidth.Text = "10";
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(244, 18);
            this.textBoxHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(59, 22);
            this.textBoxHeight.TabIndex = 2;
            this.textBoxHeight.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ширина";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Высота";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxAdditionalGeneration);
            this.groupBox1.Controls.Add(this.groupBoxGenerationAdditionalParams);
            this.groupBox1.Controls.Add(this.buttonMazeGeneration);
            this.groupBox1.Controls.Add(this.panelMazeGenerationMethod);
            this.groupBox1.Controls.Add(this.groupBoxDrawFeatures);
            this.groupBox1.Controls.Add(this.checkBoxFeatureUse);
            this.groupBox1.Controls.Add(this.radioButtonRandR);
            this.groupBox1.Controls.Add(this.radioButtonRR);
            this.groupBox1.Controls.Add(this.radioButtonLR);
            this.groupBox1.Controls.Add(this.buttonSolverStart);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBoxCount);
            this.groupBox1.Controls.Add(this.buttonGenerateBatch);
            this.groupBox1.Controls.Add(this.checkBoxWithSolution);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxSize);
            this.groupBox1.Controls.Add(this.buttonGenPictire);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxSleep);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxHeight);
            this.groupBox1.Controls.Add(this.textBoxWidth);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(1218, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(330, 982);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // checkBoxAdditionalGeneration
            // 
            this.checkBoxAdditionalGeneration.AutoSize = true;
            this.checkBoxAdditionalGeneration.Location = new System.Drawing.Point(7, 150);
            this.checkBoxAdditionalGeneration.Name = "checkBoxAdditionalGeneration";
            this.checkBoxAdditionalGeneration.Size = new System.Drawing.Size(296, 21);
            this.checkBoxAdditionalGeneration.TabIndex = 57;
            this.checkBoxAdditionalGeneration.Text = "Дополнительные параметры генерации";
            this.checkBoxAdditionalGeneration.UseVisualStyleBackColor = true;
            this.checkBoxAdditionalGeneration.CheckedChanged += new System.EventHandler(this.checkBoxAdditionalGeneration_CheckedChanged);
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
            this.groupBoxGenerationAdditionalParams.Location = new System.Drawing.Point(5, 165);
            this.groupBoxGenerationAdditionalParams.Name = "groupBoxGenerationAdditionalParams";
            this.groupBoxGenerationAdditionalParams.Size = new System.Drawing.Size(315, 155);
            this.groupBoxGenerationAdditionalParams.TabIndex = 56;
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
            this.label11.Location = new System.Drawing.Point(3, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 17);
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
            this.label10.Size = new System.Drawing.Size(165, 17);
            this.label10.TabIndex = 35;
            this.label10.Text = "Вероятность пропусков";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Y";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(114, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "X";
            this.label5.Visible = false;
            // 
            // textBoxEndY
            // 
            this.textBoxEndY.Location = new System.Drawing.Point(228, 120);
            this.textBoxEndY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxEndY.Name = "textBoxEndY";
            this.textBoxEndY.Size = new System.Drawing.Size(59, 22);
            this.textBoxEndY.TabIndex = 18;
            this.textBoxEndY.Text = "9";
            this.textBoxEndY.Visible = false;
            // 
            // textBoxEndX
            // 
            this.textBoxEndX.Location = new System.Drawing.Point(133, 120);
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
            this.label6.Location = new System.Drawing.Point(206, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Y";
            this.label6.Visible = false;
            // 
            // checkBoxFromBegin
            // 
            this.checkBoxFromBegin.AutoSize = true;
            this.checkBoxFromBegin.Location = new System.Drawing.Point(2, 20);
            this.checkBoxFromBegin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxFromBegin.Name = "checkBoxFromBegin";
            this.checkBoxFromBegin.Size = new System.Drawing.Size(170, 21);
            this.checkBoxFromBegin.TabIndex = 34;
            this.checkBoxFromBegin.Text = "Генерация со старта";
            this.checkBoxFromBegin.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(114, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 17);
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
            this.checkBoxFinish.Location = new System.Drawing.Point(2, 121);
            this.checkBoxFinish.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxFinish.Name = "checkBoxFinish";
            this.checkBoxFinish.Size = new System.Drawing.Size(112, 21);
            this.checkBoxFinish.TabIndex = 21;
            this.checkBoxFinish.Text = "Свой финиш";
            this.checkBoxFinish.UseVisualStyleBackColor = true;
            this.checkBoxFinish.CheckedChanged += new System.EventHandler(this.checkBoxFinish_CheckedChanged);
            // 
            // checkBoxStart
            // 
            this.checkBoxStart.AutoSize = true;
            this.checkBoxStart.Location = new System.Drawing.Point(2, 96);
            this.checkBoxStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxStart.Name = "checkBoxStart";
            this.checkBoxStart.Size = new System.Drawing.Size(103, 21);
            this.checkBoxStart.TabIndex = 16;
            this.checkBoxStart.Text = "Свой старт";
            this.checkBoxStart.UseVisualStyleBackColor = true;
            this.checkBoxStart.CheckedChanged += new System.EventHandler(this.checkBoxStart_CheckedChanged);
            // 
            // buttonMazeGeneration
            // 
            this.buttonMazeGeneration.Location = new System.Drawing.Point(152, 55);
            this.buttonMazeGeneration.Name = "buttonMazeGeneration";
            this.buttonMazeGeneration.Size = new System.Drawing.Size(146, 58);
            this.buttonMazeGeneration.TabIndex = 55;
            this.buttonMazeGeneration.Text = "Генерация лабиринта";
            this.buttonMazeGeneration.UseVisualStyleBackColor = true;
            this.buttonMazeGeneration.Click += new System.EventHandler(this.buttonMazeGeneration_Click);
            // 
            // panelMazeGenerationMethod
            // 
            this.panelMazeGenerationMethod.Controls.Add(this.radioButtonHuntAndKill);
            this.panelMazeGenerationMethod.Controls.Add(this.radioButtonBackTracking);
            this.panelMazeGenerationMethod.Location = new System.Drawing.Point(8, 55);
            this.panelMazeGenerationMethod.Name = "panelMazeGenerationMethod";
            this.panelMazeGenerationMethod.Size = new System.Drawing.Size(122, 58);
            this.panelMazeGenerationMethod.TabIndex = 54;
            // 
            // radioButtonHuntAndKill
            // 
            this.radioButtonHuntAndKill.AutoSize = true;
            this.radioButtonHuntAndKill.Location = new System.Drawing.Point(3, 30);
            this.radioButtonHuntAndKill.Name = "radioButtonHuntAndKill";
            this.radioButtonHuntAndKill.Size = new System.Drawing.Size(109, 21);
            this.radioButtonHuntAndKill.TabIndex = 1;
            this.radioButtonHuntAndKill.Text = "Hunt and Kill";
            this.radioButtonHuntAndKill.UseVisualStyleBackColor = true;
            // 
            // radioButtonBackTracking
            // 
            this.radioButtonBackTracking.AutoSize = true;
            this.radioButtonBackTracking.Checked = true;
            this.radioButtonBackTracking.Location = new System.Drawing.Point(3, 3);
            this.radioButtonBackTracking.Name = "radioButtonBackTracking";
            this.radioButtonBackTracking.Size = new System.Drawing.Size(110, 21);
            this.radioButtonBackTracking.TabIndex = 0;
            this.radioButtonBackTracking.TabStop = true;
            this.radioButtonBackTracking.Text = "Backtracking";
            this.radioButtonBackTracking.UseVisualStyleBackColor = true;
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
            this.groupBoxDrawFeatures.Location = new System.Drawing.Point(6, 647);
            this.groupBoxDrawFeatures.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxDrawFeatures.Name = "groupBoxDrawFeatures";
            this.groupBoxDrawFeatures.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxDrawFeatures.Size = new System.Drawing.Size(317, 156);
            this.groupBoxDrawFeatures.TabIndex = 46;
            this.groupBoxDrawFeatures.TabStop = false;
            this.groupBoxDrawFeatures.Visible = false;
            // 
            // radioButtonFeatureDarkStyle
            // 
            this.radioButtonFeatureDarkStyle.AutoSize = true;
            this.radioButtonFeatureDarkStyle.Location = new System.Drawing.Point(183, 37);
            this.radioButtonFeatureDarkStyle.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFeatureDarkStyle.Name = "radioButtonFeatureDarkStyle";
            this.radioButtonFeatureDarkStyle.Size = new System.Drawing.Size(122, 21);
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
            this.radioButtonFeatureLightStyle.Size = new System.Drawing.Size(127, 21);
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
            this.radioButtonFeatureYellow.Size = new System.Drawing.Size(83, 21);
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
            this.radioButtonFeatureTurquoise.Size = new System.Drawing.Size(104, 21);
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
            this.radioButtonFeatureViolet.Size = new System.Drawing.Size(114, 21);
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
            this.radioButtonFeatureBlue.Size = new System.Drawing.Size(120, 21);
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
            this.radioButtonFeatureGreen.Size = new System.Drawing.Size(138, 21);
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
            this.radioButtonFeatureRed.Size = new System.Drawing.Size(137, 21);
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
            this.radioButtonFeatureFiftyShades.Size = new System.Drawing.Size(157, 21);
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
            this.radioButtonFeatureChaos.Size = new System.Drawing.Size(99, 21);
            this.radioButtonFeatureChaos.TabIndex = 44;
            this.radioButtonFeatureChaos.TabStop = true;
            this.radioButtonFeatureChaos.Text = "Цветохаос";
            this.radioButtonFeatureChaos.UseVisualStyleBackColor = true;
            // 
            // checkBoxFeatureUse
            // 
            this.checkBoxFeatureUse.AutoSize = true;
            this.checkBoxFeatureUse.Location = new System.Drawing.Point(11, 627);
            this.checkBoxFeatureUse.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxFeatureUse.Name = "checkBoxFeatureUse";
            this.checkBoxFeatureUse.Size = new System.Drawing.Size(233, 21);
            this.checkBoxFeatureUse.TabIndex = 43;
            this.checkBoxFeatureUse.Text = "Использовать фичи отрисовки";
            this.checkBoxFeatureUse.UseVisualStyleBackColor = true;
            this.checkBoxFeatureUse.CheckedChanged += new System.EventHandler(this.checkBoxFeatureUse_CheckedChanged);
            // 
            // radioButtonRandR
            // 
            this.radioButtonRandR.AutoSize = true;
            this.radioButtonRandR.Location = new System.Drawing.Point(4, 444);
            this.radioButtonRandR.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonRandR.Name = "radioButtonRandR";
            this.radioButtonRandR.Size = new System.Drawing.Size(171, 21);
            this.radioButtonRandR.TabIndex = 42;
            this.radioButtonRandR.Text = "Случайные повороты";
            this.radioButtonRandR.UseVisualStyleBackColor = true;
            // 
            // radioButtonRR
            // 
            this.radioButtonRR.AutoSize = true;
            this.radioButtonRR.Location = new System.Drawing.Point(4, 416);
            this.radioButtonRR.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonRR.Name = "radioButtonRR";
            this.radioButtonRR.Size = new System.Drawing.Size(148, 21);
            this.radioButtonRR.TabIndex = 41;
            this.radioButtonRR.Text = "Правые повороты";
            this.radioButtonRR.UseVisualStyleBackColor = true;
            // 
            // radioButtonLR
            // 
            this.radioButtonLR.AutoSize = true;
            this.radioButtonLR.Checked = true;
            this.radioButtonLR.Cursor = System.Windows.Forms.Cursors.Default;
            this.radioButtonLR.Location = new System.Drawing.Point(4, 389);
            this.radioButtonLR.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonLR.Name = "radioButtonLR";
            this.radioButtonLR.Size = new System.Drawing.Size(140, 21);
            this.radioButtonLR.TabIndex = 40;
            this.radioButtonLR.TabStop = true;
            this.radioButtonLR.Text = "Левые повороты";
            this.radioButtonLR.UseVisualStyleBackColor = true;
            // 
            // buttonSolverStart
            // 
            this.buttonSolverStart.Enabled = false;
            this.buttonSolverStart.Location = new System.Drawing.Point(185, 395);
            this.buttonSolverStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSolverStart.Name = "buttonSolverStart";
            this.buttonSolverStart.Size = new System.Drawing.Size(139, 47);
            this.buttonSolverStart.TabIndex = 39;
            this.buttonSolverStart.Text = "Решить лабиринт";
            this.buttonSolverStart.UseVisualStyleBackColor = true;
            this.buttonSolverStart.Click += new System.EventHandler(this.buttonSolverStart_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(167, 585);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "Кол-во файлов";
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(274, 582);
            this.textBoxCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(52, 22);
            this.textBoxCount.TabIndex = 30;
            this.textBoxCount.Text = "100";
            // 
            // buttonGenerateBatch
            // 
            this.buttonGenerateBatch.Location = new System.Drawing.Point(172, 502);
            this.buttonGenerateBatch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGenerateBatch.Name = "buttonGenerateBatch";
            this.buttonGenerateBatch.Size = new System.Drawing.Size(153, 49);
            this.buttonGenerateBatch.TabIndex = 28;
            this.buttonGenerateBatch.Text = "Генерация набора файлов";
            this.buttonGenerateBatch.UseVisualStyleBackColor = true;
            this.buttonGenerateBatch.Click += new System.EventHandler(this.ButtonGenerateBatch_Click);
            // 
            // checkBoxWithSolution
            // 
            this.checkBoxWithSolution.AutoSize = true;
            this.checkBoxWithSolution.Location = new System.Drawing.Point(11, 557);
            this.checkBoxWithSolution.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxWithSolution.Name = "checkBoxWithSolution";
            this.checkBoxWithSolution.Size = new System.Drawing.Size(191, 21);
            this.checkBoxWithSolution.TabIndex = 26;
            this.checkBoxWithSolution.Text = "Нарисовать с решением";
            this.checkBoxWithSolution.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 585);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "Размер квадрата";
            // 
            // textBoxSize
            // 
            this.textBoxSize.Location = new System.Drawing.Point(128, 582);
            this.textBoxSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(34, 22);
            this.textBoxSize.TabIndex = 24;
            this.textBoxSize.Text = "10";
            // 
            // buttonGenPictire
            // 
            this.buttonGenPictire.Location = new System.Drawing.Point(11, 502);
            this.buttonGenPictire.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGenPictire.Name = "buttonGenPictire";
            this.buttonGenPictire.Size = new System.Drawing.Size(155, 49);
            this.buttonGenPictire.TabIndex = 23;
            this.buttonGenPictire.Text = "Сгенерировать изображение";
            this.buttonGenPictire.UseVisualStyleBackColor = true;
            this.buttonGenPictire.Click += new System.EventHandler(this.ButtonGenPicture_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Задержка отрисовки";
            // 
            // textBoxSleep
            // 
            this.textBoxSleep.Location = new System.Drawing.Point(164, 120);
            this.textBoxSleep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSleep.Name = "textBoxSleep";
            this.textBoxSleep.Size = new System.Drawing.Size(63, 22);
            this.textBoxSleep.TabIndex = 6;
            this.textBoxSleep.Text = "0";
            // 
            // MazeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1548, 982);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBoxLabirint);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(398, 196);
            this.Name = "MazeForm";
            this.Text = "MazeGenerator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.MazeForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLabirint)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxGenerationAdditionalParams.ResumeLayout(false);
            this.groupBoxGenerationAdditionalParams.PerformLayout();
            this.panelMazeGenerationMethod.ResumeLayout(false);
            this.panelMazeGenerationMethod.PerformLayout();
            this.groupBoxDrawFeatures.ResumeLayout(false);
            this.groupBoxDrawFeatures.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLabirint;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSleep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxStartY;
        private System.Windows.Forms.TextBox textBoxStartX;
        private System.Windows.Forms.CheckBox checkBoxFinish;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxEndY;
        private System.Windows.Forms.TextBox textBoxEndX;
        private System.Windows.Forms.CheckBox checkBoxStart;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.CheckBox checkBoxFromBegin;
        private System.Windows.Forms.TextBox textBoxEmptyPlacesProb;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton radioButtonRandR;
        private System.Windows.Forms.RadioButton radioButtonRR;
        private System.Windows.Forms.RadioButton radioButtonLR;
        private System.Windows.Forms.Button buttonSolverStart;
        private System.Windows.Forms.RadioButton radioButtonFeatureFiftyShades;
        private System.Windows.Forms.RadioButton radioButtonFeatureChaos;
        private System.Windows.Forms.CheckBox checkBoxFeatureUse;
        private System.Windows.Forms.GroupBox groupBoxDrawFeatures;
        private System.Windows.Forms.RadioButton radioButtonFeatureRed;
        private System.Windows.Forms.RadioButton radioButtonFeatureBlue;
        private System.Windows.Forms.RadioButton radioButtonFeatureGreen;
        private System.Windows.Forms.TextBox textBoxWhiteSpaceProb;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton radioButtonFeatureYellow;
        private System.Windows.Forms.RadioButton radioButtonFeatureTurquoise;
        private System.Windows.Forms.RadioButton radioButtonFeatureViolet;
        private System.Windows.Forms.RadioButton radioButtonFeatureLightStyle;
        private System.Windows.Forms.RadioButton radioButtonFeatureDarkStyle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonGenerateBatch;
        private System.Windows.Forms.CheckBox checkBoxWithSolution;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.Button buttonGenPictire;
        private System.Windows.Forms.RadioButton radioButtonHuntAndKill;
        private System.Windows.Forms.RadioButton radioButtonBackTracking;
        private System.Windows.Forms.Panel panelMazeGenerationMethod;
        private System.Windows.Forms.Button buttonMazeGeneration;
        private System.Windows.Forms.CheckBox checkBoxAdditionalGeneration;
        private System.Windows.Forms.GroupBox groupBoxGenerationAdditionalParams;
    }
}

