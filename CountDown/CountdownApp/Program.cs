using System;
using System.Threading;
using System.Windows.Forms;

namespace TimerApp
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
                const string APPGUID = "{20F7B46E-3D94-4763-AAA1-49CAFCE2A616}";
                using (Mutex mutex = new Mutex(false, APPGUID))
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        MessageBox.Show($"An instance of {Utilities.Constants.CommonName}App is already running...", $"{Utilities.Constants.CommonName}App", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    Application.Run(new DisplayForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
