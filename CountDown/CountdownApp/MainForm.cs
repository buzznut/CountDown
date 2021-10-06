using System;
using System.Windows.Forms;
using Utilities;

namespace TimerApp
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private DisplayForm displayForm;
        private readonly static string keyPath = $"SOFTWARE\\{Constants.CompanyName}\\{Constants.CommonName}";
        private bool isRunning;

        public MainForm()
        {
            InitializeComponent();

            displayForm = new DisplayForm();
            ShowInTaskbar = false;
            notifyIconTray.Visible = true;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                notifyIconTray.Visible = true;
            }
            else
            {
                notifyIconTray.Visible = false;
                ShowInTaskbar = true;
            }
        }

        private void notifyIconTray_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void notifyIconTray_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            displayForm.Show();
            isRunning = true;
        }
    }
}
