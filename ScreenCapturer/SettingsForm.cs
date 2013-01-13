using System.Globalization;

namespace ScreenCapturer
{
    using System;
    using System.Windows.Forms;
    using Properties;

    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadUiValues();

            ReadSettings();
        }

        /// <summary>
        /// Loads values into UI objects at start.
        /// </summary>
        private void LoadUiValues()
        {
            checkedListBoxMouseKeyTriggers.DataSource = Enum.GetNames(typeof(MouseButtons));
            comboBoxToggleShotKey.DataSource = Enum.GetNames(typeof(Keys));
            comboBoxSaveKey.DataSource = Enum.GetNames(typeof(Keys));
        }

        /// <summary>
        /// Event handler for user interaction: Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancelClick(object sender, EventArgs e)
        {
            // Discard (ignore) any changed settings.
            Close();
        }

        /// <summary>
        /// Event handler for user interaction: OK button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOkClick(object sender, EventArgs e)
        {
            SaveSettings();

            Close();
        }

        /// <summary>
        /// Read settings from file and update UI elements.
        /// </summary>
        private void ReadSettings()
        {
            // Savepath
            textBoxSavepath.Text = Settings.Default.SaveFolder;
            
            // BufferSize
            numericUpDownBufferSize.Text = Settings.Default.BufferSize.ToString(CultureInfo.InvariantCulture);
            
            // MouseKeyTriggers
            foreach(var key in Settings.Default.MouseKeyTriggers)
            {
                // Find index of key
                var index = checkedListBoxMouseKeyTriggers.Items.IndexOf(key);

                if (index == -1)
                {
                    // Item wasn't found.
                    // TODO: Log this as an error in the settings file?
                    continue;
                }

                // Set correct status
                checkedListBoxMouseKeyTriggers.SetItemCheckState(index, CheckState.Checked);
            }

            // ToggleCaptureKey
            comboBoxToggleShotKey.Text = Settings.Default.ToggleCaptureKey;

            // SaveKey
            comboBoxSaveKey.Text = Settings.Default.SaveKey;
        }

        /// <summary>
        /// Save settings from Form into the settings file.
        /// </summary>
        private void SaveSettings()
        {
            // Savepath
            Settings.Default.SaveFolder = textBoxSavepath.Text;

            // BufferSize
            Settings.Default.BufferSize = Int32.Parse(numericUpDownBufferSize.Text);

            // MouseKeyTriggers
            Settings.Default.MouseKeyTriggers.Clear();
            
            foreach(var selected in checkedListBoxMouseKeyTriggers.CheckedItems)
            {
                Settings.Default.MouseKeyTriggers.Add(selected.ToString());
            }

            // ToggleCaptureKey
            Settings.Default.ToggleCaptureKey = comboBoxToggleShotKey.Text;

            // SaveKey
            Settings.Default.SaveKey = comboBoxSaveKey.Text;

            // Done!
            Settings.Default.Save();
        }
    }
}
