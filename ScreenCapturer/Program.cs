using ScreenCapturer.Localizations;

namespace ScreenCapturer
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// Starting point of the program.
    /// "Single application" solution from http://sanity-free.org/143/csharp_dotnet_single_instance_application.html
    /// </summary>
    public static class Program
    {
        static readonly Mutex Mutex = new Mutex(true, "{628d6b64-163e-471d-8227-5d43386512e1}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            if (Mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
                Mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show(strings.dialog_MultipleInstance_Content, strings.program_Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
