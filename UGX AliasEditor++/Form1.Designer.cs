namespace UGX_AliasEditor__
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.aliasListSearchbox = new System.Windows.Forms.TextBox();
            this.keyListSearchbox = new System.Windows.Forms.TextBox();
            this.valueListSearchbox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fileDropLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuRecentFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertNewAliasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateAliasToNewAliasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAliasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAliasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFromSamplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dLoopingSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dMusicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dStreamedSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dSoundToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dLoopingSoundToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dMusicToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.relocateWaWRootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 26);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(677, 558);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowDrop = true;
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 26);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(193, 558);
            this.dataGridView2.TabIndex = 2;
            // 
            // aliasListSearchbox
            // 
            this.aliasListSearchbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.aliasListSearchbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.aliasListSearchbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.aliasListSearchbox.Location = new System.Drawing.Point(0, 0);
            this.aliasListSearchbox.Name = "aliasListSearchbox";
            this.aliasListSearchbox.Size = new System.Drawing.Size(196, 20);
            this.aliasListSearchbox.TabIndex = 3;
            // 
            // keyListSearchbox
            // 
            this.keyListSearchbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.keyListSearchbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.keyListSearchbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.keyListSearchbox.Location = new System.Drawing.Point(0, 0);
            this.keyListSearchbox.Name = "keyListSearchbox";
            this.keyListSearchbox.Size = new System.Drawing.Size(337, 20);
            this.keyListSearchbox.TabIndex = 4;
            // 
            // valueListSearchbox
            // 
            this.valueListSearchbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.valueListSearchbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.valueListSearchbox.Dock = System.Windows.Forms.DockStyle.Right;
            this.valueListSearchbox.Location = new System.Drawing.Point(343, 0);
            this.valueListSearchbox.Name = "valueListSearchbox";
            this.valueListSearchbox.Size = new System.Drawing.Size(337, 20);
            this.valueListSearchbox.TabIndex = 5;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView2);
            this.splitContainer1.Panel1.Controls.Add(this.aliasListSearchbox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fileDropLabel);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.Controls.Add(this.valueListSearchbox);
            this.splitContainer1.Panel2.Controls.Add(this.keyListSearchbox);
            this.splitContainer1.Size = new System.Drawing.Size(880, 584);
            this.splitContainer1.SplitterDistance = 196;
            this.splitContainer1.TabIndex = 6;
            this.splitContainer1.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.splitContainer1_SplitterMoving);
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // fileDropLabel
            // 
            this.fileDropLabel.AutoSize = true;
            this.fileDropLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.fileDropLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileDropLabel.Location = new System.Drawing.Point(231, 289);
            this.fileDropLabel.Name = "fileDropLabel";
            this.fileDropLabel.Size = new System.Drawing.Size(219, 20);
            this.fileDropLabel.TabIndex = 6;
            this.fileDropLabel.Text = "Drop a file or use File -> Open";
            this.fileDropLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(880, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.menuRecentFile,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // menuRecentFile
            // 
            this.menuRecentFile.Name = "menuRecentFile";
            this.menuRecentFile.Size = new System.Drawing.Size(186, 22);
            this.menuRecentFile.Text = "Recent";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertNewAliasToolStripMenuItem,
            this.duplicateAliasToNewAliasToolStripMenuItem,
            this.clearAliasToolStripMenuItem,
            this.removeAliasToolStripMenuItem,
            this.addFromSamplesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // insertNewAliasToolStripMenuItem
            // 
            this.insertNewAliasToolStripMenuItem.Name = "insertNewAliasToolStripMenuItem";
            this.insertNewAliasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.insertNewAliasToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.insertNewAliasToolStripMenuItem.Text = "Add new alias";
            this.insertNewAliasToolStripMenuItem.Click += new System.EventHandler(this.insertNewAliasToolStripMenuItem_Click);
            // 
            // duplicateAliasToNewAliasToolStripMenuItem
            // 
            this.duplicateAliasToNewAliasToolStripMenuItem.Name = "duplicateAliasToNewAliasToolStripMenuItem";
            this.duplicateAliasToNewAliasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.duplicateAliasToNewAliasToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.duplicateAliasToNewAliasToolStripMenuItem.Text = "Duplicate alias";
            this.duplicateAliasToNewAliasToolStripMenuItem.Click += new System.EventHandler(this.duplicateAliasToNewAliasToolStripMenuItem_Click);
            // 
            // clearAliasToolStripMenuItem
            // 
            this.clearAliasToolStripMenuItem.Name = "clearAliasToolStripMenuItem";
            this.clearAliasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.clearAliasToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.clearAliasToolStripMenuItem.Text = "Clear alias";
            this.clearAliasToolStripMenuItem.Click += new System.EventHandler(this.clearAliasToolStripMenuItem_Click);
            // 
            // removeAliasToolStripMenuItem
            // 
            this.removeAliasToolStripMenuItem.Name = "removeAliasToolStripMenuItem";
            this.removeAliasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.removeAliasToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.removeAliasToolStripMenuItem.Text = "Remove alias";
            this.removeAliasToolStripMenuItem.Click += new System.EventHandler(this.removeAliasToolStripMenuItem_Click);
            // 
            // addFromSamplesToolStripMenuItem
            // 
            this.addFromSamplesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dSoundToolStripMenuItem,
            this.dLoopingSoundToolStripMenuItem,
            this.dMusicToolStripMenuItem,
            this.dStreamedSoundToolStripMenuItem,
            this.dSoundToolStripMenuItem1,
            this.dLoopingSoundToolStripMenuItem1,
            this.dMusicToolStripMenuItem1});
            this.addFromSamplesToolStripMenuItem.Name = "addFromSamplesToolStripMenuItem";
            this.addFromSamplesToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.addFromSamplesToolStripMenuItem.Text = "Add from Samples";
            // 
            // dSoundToolStripMenuItem
            // 
            this.dSoundToolStripMenuItem.Name = "dSoundToolStripMenuItem";
            this.dSoundToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.dSoundToolStripMenuItem.Text = "2d Sound";
            this.dSoundToolStripMenuItem.Click += new System.EventHandler(this.dSoundToolStripMenuItem_Click);
            // 
            // dLoopingSoundToolStripMenuItem
            // 
            this.dLoopingSoundToolStripMenuItem.Name = "dLoopingSoundToolStripMenuItem";
            this.dLoopingSoundToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.dLoopingSoundToolStripMenuItem.Text = "2d Looping Sound";
            this.dLoopingSoundToolStripMenuItem.Click += new System.EventHandler(this.dLoopingSoundToolStripMenuItem_Click);
            // 
            // dMusicToolStripMenuItem
            // 
            this.dMusicToolStripMenuItem.Name = "dMusicToolStripMenuItem";
            this.dMusicToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.dMusicToolStripMenuItem.Text = "2d Music";
            this.dMusicToolStripMenuItem.Click += new System.EventHandler(this.dMusicToolStripMenuItem_Click);
            // 
            // dStreamedSoundToolStripMenuItem
            // 
            this.dStreamedSoundToolStripMenuItem.Name = "dStreamedSoundToolStripMenuItem";
            this.dStreamedSoundToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.dStreamedSoundToolStripMenuItem.Text = "2d Streamed Sound";
            this.dStreamedSoundToolStripMenuItem.Click += new System.EventHandler(this.dStreamedSoundToolStripMenuItem_Click);
            // 
            // dSoundToolStripMenuItem1
            // 
            this.dSoundToolStripMenuItem1.Name = "dSoundToolStripMenuItem1";
            this.dSoundToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
            this.dSoundToolStripMenuItem1.Text = "3d Sound";
            this.dSoundToolStripMenuItem1.Click += new System.EventHandler(this.dSoundToolStripMenuItem1_Click);
            // 
            // dLoopingSoundToolStripMenuItem1
            // 
            this.dLoopingSoundToolStripMenuItem1.Name = "dLoopingSoundToolStripMenuItem1";
            this.dLoopingSoundToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
            this.dLoopingSoundToolStripMenuItem1.Text = "3d Looping Sound";
            this.dLoopingSoundToolStripMenuItem1.Click += new System.EventHandler(this.dLoopingSoundToolStripMenuItem1_Click);
            // 
            // dMusicToolStripMenuItem1
            // 
            this.dMusicToolStripMenuItem1.Name = "dMusicToolStripMenuItem1";
            this.dMusicToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
            this.dMusicToolStripMenuItem1.Text = "3d Music";
            this.dMusicToolStripMenuItem1.Click += new System.EventHandler(this.dMusicToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.relocateWaWRootToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.aboutToolStripMenuItem.Text = "Tools";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.toolStripMenuItem1.Text = "WaW Root Folder";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(198, 22);
            this.toolStripMenuItem2.Text = "WaW Soundalias Folder";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // relocateWaWRootToolStripMenuItem
            // 
            this.relocateWaWRootToolStripMenuItem.Name = "relocateWaWRootToolStripMenuItem";
            this.relocateWaWRootToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.relocateWaWRootToolStripMenuItem.Text = "Relocate WaW Root";
            this.relocateWaWRootToolStripMenuItem.Click += new System.EventHandler(this.relocateWaWRootToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 608);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "UGX AliasEditor++";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox aliasListSearchbox;
        private System.Windows.Forms.TextBox keyListSearchbox;
        private System.Windows.Forms.TextBox valueListSearchbox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Label fileDropLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuRecentFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertNewAliasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateAliasToNewAliasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAliasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAliasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relocateWaWRootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFromSamplesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dStreamedSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dSoundToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dLoopingSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dLoopingSoundToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dMusicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dMusicToolStripMenuItem1;
    }
}

