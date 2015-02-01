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
            // Threshold counted in pixels.
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
            return string.Format("{0,4:D4}-{1,2:D2}-{2,2:D2}_{3,2:D2}.{4,2:D2}.{5,2:D2}", 
                dt.Year,
                dt.Month,
                dt.Day,
                dt.Hour,
                dt.Minute,
                dt.Second);
        }
    }
}
