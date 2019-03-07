using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using Microsoft.Win32; //Registry stuff
using JWC; //MRU List

namespace UGX_AliasEditor__
{
    public partial class Form1 : Form
    {
        bool anyUnsavedChanges = false;
        bool fileReadingComplete = false;
        string openedFile;
        string fileName1;
        List<List<string>> aliasList;
        List<string> originalAliasList;
        List<string> addedAliasList = new List<string>();
        List<string> lines;
        StreamReader sourceReader;

        protected MruStripMenu mruMenu;
        static string mruRegKey = "SOFTWARE\\UGX\\AliasEditor";

        static BackgroundWorker _bw = new BackgroundWorker();

        ContextMenuStrip editContextMenu = new ContextMenuStrip();

        public Form1()
        {
            InitializeComponent();
            this.Text = "UGX AliasEditor++ v" + Application.ProductVersion.Substring(0, 5);
            Version nonBeta = new Version(1, 0, 0, 0);
            if (System.Reflection.Assembly.GetExecutingAssembly().GetName().Version < nonBeta)
                this.Text += " BETA";

            _bw.DoWork += checkForUpdates;
            _bw.RunWorkerAsync();

            mruMenu = new MruStripMenuInline(fileToolStripMenuItem, menuRecentFile, new MruStripMenu.ClickedHandler(OnMruFile), mruRegKey + "\\MRU", 16);

            dataGridView1.DragEnter += dragEnter;
            dataGridView1.DragDrop += dragDrop;
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            dataGridView1.MouseWheel += new MouseEventHandler(dataGridViews_MouseWheel);
            dataGridView1.MouseEnter += dataGridViews_MouseEnter;
            dataGridView2.DragEnter += dragEnter;
            dataGridView2.DragDrop += dragDrop;
            dataGridView2.CellEnter += dataGridView1_CellDoubleClick;
            dataGridView2.MouseWheel += new MouseEventHandler(dataGridViews_MouseWheel);
            dataGridView2.MouseEnter += dataGridViews_MouseEnter;

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].HeaderText = "Keys";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "Values";
            dataGridView1.MultiSelect = false;

            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ColumnCount = 1;
            dataGridView2.Columns[0].HeaderText = "Alias List";
            dataGridView2.MultiSelect = false;

            insertNewAliasToolStripMenuItem.Enabled = false;
            duplicateAliasToNewAliasToolStripMenuItem.Enabled = false;
            removeAliasToolStripMenuItem.Enabled = false;
            clearAliasToolStripMenuItem.Enabled = false;
            addFromSamplesToolStripMenuItem.Enabled = false;

            aliasListSearchbox.KeyDown += aliasListSearchbox_KeyDown;
            keyListSearchbox.KeyDown += keyListSearchbox_KeyDown;
            valueListSearchbox.KeyDown += valueListSearchbox_KeyDown;
            setAllWatermarks();

            editContextMenu.Enabled = false;
            Image img = null;
            editContextMenu.Items.Add("Add alias", img, new System.EventHandler(ContextMenuClick));
            editContextMenu.Items.Add("Duplicate alias", img, new System.EventHandler(ContextMenuClick));
            editContextMenu.Items.Add("Clear alias", img, new System.EventHandler(ContextMenuClick));
            editContextMenu.Items.Add("Remove alias", img, new System.EventHandler(ContextMenuClick));
            dataGridView2.ContextMenuStrip = editContextMenu;
            dataGridView2.MouseClick += dataGridView2_MouseClick;
        }

