using System;
using System.Threading;
using System.Windows.Forms;

namespace TimerConfig
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                const string APPGUID = "{4EC0A132-A1CB-4959-9C23-25157FFAE498}";
                using (Mutex mutex = new Mutex(false, APPGUID))
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        MessageBox.Show($"An instance of {Utilities.Constants.CommonName}Config is already running...", $"{Utilities.Constants.CommonName}Config", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    Application.Run(new MainForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
