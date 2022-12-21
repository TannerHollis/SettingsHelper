using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int GetLineIndex()
        {
            return _lineIndex;
        }

        public void SetSetting(string setting)
        {
            _hasChanged = !setting.Equals(this._originalSetting);
            _setting = setting;
        }

        public string GetSetting()
        {
            return _setting;
        }

        public string GetWordbit()
        {
            return _wordBit;
        }

        public bool HasChanged()
        {
            return _hasChanged;
        }

        override public string ToString()
        {
            return _wordBit + ",\"" + _setting + "\"";
        }

        public bool WordBitEquals(string a)
        {
            return a.Equals(this._wordBit);
        }

        static public Setting FromLine(int lineIndex, String line)
        {
            String[] splitLine = line.Split(',');
            Setting setting = new Setting(lineIndex, splitLine[0], splitLine[1].Replace("\"", ""));
            return setting;
        }
    }
}
