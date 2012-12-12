namespace ScreenCapturer.Code
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Contains functions that could be useful in several places.
    /// </summary>
    internal class Utility
    {
        /// <summary>
        /// Check if two given points are distant to eachother or not.
        /// </summary>
        /// <param name="first">First point to check against.</param>
        /// <param name="second">Second point to check against.</param>
        /// <returns>True if points are far from each other, false if they are close.</returns>
        internal static bool ArePointsFarFromEachother(Point first, Point second)
        {
            const double threshold = 16;

            var distance = Math.Sqrt(Math.Pow((second.X - first.X),2) + Math.Pow((second.Y - first.Y),2));

            return distance > threshold;
        }

        /// <summary>
        /// Support function, formats DateTime to Windows file system friendly text.
        /// </summary>
        /// <param name="dt">DateTime to transform.</param>
        /// <returns>Windows-friendly text.</returns>
        internal static string DateToFileString(DateTime dt)
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
