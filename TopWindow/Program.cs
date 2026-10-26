using System.Windows.Forms;

namespace TopWindow
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int monitor_count = Screen.AllScreens.Length;
            int total_width = 0;
            int total_height = 0;

            foreach (Screen screen in Screen.AllScreens)
            {
                total_width += screen.Bounds.Width;
                total_height += screen.Bounds.Height;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Form1 window = new Form1();
            window.Size = new Size(total_width, total_height);
            window.StartPosition = FormStartPosition.CenterScreen;
            window.TopMost = true;

            window.ControlBox = false;
            window.FormBorderStyle = FormBorderStyle.None;

            window.Show();
            //ApplicationConfiguration.Initialize();
            Application.Run(window);
        }
    }
}