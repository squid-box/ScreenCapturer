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
        internal const int Buffersize = 32;

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
        /// <param name="mouseDown">Event data of the mouse event that triggered this shot.</param>
        /// <param name="mouseUp">Event data of the end of this shot.</param>
        internal void TakeShot(MouseEventArgs mouseDown, MouseEventArgs mouseUp)
        {
            if (this.IsTakingShots)
            {
                if (this.Shots.Count >= Buffersize)
                {
                    this.Shots.Dequeue().Value.Dispose();
                }

                this.Shots.Enqueue(new KeyValuePair<DateTime, Bitmap>(DateTime.Now, this.GrabScreenshot(mouseDown, mouseUp)));
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
                string filename = i + "_" + Utility.DateToFileString(shot.Key) + ".png";
                shot.Value.Save(filename, ImageFormat.Png);
                shot.Value.Dispose();
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
        /// <param name="mouseDown">Event where mouse click was initiated.</param>
        /// <param name="mouseUp">Event where mouse click was released.</param>
        private void DrawMouseClickIcon(ref Graphics target, MouseEventArgs mouseDown, MouseEventArgs mouseUp)
        {
            const int diameter = 16;
            var pen = new Pen(Color.Red, 1.0f);

            // Always draw a circle at the mouseUp event.
            DrawCircleDot(ref target, mouseUp.Location, pen, diameter);

            if (Utility.ArePointsFarFromEachother(mouseDown.Location, mouseUp.Location))
            {
                // Points are distanced from each other, draw both points and a line between them.
                DrawCircleDot(ref target, mouseDown.Location, pen, diameter);
                target.DrawLine(pen, mouseDown.X, mouseDown.Y, mouseUp.X, mouseUp.Y);
            }
        }

        /// <summary>
        /// Draws a graphical indicator on the specified point.
        /// </summary>
        /// <param name="target">Graphics target to draw indicator on.</param>
        /// <param name="origo">Center point of indicator.</param>
        /// <param name="pen">Pen to use for drawing indicator.</param>
        /// <param name="diameter">Diameter of indicator (circle).</param>
        private void DrawCircleDot(ref Graphics target, Point origo, Pen pen, int diameter)
        {
            // Draw a circle centered on the point.
            target.DrawEllipse(pen, origo.X - (diameter / 2), origo.Y - (diameter / 2), diameter, diameter);
            
            // Draw a single pixel indicating the point itself.
            target.FillRectangle(new SolidBrush(Color.Red), origo.X, origo.Y, 1, 1);
        }

        /// <summary>
        /// Takes a screenshot of current screen.
        /// Taken from http://www.switchonthecode.com/tutorials/taking-some-screenshots-with-csharp
        /// </summary>
        /// <param name="mouseDown">Mouse event that triggered this screenshot.</param>
        /// <param name="mouseUp">Mouse event that ended this screenshot.</param>
        /// <returns>Screenshot with mouse click marked on it.</returns>
        private Bitmap GrabScreenshot(MouseEventArgs mouseDown, MouseEventArgs mouseUp)
        {
            var screenShotBMP = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var screenShotGraphics = Graphics.FromImage(screenShotBMP);

            screenShotGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            this.DrawMouseClickIcon(ref screenShotGraphics, mouseDown, mouseUp);

            screenShotGraphics.Dispose();

            return screenShotBMP;
        }
    }
}
