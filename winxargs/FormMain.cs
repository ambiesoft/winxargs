﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

using Ambiesoft;

namespace winxargs
{
    public partial class FormMain : Form
    {
        const string SECTION_OPTION = "Option";
        const string KEY_COLUMN_WIDTH = "ColumnWidth";

        const string KEY_CONFIGITEM_COUNT = "ConfigItemCount";
        const string KEY_CONFIGITEM_NAME = "ConfigItemName";
        const string KEY_CONFIGITEM_FILENAME = "ConfigItemFileName";
        const string KEY_CONFIGITEM_ARGUMENTSBEFORE = "ConfigItemArgumentsBefore";
        const string KEY_CONFIGITEM_PREFIX = "ConfigItemPrefix";
        const string KEY_CONFIGITEM_SUFFIX = "ConfigItemSuffix";
        const string KEY_CONFIGITEM_ARGUMENTSAFTER = "ConfigItemArgumentsAfter";


        const string KEY_SELECTED_CONFIGITEM = "SelectedConfigItem";
        
        const string COLUMN_FILE = "File";
        const string COLUMN_DATE = "Date";
        const string COLUMN_EXTENTION = "Extention";
        const string COLUMN_LENGTH = "Size";

        string IniPath
        {
            get
            {
                string inipath = Application.ExecutablePath;
                inipath = Path.GetDirectoryName(inipath);
                return Path.Combine(inipath, Application.ProductName + ".ini");
            }
        }
        public FormMain()
        {
            InitializeComponent();

            ColumnHeader ch = null;

            ch = new ColumnHeader();
            ch.Name = COLUMN_FILE;
            ch.Text = Properties.Resources.COLUMN_FILE;
            ch.Width = 100;
            lvMain.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Name = COLUMN_DATE;
            ch.Text = Properties.Resources.COLUMN_DATE;
            ch.Width = 100;
            lvMain.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Name = COLUMN_EXTENTION;
            ch.Text = Properties.Resources.COLUMN_EXTENTION;
            ch.Width = 100;
            lvMain.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Name = COLUMN_LENGTH;
            ch.Text = Properties.Resources.COLUMN_LENGTH;
            ch.Width = 100;
            lvMain.Columns.Add(ch);

            HashIni ini = Profile.ReadAll(IniPath);
            AmbLib.LoadListViewColumnWidth(lvMain, SECTION_OPTION, KEY_COLUMN_WIDTH, ini);

            int itemcount = 0;
            Profile.GetInt(SECTION_OPTION, KEY_CONFIGITEM_COUNT, 0, out itemcount, ini);
            for (int i = 0; i < itemcount; ++i)
            {
                string key = KEY_CONFIGITEM_NAME;
                key += i.ToString();
                string name;
                Profile.GetString(SECTION_OPTION, key, string.Empty, out name, ini);

                string filename;
                key = KEY_CONFIGITEM_FILENAME;
                key += i.ToString();
                Profile.GetString(SECTION_OPTION, key, string.Empty, out filename, ini);

                string argumentsbefore;
                key = KEY_CONFIGITEM_ARGUMENTSBEFORE;
                key += i.ToString();
                Profile.GetString(SECTION_OPTION, key, string.Empty, out argumentsbefore, ini);

                string prefix;
                key = KEY_CONFIGITEM_PREFIX;
                key += i.ToString();
                Profile.GetString(SECTION_OPTION, key, string.Empty, out prefix, ini);

                string suffix;
                key = KEY_CONFIGITEM_SUFFIX;
                key += i.ToString();
                Profile.GetString(SECTION_OPTION, key, string.Empty, out suffix, ini);

                string argumentsafter;
                key = KEY_CONFIGITEM_ARGUMENTSAFTER;
                key += i.ToString();
                Profile.GetString(SECTION_OPTION, key, string.Empty, out argumentsafter, ini);

                cmbConfig.Items.Add(new ConfigItem(name, filename, argumentsbefore, prefix, suffix, argumentsafter, false));
            }
            cmbConfig.Items.Add(new ConfigItem(Properties.Resources.S_EDIT_ITEM,
                string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,true));

            string selName = null;
            Profile.GetString(SECTION_OPTION, KEY_SELECTED_CONFIGITEM, null, out selName, ini);
            if (!string.IsNullOrEmpty(selName))
            {
                cmbConfig.SelectedIndex = cmbConfig.FindString(selName);
            }
            
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool failed = false;

            HashIni ini = Profile.ReadAll(IniPath);

            AmbLib.SaveListViewColumnWidth(lvMain, SECTION_OPTION, KEY_COLUMN_WIDTH, ini);

            ConfigItem editItem = null;
            foreach (ConfigItem item in cmbConfig.Items)
            {
                if (item.IsAddingNewItem)
                    editItem = item;
            }
            Debug.Assert(editItem != null);
            cmbConfig.Items.Remove(editItem);

            for (int i = 0; i < cmbConfig.Items.Count; ++i)
            {
                ConfigItem item = (ConfigItem)cmbConfig.Items[i];
                string key = KEY_CONFIGITEM_NAME;
                key += i.ToString();
                Profile.WriteString(SECTION_OPTION, key, item.Name, ini);

                key = KEY_CONFIGITEM_FILENAME;
                key += i.ToString();
                Profile.WriteString(SECTION_OPTION, key, item.FileName, ini);

                key = KEY_CONFIGITEM_ARGUMENTSBEFORE;
                key += i.ToString();
                Profile.WriteString(SECTION_OPTION, key, item.ArgumentsBefore, ini);

                key = KEY_CONFIGITEM_PREFIX;
                key += i.ToString();
                Profile.WriteString(SECTION_OPTION, key, item.Prefix, ini);

                key = KEY_CONFIGITEM_SUFFIX;
                key += i.ToString();
                Profile.WriteString(SECTION_OPTION, key, item.Suffix, ini);

                key = KEY_CONFIGITEM_ARGUMENTSAFTER;
                key += i.ToString();
                Profile.WriteString(SECTION_OPTION, key, item.ArgumentsAfter, ini);

            }
            Profile.WriteInt(SECTION_OPTION, KEY_CONFIGITEM_COUNT, cmbConfig.Items.Count, ini);

            ConfigItem selItem = (ConfigItem)cmbConfig.SelectedItem;
            if (selItem != null)
            {
                Profile.WriteString(SECTION_OPTION, KEY_SELECTED_CONFIGITEM, selItem.Name, ini);
            }

            failed |= !Profile.WriteAll(ini, IniPath);
            if (failed)
            {
                Ambiesoft.CenteredMessageBox.Show(this,
                    Properties.Resources.S_INISAVE_FAILED,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string getVideoLengthWork(string ins)
        {
            string ret;
            ret = getVL1(ins);
            if (!string.IsNullOrEmpty(ret))
                return ret;

            ret = getVL2(ins);
            if (!string.IsNullOrEmpty(ret))
                return ret;

            return string.Empty;
        }
        private string getVL1(string ins)
        {
            Regex reg = new Regex(@"Duration: (?<duration>.*?), start");

            string[] parts = ins.Split('\n');
            foreach (string part in parts)
            {
                string s = part.TrimEnd('\r');
                Match match = reg.Match(s);
                if (match.Success)
                {
                    return match.Groups["duration"].Value;
                    //return match.Value;
                }
            }
            return string.Empty;
        }
        private string getVL2(string ins)
        {
            bool inmeta = false;
            Dictionary<string, string> allattr = new Dictionary<string, string>();
            string[] parts = ins.Split('\n');
            foreach (string part in parts)
            {
                string s = part.TrimEnd('\r');
                if (s == "  Metadata:")
                {
                    inmeta = true;
                }

                if (inmeta)
                {
                    char[] c = { ':' };
                    string[] nv = s.Split(c, 2);
                    if (nv.Length == 2 && nv[0] != null && nv[1] != null)
                    {
                        try
                        {
                            allattr.Add(nv[0].Trim(), nv[1].Trim());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }

            try
            {
                string[] dparts = allattr["Duration"].Split(',');
                return dparts[0];
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private void lvMain_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, true);
                foreach (string s in fileName)
                {
                    FileInfo fi = new FileInfo(s);
                    ListViewItem item = new ListViewItem();
                    item.Text = s;
                    item.SubItems.Add(fi.LastAccessTime.ToString());
                    item.SubItems.Add(fi.Extension);
                    item.SubItems.Add(fi.Length.ToString());
                    item.Tag = fi;
                    lvMain.Items.Add(item);
                }
            }
        }

        private TimeSpan getSum()
        {
            try
            {
                TimeSpan tsall = new TimeSpan();
                foreach (ListViewItem item in lvMain.Items)
                {
                    string s = item.SubItems[3].Text;
                    string[] parts = s.Split(':');
                    int hour;
                    int.TryParse(parts[0], out hour);
                    int minutes;
                    int.TryParse(parts[1], out minutes);

                    string[] mili = parts[2].Split('.');
                    int sec;
                    int.TryParse(mili[0], out sec);
                    int milisec;
                    int.TryParse(mili[1] + "0", out milisec);

                    TimeSpan ts = new TimeSpan(0, hour, minutes, sec, milisec);
                    tsall += ts;
                }
                return tsall;
            }
            catch (Exception)
            {
                return new TimeSpan(0);
            }
        }

        private void lvMain_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }



        private void lvMain_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }


        void UpdateTitle()
        {
            if (!this.IsHandleCreated)
                return;
            if (this.IsDisposed)
                return;

            this.Text = string.Format("{0}",
                Application.ProductName);
        }


        static string trans(Match m)
        {
            string ret = string.Empty;
            string x = m.ToString();
            if (x == "${$}")
            {
                ret = "$";
            }
            else if (x == "${SaveFile}")
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    if (DialogResult.OK != sfd.ShowDialog())
                    {
                        _runCanceled = true;
                        return x;
                    }
                    ret = AmbLib.doubleQuoteIfSpace(sfd.FileName);
                }
            }
            return ret;
        }
        static bool _runCanceled;
        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (lvMain.Items.Count < 1)
            {
                AmbLib.Alert(this,Properties.Resources.S_NO_ITEMS);
                return;
            }

            ConfigItem item = (ConfigItem)cmbConfig.SelectedItem;
            if (item == null)
            {
                AmbLib.Alert(this,Properties.Resources.S_NO_CONFIG);
                return;
            }

            string filename = item.FileName;
            StringBuilder sbArguments = new StringBuilder();
            if (!string.IsNullOrEmpty(item.ArgumentsBefore))
            {
                _runCanceled = false;
                string transed = Regex.Replace(item.ArgumentsBefore, "\\${.*}",
                    new MatchEvaluator(trans));
                if (_runCanceled)
                    return;
                sbArguments.Append(transed);
                sbArguments.Append(" ");
            }

            // append listitems to sbArgument
            bool spaceAppended = false;
            foreach (ListViewItem lvi in lvMain.Items)
            {
                if (!string.IsNullOrEmpty(item.Prefix))
                    sbArguments.Append(item.Prefix);

                sbArguments.Append(AmbLib.doubleQuoteIfSpace(lvi.Text));

                if (!string.IsNullOrEmpty(item.Suffix))
                    sbArguments.Append(item.Suffix);

                sbArguments.Append(" ");
                spaceAppended = true;
            }
            if(!spaceAppended)
                sbArguments.Append(" ");

            if (!string.IsNullOrEmpty(item.ArgumentsAfter))
            {
                _runCanceled = false;
                string transed = Regex.Replace(item.ArgumentsAfter, "\\${.*}",
                    new MatchEvaluator(trans));
                if (_runCanceled)
                    return;
                sbArguments.Append(transed);
            }

            //string tempfile = Path.GetTempFileName();
            //using (TextWriter writer = File.CreateText(tempfile))
            //{
            //    foreach (ListViewItem item in lvMain.Items)
            //    {
            //        writer.Write(@"file '");
            //        writer.Write(item.Text);
            //        writer.Write(@"'");
            //        writer.WriteLine();
            //    }
            //}


            //string outfile=null;
            //using(SaveFileDialog sfd = new SaveFileDialog())
            //{
            //    sfd.FileName = outfilename;
            //    string filter = extwithout + "File ";
            //    filter += "(*.";
            //    filter += extwithout;
            //    filter += ")|*.";
            //    filter += extwithout;
            //    filter += "|All File(*.*)|*.*";
            //    sfd.Filter=filter;
            //    sfd.InitialDirectory = initDir;
            //    if(DialogResult.OK != sfd.ShowDialog(this))
            //        return;

            //    outfile = sfd.FileName;
            //}

            //string ffmpeg = getffmpeg();
            //string argument = "-safe 0 -f concat -i \"";

            //argument += tempfile;
            //argument += "\"";
            //argument += " -c copy \"";
            //argument += outfile;
            //argument += "\"";

            StringBuilder sbResult = new StringBuilder();

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = filename;
                psi.Arguments = sbArguments.ToString();
                psi.RedirectStandardOutput = false;
                psi.RedirectStandardError = false;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = false;

                if (DialogResult.Yes != CenteredMessageBox.Show(this,
                    string.Format(Properties.Resources.S_ARE_YOU_SURE_TO_LAUNCH,
                        psi.FileName,
                        psi.Arguments),
                    Application.ProductName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question))
                {
                    return;
                }

                using (ProcessingDialog pdlg = new ProcessingDialog(psi))
                {
                    pdlg.ShowDialog();
                }
                //Process p = Process.Start(psi);
                //p.WaitForExit();

                //AmbLib.Info(this,string.Format(Properties.Resources.S_PROGFINISHED_WITH_RETVAL, p.ExitCode));
            }
            catch (Exception ex)
            {
                AmbLib.Alert(this,ex);
            }
        }

