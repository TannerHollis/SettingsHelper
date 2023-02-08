using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SettingsHelper
{
    public class KnownWordBits
    {
        public List<string> GenericWordBits { get; set; }
        public List<string> ComplexWordBits { get; set; }

        public KnownWordBits()
        {
            GenericWordBits = new List<string>();
            ComplexWordBits = new List<string>();
        }

        public KnownWordBits(List<string> GenericWordBits, List<string> ComplexWordBits)
        {
            this.GenericWordBits = GenericWordBits;
            this.ComplexWordBits = ComplexWordBits;
        }

        public static KnownWordBits FromFile(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);

            return JsonSerializer.Deserialize<KnownWordBits>(jsonString);   
        }

        private void ToFile(string fileName)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);
        }

        private static KnownWordBits Default
        {
            get
            {
                KnownWordBits known = new KnownWordBits();

                known.GenericWordBits = new List<string>()
                {
                    "FREQ",
                    "ROT",
                    "PTR",
                    "CTR",
                    "VNOM",

                    "V27P1P",
                    "V27P2P",
                    "V59P1P",
                    "V59P2P",
                    "V59N1P",

                    "F81U1P",
                    "F81U2P",
                    "F81O1P",
                    "F81O2P",

                    "I50P1P",
                    "I50P2P",
                    "I50P3P",
                    "I50P4P",
                    "I50G1P",
                    "I50D2P",
                    "I50G3P",

                    "I51P1P",
                    "I51P1C",
                    "I51P1TD",
                    "I51P1TM",
                    "I51P1TA",
                    "I51P1R",
                    "I51P2P",
                    "I51P2C",
                    "I51P2TD",
                    "I51P2TM",
                    "I51P2TA",
                    "I51P2R",
                    "I51G1P",
                    "I51G1C",
                    "I51G1TD",
                    "I51G1TM",
                    "I51G1TA",
                    "I51T1R",
                    "I51G2P",
                    "I51G2C",
                    "I51G2TD",
                    "I51G2TM",
                    "I51G2TA",
                    "I51G2R",

                    "PPWR1P",
                    "P3PWR1P",
                    "PPWR1T",
                    "PPWR2P",
                    "P3PWR2P",
                    "PPWR2T"
                };

                known.ComplexWordBits = new List<string>()
                {
                    "V27P1D",
                    "V27P2D",
                    "V59P1D",
                    "V59P2D",
                    "V59N1D",

                    "F81U1D",
                    "F81U2D",
                    "F81O1D",
                    "F81O2D",

                    "I50P2D",
                    "I50P2D",
                    "I50P3D",
                    "I50P4D",
                    "I50G1D",
                    "I50G2D",
                    "I50G3D",

                    "PPWR1D",
                    "PPWR2D"
                };

                return known;
            }
        }

        public static void Init()
        {
            Default.ToFile(FileManager.KnownWordBitsFile);
        }
    }
}
