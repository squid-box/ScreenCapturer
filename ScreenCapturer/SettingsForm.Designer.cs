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
            this.buttonOK.Location = new System.Drawing.Point(149, 231);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOkClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(230, 231);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // textBoxSavepath
            // 
            this.textBoxSavepath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxSavepath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxSavepath.Location = new System.Drawing.Point(12, 25);
            this.textBoxSavepath.Name = "textBoxSavepath";
            this.textBoxSavepath.Size = new System.Drawing.Size(291, 20);
            this.textBoxSavepath.TabIndex = 2;
            // 
            // numericUpDownBufferSize
            // 
            this.numericUpDownBufferSize.Location = new System.Drawing.Point(12, 234);
            this.numericUpDownBufferSize.Name = "numericUpDownBufferSize";
            this.numericUpDownBufferSize.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownBufferSize.TabIndex = 3;
            // 
            // checkedListBoxMouseKeyTriggers
            // 
            this.checkedListBoxMouseKeyTriggers.FormattingEnabled = true;
            this.checkedListBoxMouseKeyTriggers.Location = new System.Drawing.Point(12, 77);
            this.checkedListBoxMouseKeyTriggers.Name = "checkedListBoxMouseKeyTriggers";
            this.checkedListBoxMouseKeyTriggers.Size = new System.Drawing.Size(291, 79);
            this.checkedListBoxMouseKeyTriggers.TabIndex = 4;
            // 
            // comboBoxToggleShotKey
            // 
            this.comboBoxToggleShotKey.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxToggleShotKey.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxToggleShotKey.FormattingEnabled = true;
            this.comboBoxToggleShotKey.Location = new System.Drawing.Point(12, 185);
            this.comboBoxToggleShotKey.MaxDropDownItems = 4;
            this.comboBoxToggleShotKey.Name = "comboBoxToggleShotKey";
            this.comboBoxToggleShotKey.Size = new System.Drawing.Size(121, 21);
            this.comboBoxToggleShotKey.TabIndex = 5;
            // 
            // labelSavepath
            // 
            this.labelSavepath.AutoSize = true;
            this.labelSavepath.Location = new System.Drawing.Point(12, 9);
            this.labelSavepath.Name = "labelSavepath";
            this.labelSavepath.Size = new System.Drawing.Size(59, 13);
            this.labelSavepath.TabIndex = 6;
            this.labelSavepath.Text = "Save path:";
            // 
            // labelMouseKeyTriggers
            // 
            this.labelMouseKeyTriggers.AutoSize = true;
            this.labelMouseKeyTriggers.Location = new System.Drawing.Point(12, 61);
            this.labelMouseKeyTriggers.Name = "labelMouseKeyTriggers";
            this.labelMouseKeyTriggers.Size = new System.Drawing.Size(180, 13);
            this.labelMouseKeyTriggers.TabIndex = 7;
            this.labelMouseKeyTriggers.Text = "Mouse keys that trigger screenshots:";
            // 
            // labelToggleKey
            // 
            this.labelToggleKey.AutoSize = true;
            this.labelToggleKey.Location = new System.Drawing.Point(12, 169);
            this.labelToggleKey.Name = "labelToggleKey";
            this.labelToggleKey.Size = new System.Drawing.Size(111, 13);
            this.labelToggleKey.TabIndex = 8;
            this.labelToggleKey.Text = "Key to toggle capture:";
            // 
            // labelBufferSize
            // 
            this.labelBufferSize.AutoSize = true;
            this.labelBufferSize.Location = new System.Drawing.Point(12, 218);
            this.labelBufferSize.Name = "labelBufferSize";
            this.labelBufferSize.Size = new System.Drawing.Size(126, 13);
            this.labelBufferSize.TabIndex = 9;
            this.labelBufferSize.Text = "Number of shots to keep:";
            // 
            // labelSaveKey
            // 
            this.labelSaveKey.AutoSize = true;
            this.labelSaveKey.Location = new System.Drawing.Point(179, 169);
            this.labelSaveKey.Name = "labelSaveKey";
            this.labelSaveKey.Size = new System.Drawing.Size(107, 13);
            this.labelSaveKey.TabIndex = 11;
            this.labelSaveKey.Text = "Key to toggle a save:";
            // 
            // comboBoxSaveKey
            // 
            this.comboBoxSaveKey.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSaveKey.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSaveKey.FormattingEnabled = true;
            this.comboBoxSaveKey.Location = new System.Drawing.Point(179, 185);
            this.comboBoxSaveKey.MaxDropDownItems = 4;
            this.comboBoxSaveKey.Name = "comboBoxSaveKey";
            this.comboBoxSaveKey.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSaveKey.TabIndex = 10;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(317, 263);
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
            this.Text = "Screen Capturer Settings";
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