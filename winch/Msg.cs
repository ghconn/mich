using System.Drawing;
using System.Windows.Forms;

namespace winch
{
    public partial class Msg : DialogBase
    {
        public Msg()
        {
            InitializeComponent();
        }

        public Msg(string msg)
        {
            InitializeComponent();

            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width - 640, Screen.PrimaryScreen.WorkingArea.Height - 60);
            label1.Text = msg;
            Width = label1.Width + 120;
            Height = label1.Height + 90;
            label1.Location = new Point(60, 20);
            ShowDialog();
        }
    }
}
