using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public SettingsTranslator(string relayType)
        {
            _relayType = relayType;
            LoadJsonFile(relayType);
        }
        
        public string LookupWordBit(string genericWordBit)
        {
            JsonNode js = _jsonNode;
            JsonNode ret;

            // TODO: Add lookup wordbit function
            switch(genericWordBit)
            {
                case "FREQ":
                    ret = js["global"]["FREQ"];
                    break;

                case "ROT":
                    ret = js["global"]["ROT"];
                    break;

                case "PTR":
                    ret = js["global"]["PTR"];
                    break;

                case "CTR":
                    ret = js["global"]["CTR"];
                    break;

                case "VNOM":
                    ret = js["global"]["VNOM"]["wordBit"];
                    break;

                case "27P1P":
                    ret = js["group"]["voltage"]["27P1P"];
                    break;

                case "27P1D":
                    ret = js["group"]["voltage"]["27P1D"];
                    break;

                case "27P2P":
                    ret = js["group"]["voltage"]["27P2P"];
                    break;

                case "27P2D":
                    ret = js["group"]["voltage"]["27P2D"];
                    break;

                default:
                    ret = js["relayType"];
                    break;
            }

            return ret.GetValue<String>();
        }

        private void LoadJsonFile(string relayType)
        {
            // format relayType to lowercase and replace spaces with underscores
            string relayTypeLookupFileName = "Lookups\\" + relayType.ToLower().Replace(' ', '_') + ".json";

            // Append .json file type and open to filestream
            FileStream file = File.OpenRead(relayTypeLookupFileName);

            // Parse filestream and return .json object node
            _jsonNode = JsonObject.Parse(file);
        }
    }
}
