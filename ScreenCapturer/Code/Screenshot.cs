namespace ScreenCapturer.Code
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    /// <summary>
    /// A screenshot with associated data.
    /// </summary>
    public class Screenshot : IComparable
    {
        #region Declarations

        private DateTime timeCaptured;
        private Bitmap image;

        private MouseEventArgs mouseDown;
        private MouseEventArgs mouseUp;

        #endregion

        #region Constructors

        private Screenshot(MouseEventArgs mouseDown, MouseEventArgs mouseUp)
        {
            this.image = this.CaptureScreen();
            this.timeCaptured = DateTime.Now;

            this.mouseDown = mouseDown;
            this.mouseUp = mouseUp;

            MousePlotter.DrawMouseClickIcon(ref image, mouseDown, mouseUp);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Simple override of the basic CompareTo function.
        /// </summary>
        /// <param name="obj">Object to compare this object to.</param>
        /// <returns>
        /// Less than zero: This object is less than <paramref name="obj"/>.
        /// Zero: This object is equal to <paramref name="obj"/>.
        /// Greater than zero: This object is greater than <paramref name="obj"/>.
        /// </returns>
        public int CompareTo(object obj)
        {
            var other = (Screenshot)obj;
            return this.timeCaptured.CompareTo(other.timeCaptured);
        }

        #endregion

        /// <summary>
        /// Takes a screenshot of current screen.
        /// </summary>
        /// <returns>Screenshot.</returns>
        private Bitmap CaptureScreen()
        {
            var screenShotBMP = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var screenShotGraphics = Graphics.FromImage(screenShotBMP);

            screenShotGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            screenShotGraphics.Dispose();
            return screenShotBMP;
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
            this.image.Dispose();
        }

        /// <summary>
        /// Saves the screenshot to a file.
        /// </summary>
        /// <param name="prefixNumber">Number prefixing this file.</param>
        /// <returns>Path to saved file.</returns>
        public string SaveToFile(int prefixNumber)
        {
            string filename = prefixNumber + "_" + Utility.DateToFileString(timeCaptured) + ".png";
            this.image.Save(filename, ImageFormat.Png);
            
            return filename;
        }

        /// <summary>
        /// Take a screenshot.
        /// </summary>
        /// <returns>Screenshot of current desktop.</returns>
        public static Screenshot Capture(MouseEventArgs mouseDown, MouseEventArgs mouseUp)
        {
            return new Screenshot(mouseDown, mouseUp);
        }
    }
}