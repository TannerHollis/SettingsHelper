using SettingsHelper;
using SettingsHelper.SettingsTreeParser;
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

namespace SettingsHelperUI
{
    public partial class SettingsTreeViewer : Form
    {
        public Relay relay;

        public SettingsTreeViewer()
        {
            InitializeComponent();

            // LoadText into field
            UpdateTextWithSettings();

            // Update DeviceTypes
            deviceTypes.Items.Clear();
            deviceTypes.Items.AddRange(RelayFiles.GetSupportedRelayTranslations());
            deviceTypes.SelectedIndex = 0;
        }

        private void UpdateTextWithSettings()
        {
            rdbFileLocation.Text = ApplicationSettings.GetString("SettingsTreeRDBFile");
            parseLabel.Text = ApplicationSettings.GetString("SettingsTreeWordBit");
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = ApplicationSettings.GetString("SettingsTreeRDBLocation");
            ofd.ShowDialog();

            if (ofd.FileName == string.Empty)
                return;

            ApplicationSettings.SetString("SettingsTreeRDBLocation", Path.GetDirectoryName(ofd.FileName));
            ApplicationSettings.SetString("SettingsTreeRDBFile", ofd.FileName);

            rdbFileLocation.Text = ofd.FileName;
        }


        public void LoadRelay()
        {
            relay = new Relay(rdbFileLocation.Text);
            SetRelayListView();
        }

        public void SetRelayListView()
        {
            relayList.Items.Clear();
            relayFileList.Items.Clear();
            foreach(string relayName in relay.GetRelayNames())
            {
                relayList.Items.Add(relayName);
            }
        }

        private void relayList_Click(object sender, EventArgs e)
        {
            if (relayList.SelectedIndex == -1)
                return;

            this.Cursor = Cursors.WaitCursor;

            relay.Load(relayList.SelectedIndex);

            relayFileList.Items.Clear();

            foreach(string relayFile in relay.GetGroupNames())
            {
                relayFileList.Items.Add(relayFile);
            }

            this.Cursor = Cursors.Default;
        }

        public void ParseForWordBit(string wordBit)
        {
            SettingsTranslator translator = new SettingsTranslator(deviceTypes.Text);

            List<SettingsGroup> groups = new List<SettingsGroup>();
            foreach(string item in relayFileList.SelectedItems)
            {
                groups.Add(relay[item]);
            }

            SettingsTreeParser parser = new SettingsTreeParser(translator, groups);

            SettingsTreeNode root = parser.ParseAtWordBit(wordBit);
            UpdateTreeView(root);
        }

        public void UpdateTreeView(SettingsTreeNode root)
        {
            settingsTree.Nodes.Clear();
            TreeNode rootNode = new TreeNode(root.ToString());

            RecurseTreeView(rootNode, root);
            
            settingsTree.Nodes.Add(rootNode);
        }

        public static void RecurseTreeView(TreeNode node, SettingsTreeNode settingNode)
        {

            for(int i = 0; i < settingNode.Children.Count(); i++)
            {
                TreeNode childNode = new TreeNode(settingNode.ChildrenGroupNames[i]);

                foreach (SettingsTreeNode child in settingNode.Children[i])
                {
                    TreeNode childChildNode = new TreeNode(child.ToString());
                    
                    RecurseTreeView(childChildNode, child);

                    childNode.Nodes.Add(childChildNode);
                }

                node.Nodes.Add(childNode);
            }
        }

        private void parseButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                ParseForWordBit(parseLabel.Text);
                ApplicationSettings.SetString("SettingsTreeWordBit", parseLabel.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Could not parse: " + parseLabel.Text, "Error", MessageBoxButtons.OK);
            }

            this.Cursor = Cursors.Default;
        }

        private void rdbFileLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                LoadRelay();
            }
        }

        private void parseLabel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                parseButton_Click(sender, e);
            }
        }
    }
}
