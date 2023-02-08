namespace SettingsHelperUI
{
    partial class SettingsTreeViewer
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.openButton = new System.Windows.Forms.Button();
            this.relayList = new System.Windows.Forms.ListBox();
            this.relayFileList = new System.Windows.Forms.ListBox();
            this.rdbFileLocation = new System.Windows.Forms.TextBox();
            this.deviceTypes = new System.Windows.Forms.ComboBox();
            this.viewerTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.settingsTree = new System.Windows.Forms.TreeView();
            this.parseLabel = new System.Windows.Forms.TextBox();
            this.parseButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.settingsTabPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.viewerTabPage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.settingsTabPage);
            this.tabControl1.Controls.Add(this.viewerTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.Controls.Add(this.tableLayoutPanel1);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTabPage.Size = new System.Drawing.Size(792, 424);
            this.settingsTabPage.TabIndex = 0;
            this.settingsTabPage.Text = "Settings";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.openButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.relayList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.relayFileList, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.rdbFileLocation, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.deviceTypes, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.5933F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.4067F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(786, 418);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // openButton
            // 
            this.openButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.openButton.Location = new System.Drawing.Point(612, 18);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(149, 23);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "OpenRDB File...";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // relayList
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.relayList, 2);
            this.relayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relayList.FormattingEnabled = true;
            this.relayList.Location = new System.Drawing.Point(3, 63);
            this.relayList.Name = "relayList";
            this.relayList.Size = new System.Drawing.Size(386, 352);
            this.relayList.TabIndex = 2;
            this.relayList.Click += new System.EventHandler(this.relayList_Click);
            // 
            // relayFileList
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.relayFileList, 2);
            this.relayFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relayFileList.FormattingEnabled = true;
            this.relayFileList.Location = new System.Drawing.Point(395, 63);
            this.relayFileList.Name = "relayFileList";
            this.relayFileList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.relayFileList.Size = new System.Drawing.Size(388, 352);
            this.relayFileList.TabIndex = 3;
            // 
            // rdbFileLocation
            // 
            this.rdbFileLocation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.rdbFileLocation, 2);
            this.rdbFileLocation.Location = new System.Drawing.Point(199, 20);
            this.rdbFileLocation.Name = "rdbFileLocation";
            this.rdbFileLocation.Size = new System.Drawing.Size(386, 20);
            this.rdbFileLocation.TabIndex = 1;
            this.rdbFileLocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rdbFileLocation_KeyPress);
            // 
            // deviceTypes
            // 
            this.deviceTypes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.deviceTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceTypes.FormattingEnabled = true;
            this.deviceTypes.Items.AddRange(new object[] {
            "SEL 351-7",
            "SEL 351S",
            "SEL 351A"});
            this.deviceTypes.Location = new System.Drawing.Point(20, 19);
            this.deviceTypes.Name = "deviceTypes";
            this.deviceTypes.Size = new System.Drawing.Size(156, 21);
            this.deviceTypes.TabIndex = 4;
            // 
            // viewerTabPage
            // 
            this.viewerTabPage.Controls.Add(this.tableLayoutPanel2);
            this.viewerTabPage.Location = new System.Drawing.Point(4, 22);
            this.viewerTabPage.Name = "viewerTabPage";
            this.viewerTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.viewerTabPage.Size = new System.Drawing.Size(792, 424);
            this.viewerTabPage.TabIndex = 1;
            this.viewerTabPage.Text = "Viewer";
            this.viewerTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.86259F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.13741F));
            this.tableLayoutPanel2.Controls.Add(this.settingsTree, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.parseLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.parseButton, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(786, 418);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // settingsTree
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.settingsTree, 2);
            this.settingsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTree.Location = new System.Drawing.Point(3, 33);
            this.settingsTree.Name = "settingsTree";
            this.settingsTree.Size = new System.Drawing.Size(780, 382);
            this.settingsTree.TabIndex = 0;
            // 
            // parseLabel
            // 
            this.parseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.parseLabel.Location = new System.Drawing.Point(3, 5);
            this.parseLabel.Name = "parseLabel";
            this.parseLabel.Size = new System.Drawing.Size(212, 20);
            this.parseLabel.TabIndex = 1;
            this.parseLabel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.parseLabel_KeyPress);
            // 
            // parseButton
            // 
            this.parseButton.Location = new System.Drawing.Point(221, 3);
            this.parseButton.Name = "parseButton";
            this.parseButton.Size = new System.Drawing.Size(75, 23);
            this.parseButton.TabIndex = 2;
            this.parseButton.Text = "Parse";
            this.parseButton.UseVisualStyleBackColor = true;
            this.parseButton.Click += new System.EventHandler(this.parseButton_Click);
            // 
            // SettingsTreeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "SettingsTreeViewer";
            this.Text = "Settings Tree Viewer";
            this.tabControl1.ResumeLayout(false);
            this.settingsTabPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.viewerTabPage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.TabPage viewerTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.TextBox rdbFileLocation;
        private System.Windows.Forms.ListBox relayList;
        private System.Windows.Forms.ListBox relayFileList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TreeView settingsTree;
        private System.Windows.Forms.TextBox parseLabel;
        private System.Windows.Forms.Button parseButton;
        private System.Windows.Forms.ComboBox deviceTypes;
    }
}