using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SettingsHelper;

namespace SettingsHelperUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            // Initialize Settings
            ApplicationSettings.Init();

            // LoadText into field
            UpdateTextWithSettings();

            // Initialize SettingsHelper Library
            FileManager.InitFolderStructure();
        }

        private void UpdateTextWithSettings()
        {
            openFile.Text = ApplicationSettings.GetString("XLSFile");
            saveFile.Text = ApplicationSettings.GetString("RDBSaveFile");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = ApplicationSettings.GetString("XLSFileLocation");
            openFileDialog.ShowDialog(this);
            ApplicationSettings.SetString("XLSFileLocation", Path.GetDirectoryName(openFileDialog.FileName));
            ApplicationSettings.SetString("XLSFile", openFileDialog.FileName);
            openFile.Text = openFileDialog.FileName;
        }

        private void saveToButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = ApplicationSettings.GetString("RDBSaveLocation");
            saveFileDialog.ShowDialog();
            ApplicationSettings.SetString("RDBSaveLocation", Path.GetDirectoryName(saveFileDialog.FileName));
            ApplicationSettings.SetString("RDBSaveFile", saveFileDialog.FileName);
            saveFile.Text = saveFileDialog.FileName;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            RelayAutomator automator = new RelayAutomator(openFile.Text, saveFile.Text);

            automator.Load(0);
            automator.ApplySettingsChanges();
            automator.Save();
            automator.Close();
        }

        private void settingsTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsTreeViewer stv = new SettingsTreeViewer();
            stv.ShowDialog();
        }
    }
}
