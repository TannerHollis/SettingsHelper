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
            _jsonNode = LoadTranslationFile();
        }

        public string LookupWordBit(string genericWordBit)
        {
            JsonNode js = _jsonNode;
            JsonNode ret;

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

                case "V27P1P":
                    ret = js["group"]["voltage"]["27P1P"];
                    break;

                case "V27P1D":
                    ret = js["group"]["voltage"]["27P1D"];
                    break;

                case "V27P2P":
                    ret = js["group"]["voltage"]["27P2P"];
                    break;

                case "V27P2D":
                    ret = js["group"]["voltage"]["27P2D"];
                    break;

                case "V59P1P":
                    ret = js["group"]["voltage"]["59P1P"];
                    break;

                case "V59P1D":
                    ret = js["group"]["voltage"]["59P1D"];
                    break;

                case "V59P2P":
                    ret = js["group"]["voltage"]["59P2P"];
                    break;

                case "V59P2D":
                    ret = js["group"]["voltage"]["59P2D"];
                    break;

                case "F81U1P":
                    ret = js["group"]["frequency"]["81U1P"];
                    break;

                case "F81U1D":
                    ret = js["group"]["frequency"]["81U1D"];
                    break;

                case "F81U1DEX":
                    ret = js["group"]["frequency"]["81U1DEX"];
                    break;

                case "F81U2P":
                    ret = js["group"]["frequency"]["81U2P"];
                    break;

                case "F81U2D":
                    ret = js["group"]["frequency"]["81U2D"];
                    break;

                case "F81O1P":
                    ret = js["group"]["frequency"]["81O1P"];
                    break;

                case "F81O1D":
                    ret = js["group"]["frequency"]["81O1D"];
                    break;

                case "F81O1DEX":
                    ret = js["group"]["frequency"]["81O1DEX"];
                    break;

                case "F81O2P":
                    ret = js["group"]["frequency"]["81O2P"];
                    break;

                case "F81O2D":
                    ret = js["group"]["frequency"]["81O2D"];
                    break;

                // TODO: Complete the switch case tree for all origninal WordBits

                default:
                    ret = null;
                    break;
            }

            return (ret != null) ? ret.GetValue<string>() : "NOT_FOUND";
        }

        private string SetTimedElement(string value)
        {
            JsonNode js = _jsonNode;
            if (js["logic"]["timerCycles"].GetValue<bool>())
            {
                return value;
            }
            else
            {
                double time = Double.Parse(value) / 60.0;
                return time.ToString();
            }
        }

        public List<SettingChange> SetGenericSetting(string genericWordBit, string setting)
        {
            List<SettingChange> sc = new List<SettingChange>();

            // TODO: Create method to set setting with generic WordBit and setting

            return sc;
        }

        private JsonNode LoadTranslationFile()
        {
            string translationLayerFileName = FileManager.GetTranslationLayerFile(_relayType);

            return FileManager.LoadJsonFile(translationLayerFileName);
        }
    }
}
