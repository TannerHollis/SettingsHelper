using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsHelper;

using OpenMcdf;
using System.Windows.Forms.VisualStyles;

namespace SettingsHelperTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Test SettingsGroup.cs
            {
                bool testPass;
                string exampleFile = "Set_G1.txt";

                // Open file and write to PHROT
                SettingsGroup settingGroup1 = new SettingsGroup(exampleFile);
                settingGroup1["PHROT"].SetSetting("BCA");
                settingGroup1.WriteSettingsToFile();

                // Read back PHROT setting and verify succeess
                settingGroup1 = new SettingsGroup(exampleFile);
                testPass = settingGroup1["PHROT"].GetSetting().Equals("BCA");

                PrintTestPass("SettingsGroup.cs", testPass);
            }

            // Test SettingsTranslator.cs
            {
                bool testPass = true;
                string[] wordBits =
                {
                    "FREQ","ROT", "PTR", "CTR", "27P1P", "27P1D", "27P2P", "27P2D",
                    "59P1P", "59P1D", "59P2P", "59P2D", "81U1P", "81U1D", "81U2P", "81U2D",
                    "81O1P", "81O1D", "81O2P", "81O2D"
                };

                string[] translatedWordBits =
                {
                    "NFREQ","PRROT", "PTR", "CTR", "27P1P", "SV18PU", "27P2P", "SV19PU",
                    "59P1P", "SV20PU", "59P2P", "SV21PU", "81D1P", "81D1D", "81D2P", "81D2D",
                    "81D3P", "81D3D", "81D4P", "81D4D"
                };

                // Create translator instance of type: SEL 351-7
                string relayType = "SEL 351-7";
                SettingsTranslator translator = new SettingsTranslator(relayType);
                
                for(int i  = 0; i < wordBits.Length; i++)
                {
                    Console.WriteLine(wordBits[i] + " -> " + translator.LookupWordBit(wordBits[i]));
                    
                    // Verify translator works
                    testPass &= translator.LookupWordBit(wordBits[i]).Equals(translatedWordBits[i]);
                }

                PrintTestPass("SettingsTranslator.cs", testPass);
            }

            // Test CalcSheet.cs
            {
                
                string exampleWorkbook = "Example_Settings_Calc_Sheet.xlsx";
                exampleWorkbook = Path.Combine(Directory.GetCurrentDirectory(), exampleWorkbook);
                CalcSheet calcSheet = new CalcSheet(exampleWorkbook);

                bool testPass = Double.Parse(calcSheet.GetName("CTR")) == 120.0;

                calcSheet.PrintNames();

                calcSheet.Close();

                PrintTestPass("CalcSheet.cs", testPass);
            }

            // Test Relay.cs, includes testing of CFFolder.cs
            {
                string relayFile = "SEL-351S.rdb";

                string fullFileName = Path.Combine(Directory.GetCurrentDirectory(), relayFile);

                Relay relay = new Relay(fullFileName);
                string[] relays = relay.GetRelayNames();

                foreach(string relayName in relays)
                {
                    Console.WriteLine(relayName);
                }

                relay.LoadRelay(0);
                string[] groupNames = relay.GetGroupNames();

                foreach(string groupName in groupNames)
                {
                    Console.WriteLine(groupName);
                }

                relay["SET_1.TXT"]["RID"].SetSetting("TEST SETTINGS 1...");
                relay.SetSetting("Z1MAG", "12.34");

                relay.CompressRelayFiles();

                relay.Save("SEL-351S_New.rdb");
            }
        }

        private static void PrintTestPass(string testName, bool pass)
        {
            string printOut = "Test " + testName + ": " + (pass ? "SUCCESS" : "FAILURE") + "\n";
            Console.WriteLine(printOut);
        }
    }
}
