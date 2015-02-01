namespace ScreenCapturer
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxSavepath = new System.Windows.Forms.TextBox();
            this.numericUpDownBufferSize = new System.Windows.Forms.NumericUpDown();
            this.checkedListBoxMouseKeyTriggers = new System.Windows.Forms.CheckedListBox();
            this.comboBoxToggleShotKey = new System.Windows.Forms.ComboBox();
            this.labelSavepath = new System.Windows.Forms.Label();
            this.labelMouseKeyTriggers = new System.Windows.Forms.Label();
            this.labelToggleKey = new System.Windows.Forms.Label();
            this.labelBufferSize = new System.Windows.Forms.Label();
            this.labelSaveKey = new System.Windows.Forms.Label();
            this.comboBoxSaveKey = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBufferSize)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOkClick);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // textBoxSavepath
            // 
            resources.ApplyResources(this.textBoxSavepath, "textBoxSavepath");
            this.textBoxSavepath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxSavepath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxSavepath.Name = "textBoxSavepath";
            // 
            // numericUpDownBufferSize
            // 
            resources.ApplyResources(this.numericUpDownBufferSize, "numericUpDownBufferSize");
            this.numericUpDownBufferSize.Name = "numericUpDownBufferSize";
            // 
            // checkedListBoxMouseKeyTriggers
            // 
            resources.ApplyResources(this.checkedListBoxMouseKeyTriggers, "checkedListBoxMouseKeyTriggers");
            this.checkedListBoxMouseKeyTriggers.FormattingEnabled = true;
            this.checkedListBoxMouseKeyTriggers.Name = "checkedListBoxMouseKeyTriggers";
            // 
            // comboBoxToggleShotKey
            // 
            resources.ApplyResources(this.comboBoxToggleShotKey, "comboBoxToggleShotKey");
            this.comboBoxToggleShotKey.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxToggleShotKey.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxToggleShotKey.FormattingEnabled = true;
            this.comboBoxToggleShotKey.Name = "comboBoxToggleShotKey";
            // 
            // labelSavepath
            // 
            resources.ApplyResources(this.labelSavepath, "labelSavepath");
            this.labelSavepath.Name = "labelSavepath";
            // 
            // labelMouseKeyTriggers
            // 
            resources.ApplyResources(this.labelMouseKeyTriggers, "labelMouseKeyTriggers");
            this.labelMouseKeyTriggers.Name = "labelMouseKeyTriggers";
            // 
            // labelToggleKey
            // 
            resources.ApplyResources(this.labelToggleKey, "labelToggleKey");
            this.labelToggleKey.Name = "labelToggleKey";
            // 
            // labelBufferSize
            // 
            resources.ApplyResources(this.labelBufferSize, "labelBufferSize");
            this.labelBufferSize.Name = "labelBufferSize";
            // 
            // labelSaveKey
            // 
            resources.ApplyResources(this.labelSaveKey, "labelSaveKey");
            this.labelSaveKey.Name = "labelSaveKey";
            // 
            // comboBoxSaveKey
            // 
            resources.ApplyResources(this.comboBoxSaveKey, "comboBoxSaveKey");
            this.comboBoxSaveKey.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSaveKey.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSaveKey.FormattingEnabled = true;
            this.comboBoxSaveKey.Name = "comboBoxSaveKey";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ControlBox = false;
            this.Controls.Add(this.labelSaveKey);
            this.Controls.Add(this.comboBoxSaveKey);
            this.Controls.Add(this.labelBufferSize);
            this.Controls.Add(this.labelToggleKey);
            this.Controls.Add(this.labelMouseKeyTriggers);
            this.Controls.Add(this.labelSavepath);
            this.Controls.Add(this.comboBoxToggleShotKey);
            this.Controls.Add(this.checkedListBoxMouseKeyTriggers);
            this.Controls.Add(this.numericUpDownBufferSize);
            this.Controls.Add(this.textBoxSavepath);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "SettingsForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBufferSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxSavepath;
        private System.Windows.Forms.NumericUpDown numericUpDownBufferSize;
        private System.Windows.Forms.CheckedListBox checkedListBoxMouseKeyTriggers;
        private System.Windows.Forms.ComboBox comboBoxToggleShotKey;
        private System.Windows.Forms.Label labelSavepath;
        private System.Windows.Forms.Label labelMouseKeyTriggers;
        private System.Windows.Forms.Label labelToggleKey;
        private System.Windows.Forms.Label labelBufferSize;
        private System.Windows.Forms.Label labelSaveKey;
        private System.Windows.Forms.ComboBox comboBoxSaveKey;
    }
}