using System;
using System.IO;
using System.Threading.Tasks;

namespace TopWindow
{
    public partial class Form1 : Form
    {
        bool startedDelete = false;
        public Form1()
        {
            InitializeComponent();
        }
        private async Task FakeDeleteScanAsync(string rootPath)
        {
            lstOutput.Items.Clear();

            var directories = new Stack<string>();
            directories.Push(rootPath);

            while (directories.Count > 0)
            {
                string directory = directories.Pop();

                try
                {
                    foreach (string file in Directory.EnumerateFiles(directory))
                    {
                        lstOutput.Items.Add($"Deleting {file}...");

                        // Keep the newest entry visible
                        lstOutput.TopIndex = lstOutput.Items.Count - 1;

                        await Task.Delay(40);
                    }

                    foreach (string subdirectory in Directory.EnumerateDirectories(directory))
                    {
                        directories.Push(subdirectory);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // Skip directories the program cannot access
                }
                catch (IOException)
                {
                    // Skip inaccessible or unavailable paths
                }
            }

            //lstOutput.Items.Add("Operation complete.");
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            double d = 100.0;

            double mouseX = e.X;
            double mouseY = e.Y;

            double btnX = btn_close.Left + btn_close.Width / 2.0;
            double btnY = btn_close.Top + btn_close.Height / 2.0;

            double dx = btnX - mouseX;
            double dy = btnY - mouseY;

            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance >= d)
                return;

            // Handle the mouse being exactly at the button center
            if (distance < 0.001)
            {
                dx = 1;
                dy = 0;
                distance = 1;
            }

            // Normalize the repulsion vector
            dx /= distance;
            dy /= distance;

            // Proposed position: push away from the mouse
            double newX = mouseX + dx * d;
            double newY = mouseY + dy * d;

            double halfW = btn_close.Width / 2.0;
            double halfH = btn_close.Height / 2.0;

            double minX = halfW;
            double maxX = ClientSize.Width - halfW;
            double minY = halfH;
            double maxY = ClientSize.Height - halfH;

            // If the position is outside, teleport across the mouse
            if (newX < minX || newX > maxX ||
                newY < minY || newY > maxY)
            {
                // Reverse the repulsion vector to cross the mouse
                newX = mouseX - dx * d;
                newY = mouseY - dy * d;

                // If still outside, move toward the center instead
                if (newX < minX || newX > maxX ||
                    newY < minY || newY > maxY)
                {
                    double centerX = ClientSize.Width / 2.0;
                    double centerY = ClientSize.Height / 2.0;

                    double cx = centerX - newX;
                    double cy = centerY - newY;
                    double len = Math.Sqrt(cx * cx + cy * cy);

                    if (len > 0.001)
                    {
                        cx /= len;
                        cy /= len;
                    }

                    // Put it safely inside, away from the edge
                    double margin = d;

                    newX = Math.Max(minX + margin,
                           Math.Min(maxX - margin, centerX + cx * margin));

                    newY = Math.Max(minY + margin,
                           Math.Min(maxY - margin, centerY + cy * margin));
                }
            }

            int left = (int)Math.Round(newX - halfW);
            int top = (int)Math.Round(newY - halfH);

            // Final safety clamp
            left = Math.Max(0, Math.Min(ClientSize.Width - btn_close.Width, left));
            top = Math.Max(0, Math.Min(ClientSize.Height - btn_close.Height, top));

            btn_close.Location = new Point(left, top);
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            string? systemDrive = Path.GetPathRoot(Environment.SystemDirectory);
            if (systemDrive == null)
            {
                systemDrive = @"C:\";
            }
            await FakeDeleteScanAsync(systemDrive);
        }

        private void lstOutput_MouseMove(object sender, MouseEventArgs e)
        {
            Point formPosition = this.PointToClient(lstOutput.PointToScreen(e.Location));

            Form1_MouseMove(this, new MouseEventArgs(e.Button, e.Clicks, formPosition.X, formPosition.Y, e.Delta));
        }
    }
}
