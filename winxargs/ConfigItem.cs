using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace winxargs
{
    public class ConfigItem
    {
        string _name;
        bool _isNew = false;
        public ConfigItem(string name, bool isNew)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            _name = name;
            _isNew=isNew;
        }
        public ConfigItem(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            _name = name;
        }

        override public string ToString() 
        {
            return _name;
        }

        public bool IsAddingNewItem
        {
            get { return _isNew; }
        }
        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
