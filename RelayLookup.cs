using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace SettingsHelper
{
    public class RelayLookup
    {
        public List<WordBitLookup> WordBitLookups { get; set; }

        public RelayLookup()
        {
            WordBitLookups = new List<WordBitLookup>();
        }

        public RelayLookup(List<WordBitLookup> words)
        {
            WordBitLookups = words;
        }

        public string[] GetRelayWordBits(string genericWordBit)
        {
            foreach(WordBitLookup wordBitLookup in WordBitLookups)
            {
                if(wordBitLookup.GenericWordBit == genericWordBit)
                {
                    return wordBitLookup.RelayWordBits.ToArray();
                }
            }

            return null;
        }

        public WordBitLookup GetRelayWordBitLookup(string relayWordBit)
        {
            foreach (WordBitLookup wordBitLookup in WordBitLookups)
            {
                if (wordBitLookup.RelayWordBits.Contains(relayWordBit))
                {
                    return wordBitLookup;
                }
            }

            return null;
        }

        public WordBitLookup[] GetWordBitLookupReverse(string relayWordBit)
        {
            List<WordBitLookup> wordBitLookups = new List<WordBitLookup>();

            foreach (WordBitLookup wordBitLookup in WordBitLookups)
            {
                if (wordBitLookup.ReverseLookup.Contains(relayWordBit))
                {
                    wordBitLookups.Add(wordBitLookup);
                }

            }

            return wordBitLookups.ToArray();
        }

        public static RelayLookup FromCSVFile(string fileName)
        {
            RelayLookup relayLookup = new RelayLookup();

            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.ReadFields();
                while(!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    relayLookup.WordBitLookups.Add(WordBitLookup.FromCSVLine(fields));
                }
            }

            return relayLookup;
        }

        public static RelayLookup FromFile(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<RelayLookup>(jsonString);
        }

        public void ToFile(string fileName)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
