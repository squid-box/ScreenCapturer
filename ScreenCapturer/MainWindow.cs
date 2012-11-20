namespace ScreenCapturer
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using MouseKeyboardActivityMonitor;
    using MouseKeyboardActivityMonitor.WinApi;

    /// <summary>
    /// The main window of this program.
    /// </summary>
    public partial class MainWindow : Form
    {
        #region Fields

        /// <summary>
        /// Listens for global mouse events.
        /// </summary>
        private readonly MouseHookListener listenerMouse;

        /// <summary>
        /// Listens for global keyboard events.
        /// </summary>
        private readonly KeyboardHookListener listenerKeyboard;

        /// <summary>
        /// Performs all screencapturing actions.
        /// </summary>
        private readonly Capturer capturer;

        /// <summary>
        /// Latest mouseDown event arguments, used for drawing drag 'n drop lines.
        /// </summary>
        private MouseEventArgs lastMouseDownEvent;

        /// <summary>
        /// Indicates whether or not the next shot should be ignored.
        /// </summary>
        private bool ignoreNextShot;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class. 
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            this.capturer = new Capturer();
            
            this.listenerKeyboard = new KeyboardHookListener(new GlobalHooker());
            this.listenerMouse = new MouseHookListener(new GlobalHooker());

            this.listenerKeyboard.Enabled = true;
            this.listenerMouse.Enabled = true;

            // Start listening to Mouse- and KeyDown.
            this.listenerKeyboard.KeyDown += this.ListenerKeyboardKeyDown;
            this.listenerMouse.MouseDown += this.ListenerMouseMouseDown;
            this.listenerMouse.MouseUp += this.ListenerMouseMouseUp;
        }

        /// <summary>
        /// Function triggered when program is closing.
        /// </summary>
        /// <param name="sender">Sender of the close event.</param>
        /// <param name="e">Event arguments.</param>
        private void MainWindowFormClosing(object sender, FormClosingEventArgs e)
        {
            // Hopefully removes all hooks.
            try
            {
                this.listenerKeyboard.Dispose();
                this.listenerMouse.Dispose();
            }
            catch (Win32Exception)
            {
                // Listeners have already been disposed, probably.
                // See http://globalmousekeyhook.codeplex.com/workitem/929
            }

            if (this.capturer.Shots.Count != 0)
            {
                // There are still screenshots in the buffer! D:
                DialogResult res = MessageBox.Show(this, "There are unsaved screenshots, do you want to save them?", "Unsaved data!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (res == DialogResult.Yes)
                {
                    this.SaveShots();
                }
            }

            this.notificationIcon.Visible = false;
        }

        #region Event handling

        /// <summary>
        /// Captures mousepresses handled here.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event information.</param>
        private void ListenerMouseMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.ignoreNextShot)
                {
                    return;
                }

                this.lastMouseDownEvent = e;
            }
        }

        private void ListenerMouseMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.ignoreNextShot)
                {
                    this.ignoreNextShot = false;
                    return;
                }

                this.capturer.TakeShot(e, lastMouseDownEvent);
            }
        }

        /// <summary>
        /// Captures keypresses are handled here.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event information.</param>
        private void ListenerKeyboardKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    this.ToggleIsTakingScreenShots();
                    break;
                case Keys.F11:
                    this.SaveShots();
                    break;
            }
        }

        #region UI events

        /// <summary>
        /// Actions performed when context menu button "Exit" is pressed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void NotificationMenuExitClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Actions performed when context menu button "Toggle capturing" is pressed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void NotificationMenuToggleClick(object sender, EventArgs e)
        {
            this.ToggleIsTakingScreenShots();
        }

        /// <summary>
        /// Actions performed when context menu button "Save shots to file" is pressed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void NotificationMenuSaveClick(object sender, EventArgs e)
        {
            this.SaveShots();
        }

        /// <summary>
        /// Actions to perform when main window loads.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void MainWindowLoad(object sender, EventArgs e)
        {
            this.Size = new Size(0, 0);
        }

        /// <summary>
        /// Actions to perform when notify icon is right-clicked.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void NotificationIconMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.ignoreNextShot = true;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Enables or disables screenshot-capturing, complete with UI actions.
        /// </summary>
        /// <param name="enable">
        /// True to enable, false to disable.
        /// </param>
        private void EnableIsTakingScreenShots(bool enable)
        {
            if (enable)
            {
                this.listenerMouse.Enabled = true;
                this.capturer.IsTakingShots = true;
                this.notificationIcon.ShowBalloonTip(250, "ScreenCapturer", "SC has started taking screenshots...", ToolTipIcon.Info);
                this.notificationIcon.Text = "ScreenCapturer is taking screenshots.";
            }
            else
            {
                this.listenerMouse.Enabled = false;
                this.capturer.IsTakingShots = false;
                this.notificationIcon.ShowBalloonTip(250, "ScreenCapturer", "SC has stopped taking screenshots...", ToolTipIcon.Info);
                this.notificationIcon.Text = "ScreenCapturer is not taking screenshots.";
            }
        }

        /// <summary>
        /// Toggles whether or not SC is taking screenshots.
        /// Shows balloon tooltip and changes notification icon hover text.
        /// </summary>
        private void ToggleIsTakingScreenShots()
        {
            if (this.capturer.IsTakingShots)
            {
                this.EnableIsTakingScreenShots(false);
            }
            else
            {
                this.EnableIsTakingScreenShots(true);
            }
        }

        /// <summary>
        /// Triggers a shot saving, complete with UI information.
        /// </summary>
        private void SaveShots()
        {
            this.capturer.IsTakingShots = false;
            this.notificationIcon.ShowBalloonTip(250, "ScreenCapturer", string.Format("Saving {0} shots to file...", this.capturer.Shots.Count), ToolTipIcon.Info);
            
            var path = this.capturer.SaveShots("logs");

            if (path != null)
            {
                this.notificationIcon.ShowBalloonTip(250, "ScreenCapturer", string.Format("Saved shots to \"{0}\"", path), ToolTipIcon.Info);
                this.capturer.IsTakingShots = false;
            }
            else
            {
                this.notificationIcon.ShowBalloonTip(250, "ScreenCapturer", "No shots to save...", ToolTipIcon.Warning);
            }
        }
    }
}
