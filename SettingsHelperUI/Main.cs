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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog(this);
            openFile.Text = openFileDialog.FileName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void saveToButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
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
    }
}
