using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SettingsHelper.SettingsTreeParser
{
    public class SettingsTreeNode
    {
        public Setting NodeSetting { get; set; }
        public SettingsTreeNode Parent { get; set; }
        public List<List<SettingsTreeNode>> Children { get; set; }
        public List<string> ChildrenGroupNames { get; set; }

        public SettingsTreeNode(SettingsTreeParser parser, SettingsTreeNode parent, string wordBit)
        {
            this.Parent = parent;
            this.NodeSetting = GetSetting(parser, wordBit);
            Children = new List<List<SettingsTreeNode>>();
            ChildrenGroupNames = new List<string>();
            Parse(parser);
        }

        public void ToFile(string fileName)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);
        }

        public override string ToString()
        {
            return NodeSetting.ToString();
        }

        public static Setting GetSetting(SettingsTreeParser parser, string wordBit)
        {
            foreach(SettingsGroup group in parser.Groups)
            {
                try
                {
                    Setting setting = group[wordBit];
                    return setting;
                }
                catch
                {
                    continue;
                }
            }
            return null;
        }

        public bool IsBitInParents(string wordBit)
        {
            // If parent exists, otherwise at root
            if (Parent != null)
            {
                if (Parent.ChildrenGroupNames.Contains(wordBit))
                    return true;
                else
                    return Parent.IsBitInParents(wordBit);
            }
            else
                return false;
        }

        public void Parse(SettingsTreeParser parser)
        {
            WordBitLookup relayWordBitLookup = parser.Translator.GetRelayWordBitLookup(NodeSetting.WordBit);

            switch(relayWordBitLookup.WordBitType)
            {
                case WordBitType.SELogic:
                    foreach(string wordBit in relayWordBitLookup.RelayWordBits)
                    {
                        Setting setting = GetSetting(parser, wordBit);
                        string[] bits = ParseSetting(setting.Setpoint);
                        foreach(string bit in bits)
                        {
                            // Ignore Empty strings from find and replace
                            if (bit == string.Empty)
                                continue;

                            // If bit is found in recursion, stop recursion for bit
                            if (IsBitInParents(bit))
                                continue;

                            // Add bit to children names
                            ChildrenGroupNames.Add(bit);

                            // Create new list of nodes
                            List<SettingsTreeNode> nodes = new List<SettingsTreeNode>();

                            // Get associated wordbits with bit
                            WordBitLookup[] lookups = parser.Translator.GetWordBitLookupReverse(bit);

                            // Iterate through lookups from reverse lookup
                            foreach(WordBitLookup lookup in lookups)
                            {
                                switch(lookup.WordBitType)
                                { 
                                    case WordBitType.SELogic:
                                        foreach (string relayWordBit in lookup.RelayWordBits)
                                        {
                                            SettingsTreeNode node = new SettingsTreeNode(parser, this, relayWordBit);
                                            nodes.Add(node);
                                        }
                                        break;

                                    case WordBitType.Range:
                                        foreach (string relayWordBit in lookup.RelayWordBits)
                                        {
                                            SettingsTreeNode node = new SettingsTreeNode(parser, this, relayWordBit);
                                            nodes.Add(node);
                                        }
                                        break;

                                    case WordBitType.String:
                                        foreach (string relayWordBit in lookup.RelayWordBits)
                                        {
                                            SettingsTreeNode node = new SettingsTreeNode(parser, this, relayWordBit);
                                            nodes.Add(node);
                                        }
                                        break;

                                    default:
                                        break;
                                }
                            }
                            
                            // Add nodes to children list
                            Children.Add(nodes);
                        }
                    }
                        
                    break;
            }
        }

        public static string[] ParseSetting(string setting)
        {
            // Remove comment if any
            setting = setting.Split('#')[0];

            // Replace string array
            string[] replaceString = new string[] { "*", "+", "!", "(", ")", "AND", "OR", "NOT", " ", "/", "\\", "R_TRIG", "F_TRIG" };
            foreach (string replace in replaceString)
            {
                // Replace mathematical oprerator and other things
                setting = setting.Replace(replace, "-");
            }

            return setting.Split('-');
        }
    }
}
