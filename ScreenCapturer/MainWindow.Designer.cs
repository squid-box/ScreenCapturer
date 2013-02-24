namespace ScreenCapturer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.notificationIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notificationContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notificationMenuToggle = new System.Windows.Forms.ToolStripMenuItem();
            this.notificationMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openSavedFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.notificationMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.notificationContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notificationIcon
            // 
            this.notificationIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.notificationIcon, "notificationIcon");
            this.notificationIcon.ContextMenuStrip = this.notificationContextMenu;
            this.notificationIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotificationIconMouseClick);
            // 
            // notificationContextMenu
            // 
            resources.ApplyResources(this.notificationContextMenu, "notificationContextMenu");
            this.notificationContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notificationMenuToggle,
            this.notificationMenuSave,
            this.toolStripSeparator2,
            this.openSavedFolderToolStripMenuItem,
            this.toolStripSeparator3,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.notificationMenuExit});
            this.notificationContextMenu.Name = "notificationContextMenu";
            // 
            // notificationMenuToggle
            // 
            resources.ApplyResources(this.notificationMenuToggle, "notificationMenuToggle");
            this.notificationMenuToggle.Name = "notificationMenuToggle";
            this.notificationMenuToggle.Click += new System.EventHandler(this.NotificationMenuToggleClick);
            // 
            // notificationMenuSave
            // 
            resources.ApplyResources(this.notificationMenuSave, "notificationMenuSave");
            this.notificationMenuSave.Name = "notificationMenuSave";
            this.notificationMenuSave.Click += new System.EventHandler(this.NotificationMenuSaveClick);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // openSavedFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.openSavedFolderToolStripMenuItem, "openSavedFolderToolStripMenuItem");
            this.openSavedFolderToolStripMenuItem.Name = "openSavedFolderToolStripMenuItem";
            this.openSavedFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenSavedFolderToolStripMenuItemClick);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // settingsToolStripMenuItem
            // 
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // notificationMenuExit
            // 
            resources.ApplyResources(this.notificationMenuExit, "notificationMenuExit");
            this.notificationMenuExit.Name = "notificationMenuExit";
            this.notificationMenuExit.Click += new System.EventHandler(this.NotificationMenuExitClick);
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindowFormClosing);
            this.Load += new System.EventHandler(this.MainWindowLoad);
            this.notificationContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.NotifyIcon notificationIcon;
        private System.Windows.Forms.ContextMenuStrip notificationContextMenu;
        private System.Windows.Forms.ToolStripMenuItem notificationMenuSave;
        private System.Windows.Forms.ToolStripMenuItem notificationMenuExit;
        private System.Windows.Forms.ToolStripMenuItem notificationMenuToggle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

        #endregion
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSavedFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

