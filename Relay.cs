using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenMcdf;

namespace SettingsHelper
{
    public class Relay
    {
        private string _fileName;
        private int _selectedIndex;
        private string _relayName;
        private CompoundFile _compoundFile;
        private CFFolder _cfFolder;
        public List<SettingsGroup> _groups;

        private string[] _relays;

        public Relay(string fileName)
        {
            _fileName = fileName;
            _groups = new List<SettingsGroup>();
            ExtractRelayFiles(fileName);
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
        }

        public string[] GetRelayNames()
        {
            List<string> relayNames= new List<string>();
            foreach(string relay in _relays)
            {
                relayNames.Add(Path.GetFileName(relay));
            }
            return relayNames.ToArray();
        }

        public string[] GetGroupNames()
        {
            List<string> groupNames = new List<string>();
            foreach (SettingsGroup group in _groups)
            {
                groupNames.Add(group.GetFileName());
            }
            return groupNames.ToArray();
        }

        public SettingsGroup this[string index]
        {
            get
            {
                return _groups[LookupGroupIndex(index)];
            }
            set
            {
                _groups[LookupGroupIndex(index)] = value;
            }
        }

        public int LookupGroupIndex(string name)
        {
            int ret = -1;
            for(int i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].GetFileName().Equals(name))
                {
                    ret = i; break;
                }
            }
            return ret;
        }

        public void Load(int index)
        {
            _selectedIndex = index;
            _relayName = GetRelayNames()[index];

            _groups= new List<SettingsGroup>();

            string[] textFiles = Directory.GetFiles(_relays[index], "*.txt");
            foreach(string textFile in textFiles)
            {
                _groups.Add(new SettingsGroup(textFile));
            }
        }

        private void ExtractRelayFiles(string fileName)
        {
            if(!Directory.Exists("temp")) 
            {
                Directory.CreateDirectory("temp");
            }
            else
            {
                Directory.Delete("temp", true);
            }

            CompoundFile cf = new CompoundFile(fileName);
            CFFolder root = new CFFolder(null, cf.RootStorage, cf);
            root.ExtractTo("temp");
            _compoundFile = cf;
            _cfFolder = root;
            _relays = Directory.GetDirectories("temp\\Relays");
        }

        public void Rename(string name)
        {
            _cfFolder.FindFolder(_relayName, true).Parent.RenameItem(_relayName, name);
            _cfFolder.FindStream("Device.txt", true).SetData(Encoding.Default.GetBytes(name));
            _relayName = name;
        }

        private void ApplySettingsChanges()
        {
            foreach(SettingsGroup group in _groups)
            {
                group.ApplySettingChanges();
            }
        }

        public void MergeSettingsChanges(List<SettingChange> settingChanges)
        {
            foreach(SettingChange change in settingChanges)
            {
                SetSetting(change.WordBit, change.Setting);
            }
        }

        private void SetSetting(string wordBit, string setting)
        {
            foreach(SettingsGroup group in _groups)
            {
                try
                {
                    group[wordBit].Setpoint = setting;
                }
                catch
                {
                    Console.WriteLine("ERROR: Could not find wordBit: " + wordBit);
                }
            }
        }

        public void CompressRelayFiles()
        {
            ApplySettingsChanges();

            foreach(SettingsGroup group in _groups)
            {
                string[] lines = group.GetLines();
                string linesJoined = string.Join("\r\n", lines);
                byte[] bytes = Encoding.Default.GetBytes(linesJoined);
                CFStream stream = _cfFolder.FindStream(group.GetFileName(), true);
                stream.SetData(bytes);
            }
        }

        public void Save(string fileName)
        {
            _compoundFile.Save(fileName);
        }

        public void Close()
        {
            string pathName = Path.Combine(Directory.GetCurrentDirectory(), "temp");
            Directory.Delete(pathName, true);
        }
    }
}
