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

        /// <summary>
        /// 
        /// </summary>
        public Bitmap Image
        {
            get { return _image; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TimeCaptured { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ScreenshotArgs Arguments { get; private set; }

        private Bitmap _image;

        #endregion

        #region Constructors

        private Screenshot(ScreenshotArgs args)
        {
            _image = CaptureScreen();
            TimeCaptured = DateTime.Now;
            Arguments = args;
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
            var other = obj as Screenshot;
            return TimeCaptured.CompareTo(other.TimeCaptured);
        }

        #endregion

        /// <summary>
        /// Takes a screenshot of current screen.
        /// </summary>
        /// <returns>Screenshot.</returns>
        private Bitmap CaptureScreen()
        {
            var screenShotBmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var screenShotGraphics = Graphics.FromImage(screenShotBmp);

            screenShotGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            screenShotGraphics.Dispose();
            return screenShotBmp;
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
            Image.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        private void PlotArgsOnImage(ref Bitmap image, ScreenshotArgs args)
        {
            if (Arguments.DoubleClick)
            {
                MousePlotter.DrawMouseDoubleClickIcon(ref image, args.MouseUp);
            }
            else
            {
                MousePlotter.DrawMouseClickIcon(ref image, args.MouseDown, args.MouseUp);
            }
        }

        /// <summary>
        /// Saves the screenshot to a file.
        /// </summary>
        /// <param name="prefixNumber">Number prefixing this file.</param>
        /// <returns>Path to saved file.</returns>
        public string SaveToFile(int prefixNumber)
        {
            var filename = prefixNumber + "_" + Utility.DateToFileString(TimeCaptured) + ".png";

            if (Properties.Settings.Default.DrawMousePlot)
            {
                PlotArgsOnImage(ref _image, Arguments);
            }
                
            Image.Save(filename, ImageFormat.Png);
            
            return filename;
        }

        /// <summary>
        /// Take a screenshot.
        /// </summary>
        /// <returns>Screenshot of current desktop.</returns>
        public static Screenshot Capture(ScreenshotArgs args)
        {
            return new Screenshot(args);
        }
    }
}