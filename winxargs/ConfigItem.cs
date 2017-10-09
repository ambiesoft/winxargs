using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace winxargs
{
    public class ConfigItem
    {
        string _name;
        string _filename;
        string _argumentsBefore;
        string _prefix;
        string _suffix;
        string _argumentsAfter;

        bool _isNew = false;
        public ConfigItem(
            string name, 
            string filename,
            string argumentsBefore,
            string prefix,
            string suffix,
            string argumentsAfter,
            bool isNew)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            _name = name;
            _filename = filename;
            _argumentsBefore = argumentsBefore;
            _prefix = prefix;
            _suffix = suffix;
            _isNew=isNew;
            _argumentsAfter = argumentsAfter;
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
        public string FileName
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
            }
        }
        public string ArgumentsBefore
        {
            get
            {
                return _argumentsBefore;
            }
            set
            {
                _argumentsBefore = value; 
            }
        }
        public string Prefix
        {
            get
            {
                return _prefix;
            }
            set
            {
                _prefix = value;
            }
        }
        public string Suffix
        {
            get
            {
                return _suffix;
            }
            set
            {
                _suffix = value;
            }
        }
        public string ArgumentsAfter
        {
            get
            {
                return _argumentsAfter;
            }
            set
            {
                _argumentsAfter = value;
            }
        }

    }
}
