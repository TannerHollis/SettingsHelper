using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsHelper;

namespace SettingsHelperTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Test SettingsGroup.cs
            {
                string exampleFile = "Set_G1.txt";
                SettingsGroup settingGroup1 = new SettingsGroup(exampleFile);
                settingGroup1["PHROT"].SetSetting("BCA");
                settingGroup1.WriteSettingsToFile();
            }

            // Test SettingsTranslator.cs
            {
                string[] wordBits =
                {
                    "FREQ","ROT", "PTR", "CTR", "27P1P", "27P1D", "27P2P", "27P2D",
                    "59P1P", "59P1D", "59P2P", "59P2D", "81U1P", "81U1D", "81U2P", "81U2D",
                    "81O1P", "81O1D", "81O2P", "81O2D"
                };
                string relayType = "SEL 351-7";
                SettingsTranslator translator = new SettingsTranslator(relayType);
                
                foreach(string wordBit in wordBits)
                {
                    Console.WriteLine(wordBit + " -> " + translator.LookupWordBit(wordBit));
                }
            }

            // Test CalcSheet.cs
            {
                string exampleWorkbook = "Example_Settings_Calc_Sheet.xlsx";
                exampleWorkbook = Path.Combine(Directory.GetCurrentDirectory(), exampleWorkbook);
                CalcSheet calcSheet = new CalcSheet(exampleWorkbook);

                calcSheet.PrintNames();

                calcSheet.Close();
            }
        }
    }
}
