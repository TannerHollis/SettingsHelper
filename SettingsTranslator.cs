using Microsoft.Office.Interop.Word;
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
        private RelayLookup _relayLookup;

        public SettingsTranslator(string relayType)
        {
            _relayType = relayType;
            _relayLookup = RelayLookup.FromFile(FileManager.GetTranslationLayerFile(_relayType));
        }

        public string LookupWordBit(string genericWordBit)
        {
            string ret = string.Empty;

            RelayLookup rl = _relayLookup;

            switch(genericWordBit)
            {
                case "FREQ":
                    ret = rl.global.FREQ;
                    break;

                case "ROT":
                    ret = rl.global.ROT;
                    break;

                case "PTR":
                    ret = rl.global.PTR;
                    break;

                case "CTR":
                    ret = rl.global.CTR;
                    break;

                case "VNOM":
                    ret = rl.global.VNOM.wordBit;
                    break;

                case "V27P1P":
                    ret = rl.group.voltage.V27P1.pickup;
                    break;

                case "V27P1D":
                    ret = rl.group.voltage.V27P1.delay;
                    break;

                case "V27P2P":
                    ret = rl.group.voltage.V27P2.pickup;
                    break;

                case "V27P2D":
                    ret = rl.group.voltage.V27P2.delay;
                    break;

                case "V59P1P":
                    ret = rl.group.voltage.V59P1.pickup;
                    break;

                case "V59P1D":
                    ret = rl.group.voltage.V59P1.delay;
                    break;

                case "V59P2P":
                    ret = rl.group.voltage.V59P2.pickup;
                    break;

                case "V59P2D":
                    ret = rl.group.voltage.V59P2.delay;
                    break;

                case "F81U1P":
                    ret = rl.group.frequency.F81U1.pickup;
                    break;

                case "F81U1D":
                    ret = rl.group.frequency.F81U1.delay;
                    break;

                case "F81U1DEX":
                    ret = rl.group.frequency.F81U1.delay2;
                    break;

                case "F81U2P":
                    ret = rl.group.frequency.F81U2.pickup;
                    break;

                case "F81U2D":
                    ret = rl.group.frequency.F81U2.delay;
                    break;

                case "F81O1P":
                    ret = rl.group.frequency.F81O1.pickup;
                    break;

                case "F81O1D":
                    ret = rl.group.frequency.F81O1.delay;
                    break;

                case "F81O1DEX":
                    ret = rl.group.frequency.F81O1.delay2;
                    break;

                case "F81O2P":
                    ret = rl.group.frequency.F81O2.pickup;
                    break;

                case "F81O2D":
                    ret = rl.group.frequency.F81O2.delay;
                    break;

                // TODO: Complete the switch case tree for all origninal WordBits

                default:
                    ret = string.Empty;
                    break;
            }

            return ret;
        }

        private string SetTimedElement(string value)
        {
            RelayLookup rl = _relayLookup;
            if (rl.logic.timers.isCycles)
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

            string[] genericWordBits = FileManager.GetGenericWordBits();

            if(genericWordBits.Contains(genericWordBit))
            {
                sc.Add(GenericSettingChange(genericWordBit, setting));
                return sc;
            }

            SettingChange sc1, sc2;
            string delay1, delay2;

            switch(genericWordBit)
            {
                case "V27P1D":
                    delay1 = SetTimedElement(setting);
                    sc1 = new SettingChange(genericWordBit, LookupWordBit(genericWordBit), delay1);
                    sc.Add(sc1);
                    break;

                case "V27P2D":
                    delay1 = SetTimedElement(setting);
                    sc1 = new SettingChange(genericWordBit, LookupWordBit(genericWordBit), delay1);
                    sc.Add(sc1);
                    break;

                case "V59P1P":
                    delay1 = SetTimedElement(setting);
                    sc1 = new SettingChange(genericWordBit, LookupWordBit(genericWordBit), delay1);
                    sc.Add(sc1);
                    break;


                default:
                    break;
            }

            // TODO: Add trip logic where trip logic is added incrementally. Adding elements to the trip equation.

            return sc;
        }

        private SettingChange GenericSettingChange(string genericWordBit, string setting)
        {
            return new SettingChange(genericWordBit, LookupWordBit(genericWordBit), setting);
        }
    }
}
