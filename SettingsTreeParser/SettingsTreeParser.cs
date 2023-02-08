using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsHelper.SettingsTreeParser
{
    public class SettingsTreeParser
    {
        public SettingsTranslator Translator { get; set; }
        public List<SettingsGroup> Groups { get; set; }

        public SettingsTreeParser(SettingsTranslator translator, List<SettingsGroup> groups)
        {
            Translator = translator;
            Groups = groups;
        }

        public SettingsTreeNode ParseAtWordBit(string wordBit)
        {
            return new SettingsTreeNode(this, null, wordBit);
        }
    }
}
