using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsHelper
{
    public enum WordBitType
    {
        None,
        SELogic,
        YesNo,
        Range,
        String,
        Selection,
    }

    public class WordBitLookup
    {
        public string GenericWordBit { get; set; }
        public List<string> RelayWordBits { get; set; }
        public List<string> ReverseLookup { get; set; }
        public WordBitType WordBitType { get; set; }
        public string SelectionOptions { get; set; }
        public float MinRange { get; set; }
        public float MaxRange { get; set; }
        public string DataComment { get; set; }
        public object Data { get; set; }

        public WordBitLookup()
        {
            RelayWordBits = new List<string>();
            ReverseLookup = new List<string>();
        }

        public static WordBitLookup FromCSVLine(string[] csvLine)
        {
            WordBitLookup wordBitLookup = new WordBitLookup();
            
            wordBitLookup.GenericWordBit = csvLine[0];
            wordBitLookup.RelayWordBits.AddRange(csvLine[1].Split(','));
            wordBitLookup.ReverseLookup.AddRange(csvLine[2].Split(','));

            if (csvLine[3] != string.Empty)
                wordBitLookup.WordBitType = (WordBitType)Enum.Parse(typeof(WordBitType), csvLine[3]);
            else
                wordBitLookup.WordBitType = WordBitType.None;
            
            wordBitLookup.SelectionOptions = csvLine[4];
            
            if (csvLine[5] != string.Empty)
                wordBitLookup.MinRange = float.Parse(csvLine[5]);
            else
                wordBitLookup.MinRange = 0;
            
            if (csvLine[6] != string.Empty)
                wordBitLookup.MaxRange = float.Parse(csvLine[5]);
            else
                wordBitLookup.MaxRange = 0;

            wordBitLookup.DataComment = csvLine[7];
            wordBitLookup.Data = csvLine[8];

            return wordBitLookup;
        }
    }
}
