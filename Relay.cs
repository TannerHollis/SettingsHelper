using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SevenZip.Compression.LZMA;

using OpenMcdf;
using System.Security.Cryptography;

namespace SettingsHelper
{
    public class Relay
    {
        private string _fileName;
        private CompoundFile _compoundFile;
        private CFFolder _cfFolder;
        public List<SettingsGroup> _groups;
        private string[] _groupNames;

        private string[] _relays;

        public Relay(string fileName)
        {
            _fileName = fileName;
            _groups = new List<SettingsGroup>();
            ExtractRelayFiles(fileName);
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
            return _groupNames;
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

        public void LoadRelay(int index)
        {
            _groups= new List<SettingsGroup>();
            List<string> groupNames = new List<string>();

            string[] textFiles = Directory.GetFiles(_relays[index], "*.txt");
            foreach(string textFile in textFiles)
            {
                _groups.Add(new SettingsGroup(textFile));
                groupNames.Add(textFile);
            }

            _groupNames = groupNames.ToArray();
        }

        private void ExtractRelayFiles(string fileName)
        {
            if(!Directory.Exists("temp")) 
            {
                Directory.CreateDirectory("temp");
            }

            CompoundFile cf = new CompoundFile(fileName);
            CFFolder root = new CFFolder(null, cf.RootStorage, cf);
            root.ExtractTo("temp");
            _compoundFile = cf;
            _cfFolder = root;
            _relays = Directory.GetDirectories("temp\\Relays");
        }

        private void WriteAllGroups()
        {
            foreach(SettingsGroup group in _groups)
            {
                group.WriteSettingsToFile();
            }
        }

        public void CompressRelayFiles()
        {
            WriteAllGroups();

            foreach(SettingsGroup group in _groups)
            {
                string[] lines = group.GetLines();
                string linesJoined = string.Join("\r\n", lines);
                byte[] bytes = Encoding.Default.GetBytes(linesJoined);
                CFStream stream = _cfFolder.FindStream(group.GetFileName(), true);
                stream.SetData(bytes);
            }

            string pathName = Path.Combine(Directory.GetCurrentDirectory(), "temp");
            Directory.Delete(pathName, true);
        }

        public void Save(string fileName)
        {
            _compoundFile.Save(fileName);
        }
    }
}
