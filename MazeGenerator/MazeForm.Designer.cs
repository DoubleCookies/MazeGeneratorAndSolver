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
            this.buttonFilesGenerate = new System.Windows.Forms.Button();
            this.buttonMazeGeneration = new System.Windows.Forms.Button();
            this.panelMazeGenerationMethod = new System.Windows.Forms.Panel();
            this.radioButtonHuntAndKill = new System.Windows.Forms.RadioButton();
            this.radioButtonBackTracking = new System.Windows.Forms.RadioButton();
            this.radioButtonRandR = new System.Windows.Forms.RadioButton();
            this.radioButtonRR = new System.Windows.Forms.RadioButton();
            this.radioButtonLR = new System.Windows.Forms.RadioButton();
            this.buttonSolverStart = new System.Windows.Forms.Button();
            this.buttonAdditionalParams = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLabirint)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panelMazeGenerationMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxLabirint
            // 
            this.pictureBoxLabirint.BackColor = System.Drawing.SystemColors.Control;
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
            this.groupBox1.Controls.Add(this.buttonAdditionalParams);
            this.groupBox1.Controls.Add(this.buttonFilesGenerate);
            this.groupBox1.Controls.Add(this.buttonMazeGeneration);
            this.groupBox1.Controls.Add(this.panelMazeGenerationMethod);
            this.groupBox1.Controls.Add(this.radioButtonRandR);
            this.groupBox1.Controls.Add(this.radioButtonRR);
            this.groupBox1.Controls.Add(this.radioButtonLR);
            this.groupBox1.Controls.Add(this.buttonSolverStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxHeight);
            this.groupBox1.Controls.Add(this.textBoxWidth);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(1217, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(331, 982);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // buttonFilesGenerate
            // 
            this.buttonFilesGenerate.Location = new System.Drawing.Point(4, 307);
            this.buttonFilesGenerate.Name = "buttonFilesGenerate";
            this.buttonFilesGenerate.Size = new System.Drawing.Size(299, 48);
            this.buttonFilesGenerate.TabIndex = 58;
            this.buttonFilesGenerate.Text = "Создание файлов";
            this.buttonFilesGenerate.UseVisualStyleBackColor = true;
            this.buttonFilesGenerate.Click += new System.EventHandler(this.ButtonFilesGenerate_Click);
            // 
            // buttonMazeGeneration
            // 
            this.buttonMazeGeneration.Location = new System.Drawing.Point(152, 55);
            this.buttonMazeGeneration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMazeGeneration.Name = "buttonMazeGeneration";
            this.buttonMazeGeneration.Size = new System.Drawing.Size(147, 58);
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
            this.panelMazeGenerationMethod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMazeGenerationMethod.Name = "panelMazeGenerationMethod";
            this.panelMazeGenerationMethod.Size = new System.Drawing.Size(123, 58);
            this.panelMazeGenerationMethod.TabIndex = 54;
            // 
            // radioButtonHuntAndKill
            // 
            this.radioButtonHuntAndKill.AutoSize = true;
            this.radioButtonHuntAndKill.Location = new System.Drawing.Point(3, 30);
            this.radioButtonHuntAndKill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.radioButtonBackTracking.Location = new System.Drawing.Point(3, 2);
            this.radioButtonBackTracking.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButtonBackTracking.Name = "radioButtonBackTracking";
            this.radioButtonBackTracking.Size = new System.Drawing.Size(110, 21);
            this.radioButtonBackTracking.TabIndex = 0;
            this.radioButtonBackTracking.TabStop = true;
            this.radioButtonBackTracking.Text = "Backtracking";
            this.radioButtonBackTracking.UseVisualStyleBackColor = true;
            // 
            // radioButtonRandR
            // 
            this.radioButtonRandR.AutoSize = true;
            this.radioButtonRandR.Location = new System.Drawing.Point(4, 262);
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
            this.radioButtonRR.Location = new System.Drawing.Point(4, 234);
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
            this.radioButtonLR.Location = new System.Drawing.Point(4, 207);
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
            this.buttonSolverStart.Location = new System.Drawing.Point(152, 207);
            this.buttonSolverStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSolverStart.Name = "buttonSolverStart";
            this.buttonSolverStart.Size = new System.Drawing.Size(151, 47);
            this.buttonSolverStart.TabIndex = 39;
            this.buttonSolverStart.Text = "Решить лабиринт";
            this.buttonSolverStart.UseVisualStyleBackColor = true;
            this.buttonSolverStart.Click += new System.EventHandler(this.buttonSolverStart_Click);
            // 
            // buttonAdditionalParams
            // 
            this.buttonAdditionalParams.Location = new System.Drawing.Point(152, 128);
            this.buttonAdditionalParams.Name = "buttonAdditionalParams";
            this.buttonAdditionalParams.Size = new System.Drawing.Size(147, 53);
            this.buttonAdditionalParams.TabIndex = 59;
            this.buttonAdditionalParams.Text = "Дополнительные параметры";
            this.buttonAdditionalParams.UseVisualStyleBackColor = true;
            this.buttonAdditionalParams.Click += new System.EventHandler(this.ButtonAdditionalParams_Click);
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
            this.MinimumSize = new System.Drawing.Size(397, 195);
            this.Name = "MazeForm";
            this.Text = "MazeGenerator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.MazeForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLabirint)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelMazeGenerationMethod.ResumeLayout(false);
            this.panelMazeGenerationMethod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLabirint;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonRandR;
        private System.Windows.Forms.RadioButton radioButtonRR;
        private System.Windows.Forms.RadioButton radioButtonLR;
        private System.Windows.Forms.Button buttonSolverStart;
        private System.Windows.Forms.RadioButton radioButtonHuntAndKill;
        private System.Windows.Forms.RadioButton radioButtonBackTracking;
        private System.Windows.Forms.Panel panelMazeGenerationMethod;
        private System.Windows.Forms.Button buttonMazeGeneration;
        private System.Windows.Forms.Button buttonFilesGenerate;
        private System.Windows.Forms.Button buttonAdditionalParams;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

