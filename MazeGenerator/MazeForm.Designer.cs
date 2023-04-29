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
            this.comboBoxSolveMethods = new System.Windows.Forms.ComboBox();
            this.labelSolveMethod = new System.Windows.Forms.Label();
            this.labelGenerationMethod = new System.Windows.Forms.Label();
            this.comboBoxGenerationMethods = new System.Windows.Forms.ComboBox();
            this.buttonAdditionalParams = new System.Windows.Forms.Button();
            this.buttonFilesGenerate = new System.Windows.Forms.Button();
            this.buttonMazeGeneration = new System.Windows.Forms.Button();
            this.buttonSolverStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLabirint)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ширина";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Высота";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxSolveMethods);
            this.groupBox1.Controls.Add(this.labelSolveMethod);
            this.groupBox1.Controls.Add(this.labelGenerationMethod);
            this.groupBox1.Controls.Add(this.comboBoxGenerationMethods);
            this.groupBox1.Controls.Add(this.buttonAdditionalParams);
            this.groupBox1.Controls.Add(this.buttonFilesGenerate);
            this.groupBox1.Controls.Add(this.buttonMazeGeneration);
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
            // comboBoxSolveMethods
            // 
            this.comboBoxSolveMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSolveMethods.FormattingEnabled = true;
            this.comboBoxSolveMethods.Items.AddRange(new object[] {
            "Левые повороты",
            "Правые повороты",
            "Случайные повороты"});
            this.comboBoxSolveMethods.Location = new System.Drawing.Point(8, 296);
            this.comboBoxSolveMethods.Name = "comboBoxSolveMethods";
            this.comboBoxSolveMethods.Size = new System.Drawing.Size(151, 24);
            this.comboBoxSolveMethods.TabIndex = 63;
            // 
            // labelSolveMethod
            // 
            this.labelSolveMethod.AutoSize = true;
            this.labelSolveMethod.Location = new System.Drawing.Point(10, 273);
            this.labelSolveMethod.Name = "labelSolveMethod";
            this.labelSolveMethod.Size = new System.Drawing.Size(108, 16);
            this.labelSolveMethod.TabIndex = 62;
            this.labelSolveMethod.Text = "Метод решения";
            // 
            // labelGenerationMethod
            // 
            this.labelGenerationMethod.AutoSize = true;
            this.labelGenerationMethod.Location = new System.Drawing.Point(7, 65);
            this.labelGenerationMethod.Name = "labelGenerationMethod";
            this.labelGenerationMethod.Size = new System.Drawing.Size(122, 16);
            this.labelGenerationMethod.TabIndex = 61;
            this.labelGenerationMethod.Text = "Метод генерации";
            // 
            // comboBoxGenerationMethods
            // 
            this.comboBoxGenerationMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGenerationMethods.FormattingEnabled = true;
            this.comboBoxGenerationMethods.Items.AddRange(new object[] {
            "Eller",
            "Backtracking",
            "Hunt and Kill"});
            this.comboBoxGenerationMethods.Location = new System.Drawing.Point(8, 89);
            this.comboBoxGenerationMethods.Name = "comboBoxGenerationMethods";
            this.comboBoxGenerationMethods.Size = new System.Drawing.Size(144, 24);
            this.comboBoxGenerationMethods.TabIndex = 60;
            // 
            // buttonAdditionalParams
            // 
            this.buttonAdditionalParams.Location = new System.Drawing.Point(171, 131);
            this.buttonAdditionalParams.Name = "buttonAdditionalParams";
            this.buttonAdditionalParams.Size = new System.Drawing.Size(147, 53);
            this.buttonAdditionalParams.TabIndex = 59;
            this.buttonAdditionalParams.Text = "Дополнительные параметры";
            this.buttonAdditionalParams.UseVisualStyleBackColor = true;
            this.buttonAdditionalParams.Click += new System.EventHandler(this.ButtonAdditionalParams_Click);
            // 
            // buttonFilesGenerate
            // 
            this.buttonFilesGenerate.Location = new System.Drawing.Point(19, 481);
            this.buttonFilesGenerate.Name = "buttonFilesGenerate";
            this.buttonFilesGenerate.Size = new System.Drawing.Size(299, 48);
            this.buttonFilesGenerate.TabIndex = 58;
            this.buttonFilesGenerate.Text = "Создание файлов";
            this.buttonFilesGenerate.UseVisualStyleBackColor = true;
            this.buttonFilesGenerate.Click += new System.EventHandler(this.ButtonFilesGenerate_Click);
            // 
            // buttonMazeGeneration
            // 
            this.buttonMazeGeneration.Location = new System.Drawing.Point(171, 55);
            this.buttonMazeGeneration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMazeGeneration.Name = "buttonMazeGeneration";
            this.buttonMazeGeneration.Size = new System.Drawing.Size(147, 58);
            this.buttonMazeGeneration.TabIndex = 55;
            this.buttonMazeGeneration.Text = "Генерация лабиринта";
            this.buttonMazeGeneration.UseVisualStyleBackColor = true;
            this.buttonMazeGeneration.Click += new System.EventHandler(this.buttonMazeGeneration_Click);
            // 
            // buttonSolverStart
            // 
            this.buttonSolverStart.Enabled = false;
            this.buttonSolverStart.Location = new System.Drawing.Point(171, 273);
            this.buttonSolverStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSolverStart.Name = "buttonSolverStart";
            this.buttonSolverStart.Size = new System.Drawing.Size(147, 47);
            this.buttonSolverStart.TabIndex = 39;
            this.buttonSolverStart.Text = "Решить лабиринт";
            this.buttonSolverStart.UseVisualStyleBackColor = true;
            this.buttonSolverStart.Click += new System.EventHandler(this.buttonSolverStart_Click);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLabirint;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSolverStart;
        private System.Windows.Forms.Button buttonMazeGeneration;
        private System.Windows.Forms.Button buttonFilesGenerate;
        private System.Windows.Forms.Button buttonAdditionalParams;
        private System.Windows.Forms.ComboBox comboBoxGenerationMethods;
        private System.Windows.Forms.ComboBox comboBoxSolveMethods;
        private System.Windows.Forms.Label labelSolveMethod;
        private System.Windows.Forms.Label labelGenerationMethod;
    }
}

