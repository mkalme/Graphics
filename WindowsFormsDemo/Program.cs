using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Graphics;
using PathTracingGraphics;

namespace WindowsFormsDemo {
    static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImageViewerForm(RenderImage()));
        }

        private static Image RenderImage() {
            LockedBitmap output = new LockedBitmap(Color.Black, 1000, 1000);

            PathTracingRenderer renderer = new PathTracingRenderer();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            renderer.Render(output);

            watch.Stop();
            Console.WriteLine($"Elapsed milliseconds: {watch.ElapsedMilliseconds}");

            output.Unlock();
            Clipboard.SetImage(output.Source);

            return output.Source;
        }
    }
}
