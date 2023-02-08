using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SettingsHelper
{
    public class RelayFiles
    {
        public string KnownWordBitsFileName { get; set; }
        public List<RelayFileAssociation> RelayFileAssociations { get; set; }
        public LoggerFileSettings LoggerFileSettings { get; set; }
        public string SettingsLookupFileChecksum { get; set; }

        private RelayFiles() 
        {
            RelayFileAssociations = new List<RelayFileAssociation>();
            LoggerFileSettings = new LoggerFileSettings();
        }

        public RelayFiles(string KnownWordBitsFileName, List<RelayFileAssociation> RelayFileAssociations, LoggerFileSettings LoggerFileSettings, string SettingsLookupFileChecksum)
        {
            this.KnownWordBitsFileName = KnownWordBitsFileName;
            this.RelayFileAssociations = RelayFileAssociations;
            this.LoggerFileSettings = LoggerFileSettings;
            this.SettingsLookupFileChecksum = SettingsLookupFileChecksum;
        }

        private static RelayFiles FromFile()
        {
            string jsonString = File.ReadAllText(FileManager.RelayTypeLookupFile);

            return JsonSerializer.Deserialize<RelayFiles>(jsonString);
        }

        public static void Init()
        {
            Default.ToFile();
        }

        private void ToFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(this, options);

            File.WriteAllText(FileManager.RelayTypeLookupFile, jsonString);
        }

        public static void AddRelayAssociation(RelayFileAssociation assoc) 
        {
            RelayFiles relayFiles = FromFile();
            relayFiles.RelayFileAssociations.Add(assoc);
            relayFiles.ToFile();
        }

        public static void ClearRelayAssociations()
        {
            RelayFiles relayFiles = FromFile();
            relayFiles.RelayFileAssociations.Clear();
            relayFiles.ToFile();
        }

        public static RelayFileAssociation GetRelayFileAssociation(string relayType)
        {
            RelayFiles relayFiles = FromFile();
            
            foreach(RelayFileAssociation fileAssociation in relayFiles.RelayFileAssociations)
            {
                if(fileAssociation.RelayType == relayType)
                {
                    return fileAssociation;
                }
            }

            return null;
        }

        public static LoggerFileSettings GetLoggerFileSettings()
        {
            RelayFiles relayFiles = FromFile();

            return relayFiles.LoggerFileSettings;
        }

        public static string GetKnownWordBitsFileName()
        {
            RelayFiles relayFiles = FromFile();

            return relayFiles.KnownWordBitsFileName;
        }

        public static string GetSettingsLookupFileChecksum()
        {
            RelayFiles relayFiles = FromFile();

            return relayFiles.SettingsLookupFileChecksum;
        }

        public static void SetSettingsLookupFileChecksum(string checksum)
        {
            RelayFiles relayFiles = FromFile();

            relayFiles.SettingsLookupFileChecksum = checksum;

            relayFiles.ToFile();
        }

        public static string[] GetSupportedRelayTranslations()
        {
            RelayFiles relayFiles = FromFile();

            List<string> relayTypes = new List<string>();

            foreach(RelayFileAssociation assoc in relayFiles.RelayFileAssociations)
            {
                relayTypes.Add(assoc.RelayType);
            }

            return relayTypes.ToArray();
        }

        private static RelayFiles Default
        {
            get
            {
                RelayFiles relayFiles = new RelayFiles();

                relayFiles.LoggerFileSettings.LogFolder = "logs";
                relayFiles.LoggerFileSettings.LogFileName = "{0:s}.log";

                relayFiles.KnownWordBitsFileName = FileManager.KnownWordBitsFile;

                return relayFiles;
            }
        }
        
    }
}
