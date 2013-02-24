namespace ScreenCapturer.Code
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    
    using Ionic.Zip;

    using Properties;

    /// <summary>
    /// The Capturer performs anything related to screencapturing and handling these captures.
    /// </summary>
    public class Capturer
    {
        #region Fields & Properties

        /// <summary>
        /// Holds screenshots and their timestamp.
        /// </summary>
        internal readonly List<Screenshot> Shots;

        public int Buffersize 
        {
            get { return Settings.Default.BufferSize; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Capturer"/> class.
        /// </summary>
        internal Capturer()
        {
            Shots = new List<Screenshot>();
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
        /// <param name="doubleClick">Is this a screenshot of a double click?</param>
        internal void TakeShot(MouseEventArgs mouseDown, MouseEventArgs mouseUp, Boolean doubleClick)
        {
            if (IsTakingShots)
            {
                if (doubleClick && Shots.Count > 0)
                {
                    // If a doubleclick is detected, last shot is removed to "make room" for the double click shot.
                    Shots.RemoveAt(0);
                }

                CheckBuffer();

                Shots.Add(Screenshot.Capture(mouseDown, mouseUp, doubleClick));
            }
        }

        /// <summary>
        /// Checks if the buffer is full, and removes the oldest shot if it is.
        /// </summary>
        private void CheckBuffer()
        {
            if (Shots.Count >= Settings.Default.BufferSize)
            {
                Shots.RemoveAt(Shots.Count-1);
            }
        }

        /// <summary>
        /// Saves screenshots currently in buffer to a .zip-file named with todays date.
        /// </summary>
        /// <returns>Path to saved file.</returns>
        internal string SaveShots()
        {
            // Load settings
            var path = Settings.Default.SaveFolder;

            // Pause screenshotting
            IsTakingShots = false;

            if (Shots.Count == 0)
            {
                return null;
            }

            var filenames = new List<string>();

            var i = 1;
            foreach (var shot in Shots)
            {
                var filename = shot.SaveToFile(i);
                filenames.Add(filename);
                i++;

                shot.Dispose();
            }

            Shots.Clear();

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
            IsTakingShots = true;

            // Clear buffer
            Shots.Clear();

            return savePath;
        }
    }
}
