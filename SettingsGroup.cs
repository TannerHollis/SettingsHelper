using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenMcdf;

namespace SettingsHelper
{
    public class SettingsGroup
    {
        private string _fileName;

        private string[] _lines;

        public List<Setting> _settings;
        
        public SettingsGroup(string fileName)
        {
            _fileName = fileName;
            LoadSettingsFromFile(_fileName);
        }

        public Setting this[string index]
        {
            get
            {
                return _settings[LookupWordBitIndex(index)];
            }
            set
            {
                _settings[LookupWordBitIndex(index)] = value;
            }
        }

        public string[] GetLines()
        {
            return _lines;
        }

        public string GetFileName()
        {
            return Path.GetFileName(_fileName);
        }

        public void WriteSettingsToFile()
        {
            List<Setting> changedSettings = GetChangedSettings();

            Console.WriteLine("Writing " + changedSettings.Count + " change(s) to file: " + _fileName);

            foreach (Setting setting in changedSettings)
            {
                Console.WriteLine(" - Writing setting: " + setting.GetWordbit());

                int lineIndex = setting.GetLineIndex();
                _lines[lineIndex] = setting.ToString();
            }

            File.WriteAllLines(_fileName, _lines);
        }

        private int LookupWordBitIndex(string wordBit)
        {
            int ret = -1;
            for (int i = 0; i < _settings.Count; i++)
            {
                if (_settings[i].WordBitEquals(wordBit))
                {
                    ret = i; break;
                }
            }
            return ret;
        }

        private List<Setting> GetChangedSettings()
        {
            List<Setting> changedSettings = new List<Setting>();

            foreach (Setting setting in _settings)
            {
                if (setting.HasChanged())
                {
                    changedSettings.Add(setting);
                }
            }

            return changedSettings;
        }

        private void LoadSettingsFromFile(string fileName)
        {
            List<Setting> settings = new List<Setting>();

            string[] lines = File.ReadAllLines(fileName);

            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    Setting setting = Setting.FromLine(i, lines[i]);
                    settings.Add(setting);
                }
                catch
                {
                    // Do nothing... 
                }
            }

            _settings = settings;
            _lines = lines;
        }
    }
}