        private void setAllWatermarks()
        {
            aliasListSearchbox.SetWatermark("Search for alias name...");
            keyListSearchbox.SetWatermark("Search for setting name...");
            valueListSearchbox.SetWatermark("Search for setting value...");
        }
        private void OnMruFile(int number, String filename)
        {
            openFile(filename);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.N))
            {
                newToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.O))
            {
                openToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                saveToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Shift | Keys.S))
            {
                saveAsToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Shift | Keys.A))
            {
                insertNewAliasToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Shift | Keys.D))
            {
                duplicateAliasToNewAliasToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Shift | Keys.C))
            {
                clearAliasToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Shift | Keys.X))
            {
                removeAliasToolStripMenuItem_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void setAllTooltips()
        {
            createTip("name", "The soundalias name which is used to refer to the sound in GSC and CSC scripts.");
            createTip("platform", "Is this soundalias platform-specific? Irrelevant for PC mods.");
            createTip("file", "The path to the .wav file within root/sound_assets/Raw/sound/. Be sure to include the .wav file extension.");
            createTip("sequence", "If this file has multiple versions, this is the index which identifies this wav file from the others. To set up multiple wavs under the same alias name, create \na folder which matches the alias name and name the wav files aliasname_xx where xx is replaced with a two digit index number. Refer to other aliases as an example.");
            createTip("vol_min", "The lowest volume that this sound can fade to over the distance of the player from the sound origin. Only applies to 3d sounds.");
            createTip("vol_max", "The highest volume that this sound can fade to when the player is right next to the origin of the sound. Only applies to 3d sounds.");
            createTip("vol_mod", "Leaving blank causes no effect on vol_min and vol_max, otherwise the string must match a string in the volumemodgroups.def file and the value in that file corresponding to that string will be used to adjust vol_min and vol_max, clamped to the valid range");
            createTip("dist_min", "The minimum distance the player must be from the sound origin in order to hear the volume start to fade towards vol_min. vol_max ends at this distance. Only applies to 3d sounds.");
            createTip("dist_max", "The maximum distance the player can be from the sound origin in order to hear the sound at all. vol_min starts at this distance. Only applies to 3d sounds.");
            createTip("pitch_min", "The minimum pitch for the sound to be played at. 1 is normal playback, 2 is twice as fast, 0.5 is half as fast (default = 1)");
            createTip("pitch_max", "The maximum pitch for the sound to be played at. 1 is normal playback, 2 is twice as fast, 0.5 is half as fast (default = 1)");
            createTip("envelop percentage", "Amount of omni-directionality to apply.");
            createTip("envelop_min", "Any sounds within this distance of the player will use the full envelop percentage.");
            createTip("envelop_max", "A player between enevlop_min and envelop_max uses a fraction of the envelop percentage.");
            createTip("occlusion_level", "The level of occlusion to apply.");
            createTip("occlusion_wet_dry", "The balance of wet vs dry occlusion from 0 to 1.");
            createTip("limit_count", "The max amount of this alias to play at once. Default is 1.");
            createTip("limit_type", "Tells the engine what to do if there is more than one instance of this alias playing. Value can be reject, oldest, priority.");
            createTip("entity_limit_count", "The total number of an alias that can be played off a single entity simultaneously.");
            createTip("entity_limit_type", "Tells the engine what to do if there is more than the allowed amount of this alias playing from the same entity. Value can be priority, oldest, reject.");
            createTip("bus", "The bus which the sound plays on. The bus is the engine channel the sound is played through. Some bus volumes can be changed by the player via the Sound Options menu. \nA list of values and their respective volume levels can be found in root/raw/soundaliases/global/bus.csv ");
            createTip("volume_min_falloff_curve", "??");
            createTip("volumefalloffcurve", "If blank, uses standard linear curve which cannot be changed. A string \"XXXX\" corresponds to the curve defined by the file soundaliases/globals/curves.csv");
            createTip("reverb", "Blank means the alias is affected normally by wet and dry levels, \"fulldrylevel\" forces the alias to use a full drylevel (ignoring the global drylevel), \"nowetlevel\" forces the alias to use no wetlevel (ignoring the global wetlevel)");
            createTip("reverb_send", "The amount of wet signal to pass to the player (reverb is wet, untouched audio is dry) values from (0-1) Default: 0.5");
            createTip("dist_reverb_max", "How far wet audio can be heard from its origin.");
            createTip("reverb_min_falloff_curve", "??");
            createTip("reverb_falloff_curve", "This is the rate at which the wet signal of the reverb drops to 0 from the point of origin of a 3d sound to the dist_reverb_max.  ");
            createTip("randomize_type", "Leave blank for no randomization. Set to \"pitch\" to randomize the pitch.");
            createTip("spacialized", "Whether this sound is 2d or 3d. 2d sounds are always full volume, 3d sounds vary in volume depending on player distance from the sound origin.");
            createTip("type", "Streamed sounds are always 2d full volume and loaded from the IWD instead of the FF. To load a sound from FF instead of IWD, leave this setting blank or set to \"loaded\". Looping sounds must be blank/loaded. Streamed sounds do not count \ntowards the 1600 sound limit and should be used instead of non-streamed 2d sounds in nearly all circumstances to conserve room.");
            createTip("probability", "(Decimal 0 to 1) The probability of this sound to play when called. If left blank, probability is 100% (1).");
            createTip("loop", "Should this sound loop indefinitely? Set to \"looping\" if yes, leave blank (or set to \"nonlooping\") if no. Looped sounds must be played using playloopsound() and stopped with stoploopsound()");
            createTip("masterslave", "if \"master\", this is a master sound.  If a number, then this sound's volume will be multiplied by that number (a percentage between 0 and 1) while any master sound is playing.  If blank, then it is neither master nor slave.");
            createTip("loadspec", "Basically a \"category\" for the soundalias. When including an alias in an FF, you can optionally specifiy a loadspec name after the alias filename in order to only load files which contain the specified loadspec in their line. Ex: sound,weapons,all_sp will only load soundaliases which have all_sp as a loadspec parameter. \nYou can specify as many loadspec names as you wish on the alias line.");
            createTip("subtitle", "If cg_subtitles is set to 1 for the player, the engine will display the localizedstring set here while the sound is playing.");
            createTip("compression", "A string corresponding to an entry in \"XMAUpdate.tbl\" which is used to determine compression by XMAUpdate.exe");
            createTip("secondaryaliasname", "You can layer one soundalias on top of another by specifying another soundalias name here.");
            createTip("chainaliasname", "You can specify even more aliases to chain together. They will all play simultaneously.");
            createTip("startdelay", "You can delay the sound from playing immediately by specifying a time in milliseconds here.");
            createTip("real_delay", "When a sound is heard from a distance in real life, there is sometimes a slight delay as it travels from the source to you. This parameter allows you to simulate this effect in game. Valid vales are yes, no. Blank = no.");
            createTip("distance_lpf", "(lLPF = Low-Pass Filter) Makes audio appear to be played from a father distance. (yes/no). Default/Blank: No");
            createTip("speakermap", "If blank, uses the default speakermappings which cannot be changed. A string \"XXXX\" corresponds to the speakermap defined by the file soundaliases/globals/speakermap.csv. Speakermaps are used for surround-sound output.");
            createTip("lfe percentage", "This determines what percentage of the highest calculated spatialized speaker volume should be passed to the LFE. blank means no LFE for the sound");
            createTip("center percentage", "This determines what percentage of the volume should be redirected to the center channel (equal percentage taken from all speakers).");
            createTip("move_type", "Uses \"Treyarch Flux System\" for sound reflection and shockwaves. Plays sound in a V pattern from the point of origin. Value can be left_shot, right_shot, left_player, right_player.");
            createTip("move_time", "Value in seconds that it takes for the sound to travel down its corresponding line in the V to its \"dist_max\" setting.");
            createTip("min_priority", "This is a sounds minimum priority. It will never drop below this number. 0 - 100");
            createTip("max_priority", " This is a sounds maximum priority. It will never be above this number. 0 - 100");
            createTip("min_priority_threshold", "(Decimal 0 to 1) This is the calculated volume number at which min_priority is reached.");
            createTip("max_priority_threshold", "(Decimal 0 to 1) This is the calculated volume number at which max_priority is reached. ");
            createTip("doppler", "??");
            createTip("isbig", "This is a global occlusion modifier for sounds that you are implying are very loud...what it does is lessen the amount a sound is occluded if it goes off near you but behind an obstacle...\nimagine a grenade going off just on the other side of a wall. You wouldn't want this sound to be as occluded as you would if it went off behind a wall 30 feet away. IsBig takes only yes, no. Blank = no.");
        }
        private void createTip(string setting, string tooltip)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(setting))
                    {
                        row.Cells[0].ToolTipText = tooltip;
                        row.Cells[1].ToolTipText = tooltip;
                    }
                }
            }
            catch (Exception) { }
        }

        private void aliasListSearchbox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                string searchValue = searchBox.Text;

                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(searchValue))
                        {
                            row.Selected = true;
                            refreshDataGrid(row.Index);
                            dataGridView2.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
        private void keyListSearchbox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                string searchValue = searchBox.Text;

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(searchValue))
                        {
                            Console.WriteLine("Found " + searchValue);
                            row.Selected = true;
                            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
        private void valueListSearchbox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                string searchValue = searchBox.Text;

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(searchValue))
                        {
                            Console.WriteLine("Found " + searchValue);
                            row.Selected = true;
                            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
        private void dragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void dragDrop(object sender, DragEventArgs e)
        {
            fileReadingComplete = false;
            anyUnsavedChanges = false;
            Console.WriteLine("============== FILE DROP ===============");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    // Code to read the contents of the text file
                    if (File.Exists(fileLoc))
                    {
                        openFile(fileLoc);
                    }
                }
            }
        }
        private void openFile(string fileLoc)
        {
            Console.WriteLine("============== NEW FILE ===============");
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            //ClearAllControls();
            fileName1 = Path.GetFileName(fileLoc);
            dataGridView2.Columns[0].HeaderText = "\"" +  fileName1 + "\" aliases";
            openedFile = fileLoc;
            //consoleOut("Loaded " + fileLoc);
            fileDropLabel.Visible = false;
            fileReadingComplete = false;

            #region File Reading
            //Store each key of the input file as a string for the AutoComplete searchBox
            var aliasListAutoComplete = new AutoCompleteStringCollection();
            var keysListAutoComplete = new AutoCompleteStringCollection();
            var valuesListAutoComplete = new AutoCompleteStringCollection();

            //Store each key/value pair of the input file within a Dictionary for read/write.
            sourceReader = new StreamReader(File.OpenRead(fileLoc));

            aliasList = new List<List<string>>();
            originalAliasList = new List<string>();

            lines = new List<string>(); //these will be used when they want to overwrite the current file.
            int counter = 0;
            while (!sourceReader.EndOfStream)
            {
                var line = sourceReader.ReadLine();
                lines.Add(line);

                #region Values that are ignored
                if (line.Length == 0)
                {
                    counter++;
                    continue; //skip blank lines
                }
                string commentCheck = line.Substring(0, 1);
                if (commentCheck == "#" || commentCheck == "," || commentCheck == "\"")
                {
                    counter++;
                    continue; //Skip Comments # , "
                }
                #endregion

                //Console.WriteLine(line);
                var values = line.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    if (counter == 0) //read the setting names into the first column of right grid
                    {
                        if (values[i].Length == 0) dataGridView1.Rows.Add("<null>");
                        else dataGridView1.Rows.Add(values[i]);
                        keysListAutoComplete.Add(values[i]);
                    }
                    else if (counter > 0)
                    {
                        if (i == 0) //read the alias names into the first column of the left grid
                        {
                            dataGridView2.Rows.Add(values[i]); //Populate the Alias Names DGV
                            List<string> settingsList = new List<string>(); //create a list to store the values of this alias line
                            aliasList.Add(settingsList); //Add the alias' list to the alias list for accessing.
                            originalAliasList.Add(""); //add a placeholder
                            aliasListAutoComplete.Add(values[i]);
                        }
                        aliasList[aliasList.Count - 1].Add(values[i]); //Add the current setting to the corresponding settingsList within aliasList
                        //dataGridView1.Rows[i].Cells[1].Value = values[i]; //add the value to the cell, add this to a refresh func for the table
                        valuesListAutoComplete.Add(values[i]);
                    }
                }
                counter++;
            }
            sourceReader.Close();
            aliasListSearchbox.AutoCompleteCustomSource = aliasListAutoComplete;
            keyListSearchbox.AutoCompleteCustomSource = keysListAutoComplete;
            valueListSearchbox.AutoCompleteCustomSource = valuesListAutoComplete;
            #endregion

            refreshDataGrid(0); //display the correct data for the selected table item.
            mruMenu.AddFile((String)fileLoc);
            editContextMenu.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            insertNewAliasToolStripMenuItem.Enabled = true;
            duplicateAliasToNewAliasToolStripMenuItem.Enabled = true;
            removeAliasToolStripMenuItem.Enabled = true;
            clearAliasToolStripMenuItem.Enabled = true;
            addFromSamplesToolStripMenuItem.Enabled = true;
            mruMenu.SaveToRegistry();
            setAllWatermarks();
            setAllTooltips();
            fileReadingComplete = true;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Soundalias Files|*.csv";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.CheckFileExists)
                {
                    openFile(openFileDialog1.FileName);
                }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!fileReadingComplete) return;
            //No dialog necessary, open a write steam to the global file loc
            saveFile(openedFile);
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!fileReadingComplete) return;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            // Set filter options and filter index.
            saveFileDialog1.Filter = "Soundalias Files|*.csv";
            saveFileDialog1.FilterIndex = 1;

            // Process input if the user clicked OK.
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFile(saveFileDialog1.FileName);
            }
        }
        private void saveFile(string fileLoc)
        {
            Console.WriteLine("Writing file: " + fileLoc);

            #region File Writing
            StreamWriter sourceWriter = new StreamWriter(fileLoc); //this file is in use by line 415 (above) when you save changes to the current file... 
            //We may need to change the code to not read and write simultaneously, or store the file in memory instead of re-opening it for reading.

            int counter = 0;
            for (int k = 0; k < lines.Count; k++)
            {
                var line = lines[k];

                #region Values that pass through to the output
                if (counter == 0)
                {
                    sourceWriter.WriteLine(line);
                    Console.WriteLine("1: " + line);
                    counter++;
                    continue; //skip reading the first line
                }
                if (line.Length == 0)
                {
                    sourceWriter.WriteLine(line);
                    Console.WriteLine("2: " + line);
                    counter++;
                    continue; //skip blank lines
                }
                string commentCheck = line.Substring(0, 1);
                if (commentCheck == "#" || commentCheck == "," || commentCheck == "\"")
                {
                    sourceWriter.WriteLine(line);
                    Console.WriteLine("3: " + line);
                    counter++;
                    continue; //Skip Comments # , "
                }
                #endregion

                var values = line.Split(',');
                for (int i = 0; i < aliasList.Count; i++)
                {
                    int settingIndex = 0;
                    if (dataGridView2.Rows[i].Cells[0].Value.ToString().Equals(values[0]) || (i < originalAliasList.Count && originalAliasList[i].Equals(values[0]))) //we found a match for an alias whose name did not change, write it to file.
                    {
                        foreach (string setting in aliasList[i])
                        {
                            if (settingIndex > 0)
                            {
                                sourceWriter.Write(','); //if this is the last setting of the line, don't put a ,
                                Console.Write(',');
                            }
                            sourceWriter.Write(setting);
                            Console.Write(setting);
                            settingIndex++;
                        }
                    }
                }
                sourceWriter.Write('\n');
                Console.Write('\n');
                counter++;
            }
            
            sourceWriter.Close();
            anyUnsavedChanges = false;
            #endregion
        }

        void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dataGridView2.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow >= 0)
                {
                    editContextMenu.Items.Add(string.Format("", currentMouseOverRow.ToString()));
                }
                editContextMenu.Show(dataGridView2, new Point(e.X, e.Y));
            }
        }
        private void ContextMenuClick(Object sender, System.EventArgs e)
        {
            switch (sender.ToString().Trim())
            {
                case "Add alias":
                    addAlias(null);
                    break;
                case "Duplicate alias":
                    duplicateAlias();
                    break;
                case "Clear alias":
                    clearAlias();
                    break;
                case "Remove alias":
                    removeAlias();
                    break;
            }
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 1) return; //ignore any clicks from the readonly column.
            Console.WriteLine("Value: " + e.FormattedValue);
            string setting = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (setting == "name") //they changed the alias name, we need to refresh the Alias List
            {
                DataGridViewRow foundRow = dataGridView2.CurrentRow;
                int index = foundRow.Index;
                originalAliasList[index] = aliasList[index][0]; // save the the original value so we know where to put it in the saved file.
                aliasList[index][0] = e.FormattedValue.ToString();
                foundRow.SetValues(e.FormattedValue);
                Console.WriteLine("name updated");
            }
            aliasList[dataGridView2.SelectedCells[0].RowIndex][e.RowIndex] = (string)e.FormattedValue;
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (fileReadingComplete)
                anyUnsavedChanges = true;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (!fileReadingComplete) return;
            Console.WriteLine("Cell " + e.RowIndex + " clicked.");
            refreshDataGrid(e.RowIndex);
        }
        private void addAlias(string type)
        {
            if (!fileReadingComplete) return;
            string name = "newAlias";
            if (type != null)
                name = "new" + type.ToString() + "Alias";
            addedAliasList.Add(name); //keep track of what we add so that we can add it to the saved file.
            List<string> blankSettings = new List<string>();
            dataGridView2.Rows.Add(name);
            blankSettings.Add(name);
            for (int i = 1; i < dataGridView1.Rows.Count; i++)
            {
                #region Default Settings
                if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("spacialized"))
                {
                    if(type.Equals("2d"))                blankSettings.Add("2d");
                    else if (type.Equals("2d_streamed")) blankSettings.Add("2d");
                    else if (type.Equals("2d_music"))    blankSettings.Add("2d");
                    else if (type.Equals("2d_looping"))  blankSettings.Add("2d");
                    else if (type.Equals("3d"))          blankSettings.Add("3d");
                    else if (type.Equals("3d_looping"))  blankSettings.Add("3d");
                    else if (type.Equals("3d_music"))    blankSettings.Add("3d");
                    else                                 blankSettings.Add("");
                }
                else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("type"))
                {
                    if (type.Equals("2d"))               blankSettings.Add("loaded");
                    else if (type.Equals("2d_streamed")) blankSettings.Add("streamed");
                    else if (type.Equals("2d_music"))    blankSettings.Add("streamed");
                    else if (type.Equals("2d_looping"))  blankSettings.Add("loaded");
                    else if (type.Equals("3d"))          blankSettings.Add("loaded");
                    else if (type.Equals("3d_looping"))  blankSettings.Add("loaded");
                    else                                 blankSettings.Add("");
                }
                else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("loop"))
                {
                    if (type.Equals("2d"))               blankSettings.Add("nonlooping");
                    else if (type.Equals("2d_streamed")) blankSettings.Add("nonlooping");
                    else if (type.Equals("2d_music"))    blankSettings.Add("nonlooping");
                    else if (type.Equals("2d_looping"))  blankSettings.Add("looping");
                    else if (type.Equals("3d"))          blankSettings.Add("nonlooping");
                    else if (type.Equals("3d_looping"))  blankSettings.Add("looping");
                    else if (type.Equals("3d_music"))    blankSettings.Add("nonlooping");
                    else blankSettings.Add("");
                }
                else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("bus"))
                {
                    if (type.Equals("2d"))               blankSettings.Add("full_vol");
                    else if (type.Equals("2d_streamed")) blankSettings.Add("full_vol");
                    else if (type.Equals("2d_music"))    blankSettings.Add("music");
                    else if (type.Equals("2d_looping"))  blankSettings.Add("full_vol");
                    else if (type.Equals("3d"))          blankSettings.Add("full_vol");
                    else if (type.Equals("3d_looping"))  blankSettings.Add("full_vol");
                    else if (type.Equals("3d_music"))    blankSettings.Add("music");
                    else blankSettings.Add("");
                }
                else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("vol_min"))
                {
                    if (type.Equals("2d")) blankSettings.Add("1");
                    else if (type.Equals("2d_streamed")) blankSettings.Add("1");
                    else if (type.Equals("2d_music")) blankSettings.Add("1");
                    else if (type.Equals("2d_looping")) blankSettings.Add("1");
                    else if (type.Equals("3d")) blankSettings.Add("0.5");
                    else if (type.Equals("3d_looping")) blankSettings.Add("0.5");
                    else if (type.Equals("3d_music")) blankSettings.Add("0.5");
                    else blankSettings.Add("");
                }
                else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("vol_max"))
                {
                    if (type.Equals("2d")) blankSettings.Add("1");
                    else if (type.Equals("2d_streamed")) blankSettings.Add("1");
                    else if (type.Equals("2d_music")) blankSettings.Add("1");
                    else if (type.Equals("2d_looping")) blankSettings.Add("1");
                    else if (type.Equals("3d")) blankSettings.Add("1");
                    else if (type.Equals("3d_looping")) blankSettings.Add("1");
                    else if (type.Equals("3d_music")) blankSettings.Add("1");
                    else blankSettings.Add("");
                }
                else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("dist_min"))
                {
                    if (type.Equals("3d")) blankSettings.Add("120");
                    else if (type.Equals("3d_looping")) blankSettings.Add("120");
                    else if (type.Equals("3d_music")) blankSettings.Add("120");
                    else blankSettings.Add("");
                }
                else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("dist_max"))
                {
                    if (type.Equals("3d")) blankSettings.Add("1200");
                    else if (type.Equals("3d_looping")) blankSettings.Add("1200");
                    else if (type.Equals("3d_music")) blankSettings.Add("1200");
                    else blankSettings.Add("");
                }
                else
                    blankSettings.Add("");
                #endregion
            }
            aliasList.Add(blankSettings);
            dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0];
            dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Index;
            refreshDataGrid(dataGridView2.Rows.Count - 1);
        }
        private void duplicateAlias()
        {
            if (!fileReadingComplete) return;
            int index = dataGridView2.CurrentCell.RowIndex;

            addedAliasList.Add(aliasList[index][0]); //keep track of what we add so that we can add it to the saved file.
            List<string> blankSettings = new List<string>();
            dataGridView2.Rows.Add(aliasList[index][0]);
            for (int i = 0; i < aliasList[index].Count; i++)
                blankSettings.Add(aliasList[index][i]);
            aliasList.Add(blankSettings);
            
            dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0];
            dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Index;
            refreshDataGrid(dataGridView2.Rows.Count - 1);
        }
        private void clearAlias()
        {
            if (!fileReadingComplete) return;
            int index = dataGridView2.CurrentCell.RowIndex;
            for (int i = 1; i < aliasList[index].Count; i++)
                aliasList[index][i] = "";
            refreshDataGrid(index);
        }
        private void removeAlias()
        {
            if (!fileReadingComplete) return;
            if (dataGridView2.RowCount <= 1) return;
            int index = dataGridView2.CurrentCell.RowIndex;
            int dest = index - 1;
            if (index == 0) dest = index + 1;            
            aliasList.RemoveAt(index);
            dataGridView2.Rows.RemoveAt(index);
            if (index == dataGridView2.Rows.Count) index = index - 1;
            dataGridView2.CurrentCell = dataGridView2.Rows[index].Cells[0];
            dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.Rows[index].Index;
            refreshDataGrid(index);
        }
        private void newFile()
        {
            //Fill with blank stuff
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns[0].HeaderText = "aliases";
            fileDropLabel.Visible = false;
            aliasList = new List<List<string>>();
            originalAliasList = new List<string>();
            lines = new List<string>();
            lines.Add("name,platform,file,sequence,vol_min,vol_max,dist_min,dist_max,limit_count,limit_type,entity_limit_count,entity_limit_type,bus,volume_min_falloff_curve,volumefalloffcurve,reverb_send,dist_reverb_max,reverb_min_falloff_curve,reverb_falloff_curve,pitch_min,pitch_max,randomize_type,spatialized,type,probability,loop,masterslave,loadspec,subtitle,compression,secondaryaliasname,chainaliasname,startdelay,speakermap,lfe percentage,center percentage,envelop_min,envelop_max,envelop percentage,occlusion_level,occlusion_wet_dry,real_delay,distance_lpf,move_type,move_time,min_priority,max_priority,min_priority_threshold,max_priority_threshold,doppler,isbig");
            var aliasListAutoComplete = new AutoCompleteStringCollection();
            var keysListAutoComplete = new AutoCompleteStringCollection();
            var valuesListAutoComplete = new AutoCompleteStringCollection();

            var values = lines[0].Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                dataGridView1.Rows.Add(values[i]);
                keysListAutoComplete.Add(values[i]);

                if (i == 0) //read the alias names into the first column of the left grid
                {
                    dataGridView2.Rows.Add("newAlias"); //Populate the Alias Names DGV
                    List<string> settingsList = new List<string>(); //create a list to store the values of this alias line
                    settingsList.Add("newAlias");
                    aliasList.Add(settingsList); //Add the alias' list to the alias list for accessing.
                    originalAliasList.Add(""); //add a placeholder
                    aliasListAutoComplete.Add(values[i]);
                }
                aliasList[aliasList.Count - 1].Add(""); //Add the current setting to the corresponding settingsList within aliasList
                //dataGridView1.Rows[i].Cells[1].Value = values[i]; //add the value to the cell, add this to a refresh func for the table
                valuesListAutoComplete.Add(values[i]);
            }
            aliasListSearchbox.AutoCompleteCustomSource = aliasListAutoComplete;
            keyListSearchbox.AutoCompleteCustomSource = keysListAutoComplete;
            valueListSearchbox.AutoCompleteCustomSource = valuesListAutoComplete;
            fileReadingComplete = true;
            editContextMenu.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            insertNewAliasToolStripMenuItem.Enabled = true;
            duplicateAliasToNewAliasToolStripMenuItem.Enabled = true;
            removeAliasToolStripMenuItem.Enabled = true;
            clearAliasToolStripMenuItem.Enabled = true;
            addFromSamplesToolStripMenuItem.Enabled = true;

            setAllWatermarks();
            setAllTooltips();
            refreshDataGrid(0);
        }

        private void refreshDataGrid(int aliasIndex)
        {
            fileReadingComplete = false; //prevent anything from picking these up as value changes.
            if (aliasList.Count <= 0) return;
            try
            {
                for (int i = 0; i < aliasList[aliasIndex].Count - 1; i++)
                    dataGridView1.Rows[i].Cells[1].Value = aliasList[aliasIndex][i];
            }
            catch (Exception)
            {
                MessageBox.Show("Could not open soundalias .csv file. Most likely it is not a soundalias file!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            fileReadingComplete = true;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            keyListSearchbox.Width = splitContainer1.Panel2.Width / 2 + 2; //The +- 2 is because they aren't perfectly centered otherwise, a quirk
            valueListSearchbox.Width = splitContainer1.Panel2.Width / 2 - 5;
        }
        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            Form1_Resize(sender, e);
        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Form1_Resize(sender, e);
        }

        #region Folder and URL links, simple stuff
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path = GetRootFolder();
            Console.WriteLine(path);
            if (Directory.Exists(path))
                System.Diagnostics.Process.Start(path);
            else
                RelocateCoDWaW();
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string path = GetRootFolder() + "raw\\soundaliases";
            Console.WriteLine(path);
            if (Directory.Exists(path))
                System.Diagnostics.Process.Start(path);
            else
                RelocateCoDWaW();
        }
        private void relocateWaWRootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RelocateCoDWaW();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://ugx-mods.com/forum");
        }
        private string GetRootFolder()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Activision\\Call of Duty WAW");
            string path = Convert.ToString(key.GetValue("InstallPath"));
            if (string.IsNullOrWhiteSpace(path)) path = "";
            return GetActualDirectoryPath(path);
        }
        private string GetActualDirectoryPath(string path)
        {
            if (path.Length == 0)
                return path;
            if (path[path.Length - 1] != '\\')
                path += "\\";
            return path;
        }
        private void RelocateCoDWaW()
        {
            //Prompt them to find their root folder.
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            fbd.Description = "Your CoDWaW installation could not be found. Please navigate to it. The location will be saved to your registry so that other programs can correctly find your installation.";
            DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;
            string path = fbd.SelectedPath;
            //guy didn't install the game legitimately, create the key for him to prevent problems with less-intelligent programs :P
            Microsoft.Win32.RegistryKey newkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Activision\\Call of Duty WAW");
            newkey.SetValue("InstallPath", path);
        }
        #endregion

        void dataGridViews_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!fileReadingComplete) return;
            DataGridView grid = sender as DataGridView;
            if (grid.RowCount <= 1) return;
            int currentIndex = grid.FirstDisplayedScrollingRowIndex;
            int scrollLines = SystemInformation.MouseWheelScrollLines;

            if (e.Delta > 0)
            {
                grid.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
            }
            else if (e.Delta < 0)
            {
                grid.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
            }
        }
        private void dataGridViews_MouseEnter(object sender, EventArgs e)
        {
            if (!fileReadingComplete) return;
            DataGridView grid = sender as DataGridView;
            grid.Focus();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newFile();
        }
        private void insertNewAliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addAlias(null);
        }
        private void duplicateAliasToNewAliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            duplicateAlias();
        }
        private void clearAliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearAlias();
        }
        private void removeAliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeAlias();
        }
        private void dSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addAlias("2d");
        }
        private void dStreamedSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addAlias("2d_streamed");
        }
        private void dSoundToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addAlias("3d");
        }
        private void dLoopingSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addAlias("2d_looping");
        }
        private void dLoopingSoundToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addAlias("3d_looping");
        }
        private void dMusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addAlias("2d_music");
        }
        private void dMusicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addAlias("3d_music");
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        #region Updating
        static void checkForUpdates(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("======== CHECKING FOR UPDATES ========");
            // in newVersion variable we will store the  
            // version info from xml file  
            Version newVersion = null;
            // and in this variable we will put the url we  
            // would like to open so that the user can  
            // download the new version  
            // it can be a homepage or a direct  
            // link to zip/exe file  
            string url = "http://ugx-mods.com/downloads/aliaseditor/version.xml";
            string desc = "";
            try
            {
                XmlTextReader reader = new XmlTextReader(url);
                string elementName = "";
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                        elementName = reader.Name;
                    else
                    {
                        // for text nodes...  
                        if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                        {
                            // we check what the name of the node was  
                            switch (elementName)
                            {
                                case "version":
                                    Console.WriteLine("version found: " + reader.Value);
                                    newVersion = new Version(reader.Value);
                                    break;
                                case "description":
                                    Console.WriteLine("description: " + reader.Value);
                                    desc = reader.Value.ToString();
                                    break;
                                case "url":
                                    Console.WriteLine("url found: " + reader.Value);
                                    url = reader.Value;
                                    break;
                                default:
                                    Console.WriteLine("Unrecognized Element: " + elementName);
                                    break;
                            }
                        }
                    }
                }
                reader.Close();
            }
            catch
            {
            }

            // get the running version  
            Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            // compare the versions  
            if (curVersion.CompareTo(newVersion) < 0)
            {
                string title = "Update is available!";
                string question = "An update for UGX AliasEditor++ is available!\n\nv" + newVersion + " Changelog: " + desc + "\n\nView the new version now?";
                if (DialogResult.Yes == MessageBox.Show(question, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    System.Diagnostics.Process.Start(url);
                }
            }
        }
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bw.RunWorkerAsync();
        }
        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (anyUnsavedChanges)
            {
                const string message = "You have unsaved changes! Are you sure you want to quit?";
                const string caption = "You have unsaved changes!";
                var result = MessageBox.Show(message, caption,
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                e.Cancel = (result == DialogResult.No);
            }
        }
    }
    public static class TextBoxWatermarkExtensionMethod
    {
        private const uint ECM_FIRST = 0x1500;
        private const uint EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public static void SetWatermark(this TextBox textBox, string watermarkText)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermarkText);
        }

    }
}
