namespace ScreenCapturer.Code
{
    using System.Drawing;
    using System.Windows.Forms;
    
    /// <summary>
    /// Handles drawing mouse activity on screenshots.
    /// </summary>
    public class MousePlotter
    {
        /// <summary>
        /// Draws an icon indicating where mouse click was performed.
        /// </summary>
        /// <param name="target">Graphics object to draw icon on.</param>
        /// <param name="mouseDown">Event where mouse click was initiated.</param>
        /// <param name="mouseUp">Event where mouse click was released.</param>
        public static void DrawMouseClickIcon(ref Bitmap target, MouseEventArgs mouseDown, MouseEventArgs mouseUp)
        {
            var graph = Graphics.FromImage(target);
            const int diameter = 16;
            var pen = new Pen(Color.Red, 1.0f);

            // Always draw a circle at the mouseUp event.
            DrawCircleDot(ref graph, mouseUp.Location, pen, diameter);

            if (Utility.ArePointsFarFromEachother(mouseDown.Location, mouseUp.Location))
            {
                // Points are distanced from each other, draw both points and a line between them.
                DrawCircleDot(ref graph, mouseDown.Location, pen, diameter);
                graph.DrawLine(pen, mouseDown.X, mouseDown.Y, mouseUp.X, mouseUp.Y);
            }

            graph.Dispose();
            pen.Dispose();
        }

        public static void DrawMouseDoubleClickIcon(ref Bitmap target, MouseEventArgs mouseDown, MouseEventArgs mouseUp)
        {
            var graph = Graphics.FromImage(target);
            const int diameter = 16;
            var pen = new Pen(Color.Red, 1.0f);

            // Always draw a circle at the mouseUp event.
            DrawCircleDot(ref graph, mouseUp.Location, pen, diameter);
            DrawCircleDot(ref graph, mouseUp.Location, pen, diameter*2);

            graph.Dispose();
            pen.Dispose();
        }

        /// <summary>
        /// Draws a graphical indicator on the specified point.
        /// </summary>
        /// <param name="target">Graphics target to draw indicator on.</param>
        /// <param name="origo">Center point of indicator.</param>
        /// <param name="pen">Pen to use for drawing indicator.</param>
        /// <param name="diameter">Diameter of indicator (circle).</param>
        private static void DrawCircleDot(ref Graphics target, Point origo, Pen pen, int diameter)
        {
            // Draw a circle centered on the point.
            target.DrawEllipse(pen, origo.X - (diameter / 2), origo.Y - (diameter / 2), diameter, diameter);

            // Draw a single pixel indicating the point itself.
            target.FillRectangle(new SolidBrush(Color.Red), origo.X, origo.Y, 1, 1);
        }
    }
}