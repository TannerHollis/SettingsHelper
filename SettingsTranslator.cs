using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SettingsHelper
{
    public class SettingsTranslator
    {
        private string _relayType;
        private JsonNode _jsonNode;

        SettingsTranslator(string relayType)
        {
            _relayType = relayType;
        }
        
        public string LookupWordBit(string genericWordBit)
        {
            // TODO: Add lookup wordbit function
            return string.Empty;
        }

        private JsonNode LoadJsonFile(string relayType)
        {
            // format relayType to lowercase and replace spaces with underscores
            relayType = relayType.ToLower().Replace(' ', '_');

            // Append .json file type and open to filestream
            FileStream file = File.OpenRead(relayType + ".json");

            // Parse filestream and return .json object node
            return JsonObject.Parse(file);
        }
    }
}
