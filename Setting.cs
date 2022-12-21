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
        private string _wordBit;
        private string _setting;
        private bool _hasChanged;

        Setting(string wordBit, string setting)
        {
            _wordBit = wordBit;
            _setting = setting;
            _hasChanged = false;
        }

        public void SetSetting(string setting)
        {
            if(!setting.Equals(this._setting))
            {
                _hasChanged = true;
            }
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

        public string ToString()
        {
            return _wordBit + ",\"" + _setting + "\"";
        }

        public bool WordBitEquals(Setting a)
        {
            return a.GetWordbit().Equals(this._wordBit);
        }

        static public Setting FromLine(String line)
        {
            String[] splitLine = line.Split(',');
            Setting setting = new Setting(splitLine[0], splitLine[1].Replace("\"", ""));
            return setting;
        }
    }
}
