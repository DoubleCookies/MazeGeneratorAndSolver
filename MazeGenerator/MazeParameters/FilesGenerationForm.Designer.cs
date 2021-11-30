
namespace MazeGenerator
{
    partial class FilesGenerationForm
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
            this.buttonCreate = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.checkBoxWithSolution = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(42, 135);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(153, 54);
            this.buttonCreate.TabIndex = 0;
            this.buttonCreate.Text = "Создать";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 17);
            this.label9.TabIndex = 36;
            this.label9.Text = "Кол-во файлов";
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(166, 79);
            this.textBoxCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(42, 22);
            this.textBoxCount.TabIndex = 35;
            this.textBoxCount.Text = "1";
            // 
            // checkBoxWithSolution
            // 
            this.checkBoxWithSolution.AutoSize = true;
            this.checkBoxWithSolution.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxWithSolution.Location = new System.Drawing.Point(17, 15);
            this.checkBoxWithSolution.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxWithSolution.Name = "checkBoxWithSolution";
            this.checkBoxWithSolution.Size = new System.Drawing.Size(191, 21);
            this.checkBoxWithSolution.TabIndex = 34;
            this.checkBoxWithSolution.Text = "Нарисовать с решением";
            this.checkBoxWithSolution.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 17);
            this.label8.TabIndex = 33;
            this.label8.Text = "Размер квадрата";
            // 
            // textBoxSize
            // 
            this.textBoxSize.Location = new System.Drawing.Point(166, 45);
            this.textBoxSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(42, 22);
            this.textBoxSize.TabIndex = 32;
            this.textBoxSize.Text = "10";
            // 
            // FilesGenerationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 211);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.checkBoxWithSolution);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.buttonCreate);
            this.Name = "FilesGenerationForm";
            this.Text = "Генерация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.CheckBox checkBoxWithSolution;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxSize;
    }
}