        private bool _reverse = false;
        private void lvMain_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            _reverse = !_reverse;
            lvMain.ListViewItemSorter = new ListViewItemComparer(e.Column, _reverse);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lvMain.Items.Clear();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

       





        // The LVItem being dragged
        private ListViewItem _itemDnD = null;
        private bool _mouseDowning;
        private int _mouseDownStartTick;
        private void lvMain_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDowning = true;
            _mouseDownStartTick = Environment.TickCount;
            _itemDnD = lvMain.GetItemAt(e.X, e.Y);
        }

        private void lvMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDowning && (Environment.TickCount - _mouseDownStartTick) > 100)
            {

            }
            else
            {
                _itemDnD = null;
            }
            if (_itemDnD == null)
                return;

            // drag begines
            lvMain.ListViewItemSorter = null;

            // Show the user that a drag operation is happening
            Cursor = Cursors.Hand;

            // calculate the bottom of the last item in the LV so that you don't have to stop your drag at the last item
            int lastItemBottom = Math.Min(e.Y, lvMain.Items[lvMain.Items.Count - 1].GetBounds(ItemBoundsPortion.Entire).Bottom - 1);

            // use 0 instead of e.X so that you don't have to keep inside the columns while dragging
            ListViewItem itemOver = lvMain.GetItemAt(0, lastItemBottom);

            if (itemOver == null)
                return;

            Rectangle rc = itemOver.GetBounds(ItemBoundsPortion.Entire);
            if (e.Y < rc.Top + (rc.Height / 2))
            {
                lvMain.LineBefore = itemOver.Index;
                lvMain.LineAfter = -1;
            }
            else
            {
                lvMain.LineBefore = -1;
                lvMain.LineAfter = itemOver.Index;
            }

            // invalidate the LV so that the insertion line is shown
            lvMain.Invalidate();
        }

        private void lvMain_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDowning = false;
            if (_itemDnD == null)
                return;

            try
            {
                // calculate the bottom of the last item in the LV so that you don't have to stop your drag at the last item
                int lastItemBottom = Math.Min(e.Y, lvMain.Items[lvMain.Items.Count - 1].GetBounds(ItemBoundsPortion.Entire).Bottom - 1);

                // use 0 instead of e.X so that you don't have to keep inside the columns while dragging
                ListViewItem itemOver = lvMain.GetItemAt(0, lastItemBottom);

                if (itemOver == null)
                    return;

                Rectangle rc = itemOver.GetBounds(ItemBoundsPortion.Entire);

                // find out if we insert before or after the item the mouse is over
                bool insertBefore;
                if (e.Y < rc.Top + (rc.Height / 2))
                {
                    insertBefore = true;
                }
                else
                {
                    insertBefore = false;
                }

                if (_itemDnD != itemOver) // if we dropped the item on itself, nothing is to be done
                {
                    if (insertBefore)
                    {
                        lvMain.Items.Remove(_itemDnD);
                        lvMain.Items.Insert(itemOver.Index, _itemDnD);
                    }
                    else
                    {
                        lvMain.Items.Remove(_itemDnD);
                        lvMain.Items.Insert(itemOver.Index + 1, _itemDnD);
                    }
                }

                // clear the insertion line
                lvMain.LineAfter =
                lvMain.LineBefore = -1;

                lvMain.Invalidate();

            }
            finally
            {
                // finish drag&drop operation
                _itemDnD = null;
                Cursor = Cursors.Default;
            }
        }

        ConfigItem _lastSelectedItem;
        private void cmbConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigItem selitem = (ConfigItem)cmbConfig.SelectedItem;
            List<ConfigItem> all = new List<ConfigItem>();
            foreach (ConfigItem o in cmbConfig.Items)
                all.Add(o);

            try
            {
                if (selitem.IsAddingNewItem)
                {
                    using (ConfigDialog configDialog = new ConfigDialog(all))
                    {
                        if (DialogResult.OK != configDialog.ShowDialog())
                            return;

                        ConfigItem newItem = configDialog.Result;
                        if (newItem != null)
                        {
                            cmbConfig.Items.Add(newItem);
                            cmbConfig.SelectedItem = newItem;
                        }
                        else
                        {
                            if (_lastSelectedItem != null)
                            {
                                // still exists in cmbBox?
                                bool still = false;
                                foreach (ConfigItem item in cmbConfig.Items)
                                {
                                    if (item == _lastSelectedItem)
                                    {
                                        still = true;
                                        break;
                                    }
                                }
                                if (still)
                                    cmbConfig.SelectedItem = _lastSelectedItem;
                            }
                        }
                    }
                }
            }
            finally
            {
                _lastSelectedItem = selitem;
            }
        }
    }

    public class ListViewItemComparer : System.Collections.IComparer
    {
        private int _column;
        private bool _reverse;
        /// <summary>
        /// ListViewItemComparerクラスのコンストラクタ
        /// </summary>
        /// <param name="col">並び替える列番号</param>
        public ListViewItemComparer(int col, bool reverse)
        {
            _column = col;
            _reverse = reverse;
        }

        //xがyより小さいときはマイナスの数、大きいときはプラスの数、
        //同じときは0を返す
        public int Compare(object x, object y)
        {
            //ListViewItemの取得
            ListViewItem itemx = (ListViewItem)x;
            ListViewItem itemy = (ListViewItem)y;

            int ret = 0;
            if (_column == 0)
            {
                ret = string.Compare(itemx.SubItems[_column].Text,
                    itemy.SubItems[_column].Text);
            }
            else if (_column == 1)
            {
                FileInfo fix = (FileInfo)itemx.Tag;
                FileInfo fiy = (FileInfo)itemy.Tag;

                ret = fix.LastAccessTime.CompareTo(fiy.LastAccessTime);
            }
            else if(_column==2)
            {
                ret = string.Compare(itemx.SubItems[_column].Text,
                    itemy.SubItems[_column].Text,true);
            }
            else
            {
                Debug.Assert(false);
            }
            return _reverse ? -ret : ret;

        }
    }


}