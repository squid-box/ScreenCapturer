namespace ScreenCapturer.Code
{
    using System;
    using System.Collections.Generic;
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
        internal readonly Queue<Screenshot> Shots;

        /// <summary>
        /// Number of screenshots to save.
        /// </summary>
        internal int Buffersize = Properties.Settings.Default.BufferSize;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Capturer"/> class.
        /// </summary>
        internal Capturer()
        {
            Shots = new Queue<Screenshot>(Buffersize);
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
            if (IsTakingShots)
            {
                if (Shots.Count >= Buffersize)
                {
                    Shots.Dequeue().Dispose();
                }

                Shots.Enqueue(Screenshot.Capture(mouseDown, mouseUp));
            }
        }
        
        /// <summary>
        /// Saves screenshots currently in buffer to a .zip-file named with todays date.
        /// </summary>
        /// <returns>Path to saved file.</returns>
        internal string SaveShots()
        {
            // Load settings
            var path = Properties.Settings.Default.SaveFolder;

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
