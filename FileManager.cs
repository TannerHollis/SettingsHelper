using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SettingsHelper
{
    public class FileManager
    {
        public const string relayTypeLookupFile = "Lookups\\relayFiles.json";

        public static FileStream _logFile;
        public static int _logFileOffset;

        public static string[] GetGenericWordBits()
        {
            JsonNode jsonNode = LoadJsonFile(relayTypeLookupFile);

            string jsonWordBitsFileName = jsonNode["knownWordBitsFile"].GetValue<string>();

            jsonNode = LoadJsonFile("Lookups\\" + jsonWordBitsFileName);

            JsonNode jsonWordBits = jsonNode["genericWordBits"];

            List<string> words = new List<string>();

            foreach (string word in jsonWordBits.AsArray())
            {
                words.Add(word);
            }

            return words.ToArray();
        }

        public static string[] GetComplexWordBits()
        {
            JsonNode jsonNode = LoadJsonFile(relayTypeLookupFile);

            string jsonWordBitsFileName = jsonNode["knownWordBitsFile"].GetValue<string>();

            jsonNode = LoadJsonFile("Lookups\\" + jsonWordBitsFileName);

            JsonNode jsonWordBits = jsonNode["complexWordBits"];

            List<string> words = new List<string>();

            foreach (string word in jsonWordBits.AsArray())
            {
                words.Add(word);
            }

            return words.ToArray();
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
            JsonNode jsonNode = LoadJsonFile(relayTypeLookupFile);

            relayType = FormatRelayType(relayType);

            string rdbTemplateFileName = jsonNode[relayType]["translationLayer"].GetValue<string>();

            return "Lookups\\" + rdbTemplateFileName;
        }

        public static string GetRDBTemplateFileName(string relayType)
        {
            JsonNode jsonNode = LoadJsonFile(relayTypeLookupFile);

            relayType = FormatRelayType(relayType);

            string rdbTemplateFileName = jsonNode[relayType]["rdbTemplate"].GetValue<string>();

            return "Lookups\\" + rdbTemplateFileName;
        }

        public static string GetLogFolder()
        {
            JsonNode jsonNode = LoadJsonFile(relayTypeLookupFile);

            string logFolder = jsonNode["logger"]["folder"].GetValue<string>();

            return logFolder;
        }

        public static string GetLogFileName()
        {
            JsonNode jsonNode = LoadJsonFile(relayTypeLookupFile);

            string logFileName = jsonNode["logger"]["fileName"].GetValue<string>();

            logFileName = string.Format(logFileName, DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"));

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

        public static JsonNode LoadJsonFile(string fileName)
        {
            FileStream file = File.OpenRead(fileName);

            return JsonObject.Parse(file);
        }

        private static string FormatRelayType(string relayType)
        {
            return relayType.ToLower().Replace(' ', '_');
        }
    }
}
