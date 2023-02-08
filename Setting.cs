using System;
using System.Text.RegularExpressions;

namespace SettingsHelper
{
    public class Setting
    {
        private int _lineIndex;
        private string _wordBit;
        private string _setting;
        private string _originalSetting;
        private bool _hasChanged;

        public Setting(int lineIndex, string wordBit, string setting)
        {
            _lineIndex= lineIndex;
            _wordBit = wordBit;
            _setting = setting;
            
            // Store originial setting to compare new setpoints to original settings,
            //  this significatly reduces amount of file write operations.
            _originalSetting = setting;
            _hasChanged = false;
        }

        public int LineIndex { get { return _lineIndex;} }

        public string WordBit { get { return _wordBit;} }

        public string Setpoint
        { 
            get 
            {
                string temp = _setting.Replace("\u001c", string.Empty);
                return Regex.Replace(temp, @"[^\u0000-\u007F]+", string.Empty);
            } 
            set
            {
                _hasChanged = !value.Equals(this._originalSetting);
                _setting = value;
            }
        }

        public void RevertSetting()
        {
            Setpoint = _originalSetting;
        }

        public bool HasChanged { get { return _hasChanged; } }

        override public string ToString()
        {
            return WordBit + ",\"" + Setpoint + "\"";
        }

        public bool WordBitEquals(string a)
        {
            return a.Equals(WordBit);
        }

        static public Setting FromLine(int lineIndex, String line)
        {
            String[] splitLine = line.Split(',');
            Setting setting = new Setting(lineIndex, splitLine[0], splitLine[1].Replace("\"", ""));
            return setting;
        }
    }
}
