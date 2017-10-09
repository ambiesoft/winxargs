using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ambiesoft;

namespace winxargs
{
    public partial class ConfigDialog : Form
    {
        List<ConfigItem> _all;
        public ConfigDialog(List<ConfigItem> all)
        {
            InitializeComponent();

            _all = all;
            foreach (ConfigItem item in _all)
            {
                cmbConfig.Items.Add(item);
            }
            cmbConfig.SelectedIndex = 0;
        }

        bool IsNameOK(ConfigItem targetitem, bool empok)
        {
            if (!empok)
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    AmbLib.Alert(Properties.Resources.S_NAME_ISEMPTY);
                    txtName.Focus();
                    return false;
                }
            }

            foreach (ConfigItem item in _all)
            {
                if (targetitem == null)
                    continue;
                if (targetitem == item)
                    continue;
                if (item.IsAddingNewItem)
                    continue;

                if (string.Compare(txtName.Text, item.ToString()) == 0)
                {
                    AmbLib.Alert(string.Format(Properties.Resources.S_NAME_ALREADYEXISTS, txtName.Text));
                    txtName.Focus();
                    return false;
                }
            }
            return true;
        }

        private void ConfigDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            e.Cancel = true;
            // closing with OK
            // check the inputs
            ConfigItem item = (ConfigItem)cmbConfig.SelectedItem;
            if (item == null)
            {
                e.Cancel = false;
            }
            else if (item.IsAddingNewItem)
            {
                // assume fail

                if (!IsNameOK(null, false))
                    return;

                _result = new ConfigItem(
                    txtName.Text,
                    txtFilename.Text,
                    txtArgumentsBefore.Text,
                    txtPrefix.Text,
                    txtSuffix.Text,
                    txtArgumentsAfter.Text,
                    false);
                e.Cancel = false;
            }
            else
            {
                // existing item
                if (!IsNameOK(item, true))
                    return;
                if (!string.IsNullOrEmpty(txtName.Text))
                    item.Name = txtName.Text;

                item.FileName = txtFilename.Text;
                item.ArgumentsBefore = txtArgumentsBefore.Text;
                item.Prefix = txtPrefix.Text;
                item.Suffix = txtSuffix.Text;
                item.ArgumentsAfter = txtArgumentsAfter.Text;
                e.Cancel = false;
            }
        }

        ConfigItem _result;
        public ConfigItem Result
        {
            get { return _result; }
        }

        ConfigItem _lastSelection;
        private void cmbConfig_Enter(object sender, EventArgs e)
        {
            _lastSelection = (ConfigItem)cmbConfig.SelectedItem;
        }  
        private void cmbConfig_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_lastSelection == null)
                return;
            
            ConfigItem item = _lastSelection;

            bool failed = true;
            try
            {
                // first save current 
                if (!item.IsAddingNewItem)
                {
                    if (!IsNameOK(item, true))
                        return;
                    if (!string.IsNullOrEmpty(txtName.Text))
                        item.Name = txtName.Text;

                    item.FileName = txtFilename.Text;
                    item.ArgumentsBefore = txtArgumentsBefore.Text;
                    item.Prefix = txtPrefix.Text;
                    item.Suffix = txtSuffix.Text;
                    item.ArgumentsAfter = txtArgumentsAfter.Text;
                }
                failed = false;
            }
            finally
            {
                if (failed)
                {
                    cmbConfig.SelectedItem = _lastSelection;
                }
            }
        }
        private void cmbConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigItem item = (ConfigItem)cmbConfig.SelectedItem;
            if (item == null)
                return;
            _lastSelection = item;
            if (item.IsAddingNewItem)
            {
                txtName.Text =
                    txtFilename.Text =
                    txtArgumentsBefore.Text =
                    txtPrefix.Text =
                    txtSuffix.Text =
                    txtArgumentsAfter.Text=
                    string.Empty;
                return;
            }
            else
            {
                txtName.Text = item.Name;
                txtFilename.Text = item.FileName;
                txtArgumentsBefore.Text = item.ArgumentsBefore;
                txtPrefix.Text = item.Prefix;
                txtSuffix.Text = item.Suffix;
                txtArgumentsAfter.Text = item.ArgumentsAfter;
            }
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (DialogResult.OK != ofd.ShowDialog())
                    return;

                txtFilename.Text = ofd.FileName;
            }

        }

     

       
    }
}
