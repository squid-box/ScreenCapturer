namespace ScreenCapturer.Code
{
    using System.Windows.Forms;

    /// <summary>
    /// Holds information needed to plot information on a screenshot.
    /// </summary>
    public class ScreenshotArgs
    {
        /// <summary>
        /// Event for the press of the mouse button.
        /// </summary>
        public MouseEventArgs MouseDown { get; private set; }

        /// <summary>
        /// Event for the release of the mouse button.
        /// </summary>
        public MouseEventArgs MouseUp { get; private set; }

        /// <summary>
        /// Was click a double click?
        /// </summary>
        public bool DoubleClick { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mouseDown">Event data of the mouse event that triggered this shot.</param>
        /// <param name="mouseUp">Event data of the end of this shot.</param>
        /// <param name="doubleClick">Is this a screenshot of a double click?</param>
        public ScreenshotArgs(MouseEventArgs mouseDown, MouseEventArgs mouseUp, bool doubleClick)
        {
            MouseDown = mouseDown;
            MouseUp = mouseUp;
            DoubleClick = doubleClick;
        }
    }
}
