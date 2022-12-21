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

        SettingsTranslator(string relayType)
        {
            _relayType = relayType;
        }
        
        public string LookupWordBit(string genericWordBit)
        {
            return string.Empty;
        }

        private JsonNode Load(string relayType)
        {
            relayType = relayType.ToLower().Replace(' ', '_');

            FileStream file = File.OpenRead(relayType + ".json");

            return JsonObject.Parse(file);
        }
    }
}
