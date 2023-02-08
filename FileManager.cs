using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SettingsHelper
{
    public class FileManager
    {
        public static readonly string SettingsHelperFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SettingsHelper");
        public static readonly string LookupsFolder = Path.Combine(SettingsHelperFolder , "Lookups");
        public static readonly string RDBTemplatesFolder = Path.Combine(SettingsHelperFolder, "RDB Templates");
        public static readonly string ZIPReadyPackage = Path.Combine(Directory.GetCurrentDirectory(), "ship.zip");
        public static readonly string RelayTypeLookupFile = Path.Combine(SettingsHelperFolder, "relayFiles.json");
        public static readonly string KnownWordBitsFile = Path.Combine(SettingsHelperFolder, "knownWordBits.json");
        public static readonly string SettingsTranslationFile = Path.Combine(SettingsHelperFolder, "SettingsLookups.xlsx");

        public static FileStream _logFile;
        public static int _logFileOffset;

        public static void InitFolderStructure()
        {
            //Create nexessary directories
            Directory.CreateDirectory(SettingsHelperFolder);
            Directory.CreateDirectory(LookupsFolder);
            Directory.CreateDirectory(RDBTemplatesFolder);
            
            // Initialize Relay File Associations file
            if(!File.Exists(RelayTypeLookupFile))
            {
                RelayFiles.Init();
            }

            // Initialize Known Word Bits file
            if(!File.Exists(KnownWordBitsFile))
            {
                KnownWordBits.Init();
            }

            // Initialize Log folder
            Directory.CreateDirectory(GetLogFolder());

            // Unzip ship package, overwriting
            using (ZipArchive zipFile = ZipFile.OpenRead(ZIPReadyPackage))
            {
                foreach(ZipArchiveEntry entry in zipFile.Entries)
                {
                    string fileName = Path.Combine(SettingsHelperFolder, entry.FullName);
                    entry.ExtractToFile(fileName, true);
                }
            }

            // Re-run the relay translation spreadsheet is changes are detected
            string checksum = GetFileChecksum(SettingsTranslationFile);
            if(RelayFiles.GetSettingsLookupFileChecksum() != checksum)
            {
                SettingsTranslator.ExtractFromXLSX(SettingsTranslationFile);
                RelayFiles.SetSettingsLookupFileChecksum(checksum);
            }
        }

        public static string GetFileChecksum(string fileName)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream file = File.OpenRead(fileName))
                {
                    byte[] bytes = md5.ComputeHash(file);
                    return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static string[] GetGenericWordBits()
        {
            string fileName = RelayFiles.GetKnownWordBitsFileName();
            return KnownWordBits.FromFile(fileName).GenericWordBits.ToArray();
        }

        public static string[] GetComplexWordBits()
        {
            string fileName = RelayFiles.GetKnownWordBitsFileName();
            return KnownWordBits.FromFile(fileName).ComplexWordBits.ToArray();
        }

        public static string[] GetKnownWordBits()
        {
            string[] genericWordBits = GetGenericWordBits();
            string[] complexWordBits = GetComplexWordBits();

            List<string> wordBits = new List<string>();
            wordBits.AddRange(genericWordBits);
            wordBits.AddRange(complexWordBits);

            return wordBits.ToArray();
        }

        public static string GetTranslationLayerFile(string relayType)
        {
            return RelayFiles.GetRelayFileAssociation(relayType).TranslationLayerFile;
        }

        public static string GetRDBTemplateFileName(string relayType)
        {
            return RelayFiles.GetRelayFileAssociation(relayType).RDBTemplate;
        }

        public static string GetLogFolder()
        {
            return Path.Combine(SettingsHelperFolder, RelayFiles.GetLoggerFileSettings().LogFolder);
        }

        public static string GetLogFileName()
        {
            string logFileName = RelayFiles.GetLoggerFileSettings().LogFileName;

            logFileName = string.Format(logFileName, DateTime.Now.ToString("yyyy-dd-MM--HH-mm-ss"));

            return GetLogFolder() + "\\" + logFileName;
        }

        public enum LogLevel
        {
            Debug = 0,
            Info = 1,
            Warning = 2,
            Error = 3,
            Fatal = 4,
        }

        public static void Log(string text, LogLevel level)
        {
            if(_logFile == null)
            {
                OpenLogFile();
            }

            string logLine = level.ToString().ToUpper() + 
                " - " + DateTime.Now.ToString("[yyyy-dd-MM, HH:mm.ss] - ") +
                text + "\n";

            byte[] bytes = Encoding.Default.GetBytes(logLine);

            _logFile.Write(bytes, 0, bytes.Length);
            _logFileOffset += bytes.Length - 1;
        }

        private static void OpenLogFile()
        {
            _logFileOffset = 0;

            string folderName = GetLogFolder();

            if(!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            _logFile = File.OpenWrite(GetLogFileName());
        }
    }
}
