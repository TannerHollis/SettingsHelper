using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsHelper
{
    public class SettingChange
    {
        private string _genericWordBit;
        private string _wordBit;
        private string _setting;
        public SettingChange(string genericWordBit, string wordBit, string setting) 
        {
            _genericWordBit = genericWordBit;
            _wordBit = wordBit;
            _setting = setting;
        }

        public string WordBit
        {
            get { return _wordBit; }
        }

        public string Setting
        {
            get { return _setting; }
        }
    }
}
