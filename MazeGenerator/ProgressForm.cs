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
