﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SevenZip.Compression.LZMA;

namespace SettingsHelper
{
    public class Relay
    {
        private string _fileName;
        public List<SettingsGroup> _groups;
        private string[] _groupNames;

        private string[] _relays;

        private string _7ZExtractArguments = "x \"{0}\" -otemp -r -y";
        private string _7ZCompressArguments = "a \"{0}\" .\\temp\\* -y";

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

            string command = String.Format(_7ZExtractArguments, fileName);
            ExecuteCommand(command);

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

            string command = String.Format(_7ZCompressArguments, _fileName);

            ExecuteCommand(command);
            //Directory.Delete("temp", true);
        }

        private void ExecuteCommand(string arguments)
        {
            ProcessStartInfo processInfo;

            processInfo = new ProcessStartInfo();
            processInfo.FileName = "7z.exe ";
            processInfo.Arguments = arguments;
            processInfo.UseShellExecute = false;
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process process = Process.Start(processInfo);
            process.WaitForExit();
            Console.WriteLine("Process ended with: " + process.ExitCode);
        }
    }
}
