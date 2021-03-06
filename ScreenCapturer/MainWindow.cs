﻿namespace ScreenCapturer
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using MouseKeyboardActivityMonitor;
    using MouseKeyboardActivityMonitor.WinApi;

    using Code;
    using Localizations;
    using Properties;

    /// <summary>
    /// The main window of this program.
    /// </summary>
    public partial class MainWindow : Form
    {
        #region Fields

        /// <summary>
        /// Listens for global mouse events.
        /// </summary>
        private readonly MouseHookListener _listenerMouse;

        /// <summary>
        /// Listens for global keyboard events.
        /// </summary>
        private readonly KeyboardHookListener _listenerKeyboard;

        /// <summary>
        /// Performs all screencapturing actions.
        /// </summary>
        private readonly Capturer _capturer;

        /// <summary>
        /// Latest mouseDown event arguments, used for drawing drag 'n drop lines.
        /// </summary>
        private MouseEventArgs _lastMouseDownEvent;

        /// <summary>
        /// Indicates whether or not the next shot should be ignored.
        /// </summary>
        private bool _ignoreNextShot;

        /// <summary>
        /// System time when last shot was taken, used to detect double clicks.
        /// </summary>
        private long _lastShotTaken;
        
        /// <summary>
        /// Defines the maximum amount of milliseconds there can be between two clicks to define the two clicks as a doubleclick.
        /// </summary>
        private const long DoubleclickThreshold = 500;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class. 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Visible = false;

            _capturer = new Capturer();
            
            // TODO: Make this functionality better & work well for "folder path changed during runtime"...
            System.IO.Directory.CreateDirectory(Settings.Default.SaveFolder);

            _lastShotTaken = 0;
            
            _listenerKeyboard = new KeyboardHookListener(new GlobalHooker());
            _listenerMouse = new MouseHookListener(new GlobalHooker());

            _listenerKeyboard.Enabled = true;
            _listenerMouse.Enabled = true;

            // Start listening to Mouse- and KeyDown.
            _listenerKeyboard.KeyDown += ListenerKeyboardKeyDown;
            _listenerMouse.MouseDown += ListenerMouseMouseDown;
            _listenerMouse.MouseUp += ListenerMouseMouseUp;
        }

        /// <summary>
        /// Function triggered when program is closing.
        /// </summary>
        /// <param name="sender">Sender of the close event.</param>
        /// <param name="e">Event arguments.</param>
        private void MainWindowFormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.Instance.Debug("Program closing");

            // Hopefully removes all hooks.
            try
            {
                _listenerKeyboard.Dispose();
                _listenerMouse.Dispose();
            }
            catch (Win32Exception exc)
            {
                // Listeners have already been disposed, probably.
                // See http://globalmousekeyhook.codeplex.com/workitem/929
                Logger.Instance.Exception("Disposing keyhooks, exception ignored.", exc);
            }

            if (_capturer.Shots.Count != 0)
            {
                Logger.Instance.Debug("Shots left in buffer.");
                // There are still screenshots in the buffer! D:
                var res = MessageBox.Show(this, strings.dialog_closingProgram_UnsavedContent, strings.dialog_closingProgram_UnsavedTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (res == DialogResult.Yes)
                {
                    Logger.Instance.Debug("Saving screenshots before exiting.");
                    SaveShots();
                }
            }
        }

        #region Event handling

        /// <summary>
        /// Determines if the mouse button should trigger a screenshot.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool IsMouseButtonActive(MouseButtons button)
        {
            return Settings.Default.MouseKeyTriggers.Contains(button.ToString());
        }

        /// <summary>
        /// First part of the capturing process, saves this MouseEvent for <see cref="ListenerMouseMouseUp"/>.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event information.</param>
        private void ListenerMouseMouseDown(object sender, MouseEventArgs e)
        {
            if (IsMouseButtonActive(e.Button))
            {
                if (_ignoreNextShot)
                {
                    return;
                }

                _lastMouseDownEvent = e;
            }
        }

        /// <summary>
        /// Actually performs the capturing actions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListenerMouseMouseUp(object sender, MouseEventArgs e)
        {
            if (IsMouseButtonActive(e.Button))
            {
                if (_ignoreNextShot)
                {
                    _ignoreNextShot = false;
                    return;
                }

                _capturer.TakeShot(new ScreenshotArgs(e, _lastMouseDownEvent, DoubleClickDetected()));
                _lastShotTaken = Environment.TickCount;
            }
        }

        /// <summary>
        /// Checks if a click is close enough to the previous one to define a double click.
        /// </summary>
        /// <returns>True if double click, false if not.</returns>
        private bool DoubleClickDetected()
        {
            var now = Environment.TickCount;

            return (now - _lastShotTaken) < DoubleclickThreshold;
        }

        /// <summary>
        /// Captured keypresses are handled here.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event information.</param>
        private void ListenerKeyboardKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == Settings.Default.ToggleCaptureKey)
            {
                ToggleIsTakingScreenShots();
            }
            
            if (e.KeyCode.ToString() == Settings.Default.SaveKey)
            {
                SaveShots();
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
            Close();
        }

        /// <summary>
        /// Actions performed when context menu button "Toggle capturing" is pressed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void NotificationMenuToggleClick(object sender, EventArgs e)
        {
            ToggleIsTakingScreenShots();
        }

        /// <summary>
        /// Actions performed when context menu button "Save shots to file" is pressed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void NotificationMenuSaveClick(object sender, EventArgs e)
        {
            SaveShots();
        }

        /// <summary>
        /// Actions to perform when main window loads.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void MainWindowLoad(object sender, EventArgs e)
        {
            Size = new Size(0, 0);
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
                _ignoreNextShot = true;
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
                _listenerMouse.Enabled = true;
                _capturer.IsTakingShots = true;
                notificationIcon.ShowBalloonTip(250, strings.program_Name, strings.notificationIcon_StartedShooting, ToolTipIcon.Info);
                notificationIcon.Text = strings.notificationIcon_IsTakingShots;
            }
            else
            {
                _listenerMouse.Enabled = false;
                _capturer.IsTakingShots = false;
                notificationIcon.ShowBalloonTip(250, strings.program_Name, strings.notificationIcon_StoppedShooting, ToolTipIcon.Info);
                notificationIcon.Text = strings.notificationIcon_IsNotTakingShots;
            }
        }

        /// <summary>
        /// Toggles whether or not SC is taking screenshots.
        /// Shows balloon tooltip and changes notification icon hover text.
        /// </summary>
        private void ToggleIsTakingScreenShots()
        {
            EnableIsTakingScreenShots(!_capturer.IsTakingShots);
        }

        /// <summary>
        /// Triggers a shot saving, complete with UI information.
        /// </summary>
        private void SaveShots()
        {
            _capturer.IsTakingShots = false;
            notificationIcon.ShowBalloonTip(250, strings.program_Name, string.Format(strings.notificationIcon_SavingFiles, _capturer.Shots.Count), ToolTipIcon.Info);
            
            var path = _capturer.SaveShots();

            if (path != null)
            {
                notificationIcon.ShowBalloonTip(250, strings.program_Name, string.Format(strings.notificationIcon_SavedFiles, path), ToolTipIcon.Info);
                _capturer.IsTakingShots = false;
            }
            else
            {
                notificationIcon.ShowBalloonTip(250, strings.program_Name, strings.notificationIcon_NoFilesToSave, ToolTipIcon.Warning);
            }
        }

        private void SettingsToolStripMenuItemClick(object sender, EventArgs e)
        {
            var settings = new SettingsForm();

            settings.Show(this);
        }

        private void OpenSavedFolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Settings.Default.SaveFolder);
        }
    }
}
