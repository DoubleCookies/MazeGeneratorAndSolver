namespace MazeGenerator
{
    partial class ProgressForm
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
            this.progressBarGenerateProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // progressBarGenerateProgress
            // 
            this.progressBarGenerateProgress.Location = new System.Drawing.Point(17, 12);
            this.progressBarGenerateProgress.Name = "progressBarGenerateProgress";
            this.progressBarGenerateProgress.Size = new System.Drawing.Size(553, 47);
            this.progressBarGenerateProgress.TabIndex = 0;
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 71);
            this.Controls.Add(this.progressBarGenerateProgress);
            this.Name = "ProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Идёт процесс генерации файлов...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarGenerateProgress;
    }
}