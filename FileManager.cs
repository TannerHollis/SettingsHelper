using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SettingsHelper
{
    public class FileManager
    {
        public const string relayTypeLookupFile = "Lookups\\relayFiles.json";

        public static string[] GetOriginalWordBits()
        {
            JsonNode jsonNode = LoadJsonFile(relayTypeLookupFile);

            string jsonWordBitsFileName = jsonNode["genericWordBitsFile"].GetValue<string>();

            jsonNode = LoadJsonFile("Lookups\\" + jsonWordBitsFileName);

            JsonNode jsonWordBits = jsonNode["genericWordBits"];

            List<string> words = new List<string>();

            foreach (string word in jsonWordBits.AsArray())
            {
                words.Add(word);
            }

            return words.ToArray();
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
