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

            // closing with OK
            // check the inputs

            // assume fail
            e.Cancel = true;
            if (!IsNameOK(null, false))
                return;

            _result = new ConfigItem(txtName.Text);
            e.Cancel = false;
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
                txtName.Text = string.Empty;
                return;
            }
            else
            {
                txtName.Text = item.Name;
            }
        }

     

       
    }
}
