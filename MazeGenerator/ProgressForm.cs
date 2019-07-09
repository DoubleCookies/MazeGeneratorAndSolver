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
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public void Init(int count)
        {
            progressBarGenerateProgress.Value = 0;
            progressBarGenerateProgress.Maximum = count;
        }

        public void ProgressBarUpdate()
        {
            progressBarGenerateProgress.Value++;
        }
    }
}
