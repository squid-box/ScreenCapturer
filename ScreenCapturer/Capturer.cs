namespace ScreenCapturer
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    using Ionic.Zip;

    /// <summary>
    /// The Capturer performs anything related to screencapturing and handling these captures.
    /// </summary>
    public class Capturer
    {
        #region Fields
        /// <summary>
        /// Holds screenshots and their timestamp.
        /// </summary>
        internal readonly Queue<KeyValuePair<DateTime, Bitmap>> Shots;

        /// <summary>
        /// Number of screenshots to save.
        /// </summary>
        private const int Buffersize = 32;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Capturer"/> class.
        /// </summary>
        internal Capturer()
        {
            this.Shots = new Queue<KeyValuePair<DateTime, Bitmap>>(Buffersize);
        }

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether or not the program is capturing screenshots.
        /// </summary>
        internal bool IsTakingShots { get; set; }

        #endregion

        /// <summary>
        /// Takes Shots, keeping the queue correct.
        /// </summary>
        /// <param name="e">Event data of the mouse event that triggered this shot.</param>
        internal void TakeShot(MouseEventArgs e)
        {
            if (this.IsTakingShots)
            {
                if (this.Shots.Count >= Buffersize)
                {
                    this.Shots.Dequeue();
                }

                this.Shots.Enqueue(new KeyValuePair<DateTime, Bitmap>(DateTime.Now, this.GrabScreenshot(e)));
            }
        }
        
        /// <summary>
        /// Saves screenshots currently in buffer to a .zip-file named with todays date.
        /// </summary>
        /// <param name="path">Path (absolute or relative) where .zip will be placed.</param>
        /// <returns>Path to saved file.</returns>
        internal string SaveShots(string path)
        {
            // Pause screenshotting
            this.IsTakingShots = false;

            if (this.Shots.Count == 0)
            {
                return null;
            }

            var filenames = new List<string>();

            var i = 1;
            foreach (KeyValuePair<DateTime, Bitmap> shot in this.Shots)
            {
                string filename = i + "_" + this.DateToFileString(shot.Key) + ".png";
                shot.Value.Save(filename, ImageFormat.Png);
                filenames.Add(filename);
                i++;
            }

            this.Shots.Clear();

            var numberOfSaves = 1;
            var savePath = string.Format("{0}\\screenshots_{1}_{2}.zip", path, DateTime.Today.ToShortDateString(), numberOfSaves);

            // Start zipping it all up!
            using (var zip = new ZipFile())
            {
                zip.AddFiles(filenames);

                // Check if path exists, if not; create it.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                while (File.Exists(savePath))
                {
                    numberOfSaves++;
                    savePath = string.Format("{0}\\screenshots_{1}_{2}.zip", path, DateTime.Today.ToShortDateString(), numberOfSaves);
                }

                zip.Save(savePath);
            }

            // Remove files from folder (they're in the zip now)
            foreach (var s in filenames)
            {
                File.Delete(s);
            }

            // Resume screenshotting
            this.IsTakingShots = true;

            // Clear buffer
            this.Shots.Clear();

            return savePath;
        }

        /// <summary>
        /// Draws an icon indicating where mouse click was performed.
        /// </summary>
        /// <param name="target">Graphics object to draw icon on.</param>
        /// <param name="mouseEvent">Event triggering screenshot, includes X and Y values.</param>
        private static void DrawMouseClickIcon(ref Graphics target, MouseEventArgs mouseEvent)
        {
            const int Diameter = 16;
            var pen = new Pen(Color.Red, 2.0f);
            target.DrawEllipse(pen, mouseEvent.X - (Diameter / 2), mouseEvent.Y - (Diameter / 2), Diameter, Diameter);
            target.FillRectangle(new SolidBrush(Color.Red), mouseEvent.X, mouseEvent.Y, 1, 1);
        }

        /// <summary>
        /// Takes a screenshot of current screen.
        /// Taken from http://www.switchonthecode.com/tutorials/taking-some-screenshots-with-csharp
        /// </summary>
        /// <param name="e">Mouse event that triggered this screenshot.</param>
        /// <returns>Screenshot with mouse click marked on it.</returns>
        private Bitmap GrabScreenshot(MouseEventArgs e)
        {
            var screenShotBMP = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var screenShotGraphics = Graphics.FromImage(screenShotBMP);

            screenShotGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            DrawMouseClickIcon(ref screenShotGraphics, e);

            screenShotGraphics.Dispose();

            return screenShotBMP;
        }

        /// <summary>
        /// Support function, formats DateTime to Windows file system friendly text.
        /// </summary>
        /// <param name="dt">DateTime to transform.</param>
        /// <returns>Windows-friendly text.</returns>
        private string DateToFileString(DateTime dt)
        {
            var date = dt.ToShortDateString();

            var time = string.Empty;

            if (dt.Hour < 10)
            {
                time += "0";
            }

            time += dt.Hour + ".";

            if (dt.Minute < 10)
            {
                time += "0";
            }

            time += dt.Minute + ".";

            if (dt.Second < 10)
            {
                time += "0";
            }

            time += dt.Second;
            
            return date + "_" + time;
        }
    }
}
