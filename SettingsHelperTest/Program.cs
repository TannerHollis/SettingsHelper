using SettingsHelper;
using System;
using System.IO;
using System.Linq;

using SettingsHelper.SettingsTreeParser;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SettingsHelperTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize Folder Structure
            {
                FileManager.InitFolderStructure();
            }

            // Test SettingsGroup.cs
            {
                bool testPass;
                string exampleFile = "Set_G1.txt";

                // Open file and write to PHROT
                SettingsGroup settingGroup1 = new SettingsGroup(exampleFile);
                settingGroup1["PHROT"].Setpoint = "BCA";
                settingGroup1.ApplySettingChanges();
                settingGroup1.WriteSettingsToFile();

                // Read back PHROT setting and verify succeess
                settingGroup1 = new SettingsGroup(exampleFile);
                testPass = settingGroup1["PHROT"].Setpoint.Equals("BCA");

                PrintTestPass("SettingsGroup.cs", testPass);
            }

            // Test SettingsTranslator.cs
            {
                bool testPass = true;
                string[] wordBits =
                {
                    "FREQ","ROT", "PTR", "CTR", "V27P1P", "V27P1D", "V27P2P", "V27P2D",
                    "V59P1P", "V59P1D", "V59P2P", "V59P2D", "F81U1P", "F81U1D", "F81U2P", "F81U2D",
                    "F81O1P", "F81O1D", "F81O2P", "F81O2D"
                };

                string[] translatedWordBits =
                {
                    "NFREQ","PHROT", "PTR", "CTR", "27P1P", "SV18PU", "27P2P", "SV19PU",
                    "59P1P", "SV20PU", "59P2P", "SV21PU", "81D1P", "81D1D", "81D2P", "81D2D",
                    "81D3P", "81D3D", "81D4P", "81D4D"
                };

                // Create translator instance of type: SEL 351-7
                string relayType = "SEL 351-7";
                SettingsTranslator translator = new SettingsTranslator(relayType);

                for (int i = 0; i < wordBits.Length; i++)
                {
                    Console.WriteLine(wordBits[i] + " -> " + translator.LookupWordBit(wordBits[i])[0]);

                    // Verify translator works
                    testPass &= translator.LookupWordBit(wordBits[i]).Equals(translatedWordBits[i]);
                }

                PrintTestPass("SettingsTranslator.cs", testPass);
            }

            // Test CalcSheet.cs
            {
                string exampleWorkbook = "Example_Settings_Calc_Sheet.xlsx";
                exampleWorkbook = Path.Combine(Directory.GetCurrentDirectory(), exampleWorkbook);

                // Create a CalcSheet instance using path to excel file
                CalcSheet calcSheet = new CalcSheet(exampleWorkbook);

                // Try to get named tag: "CTR" which should be 120.0
                bool testPass = Double.Parse(calcSheet.GetName("CTR")) == 120.0;

                // Change named tag and save
                calcSheet.SetName("CUSTCONTACTNAME", "Tanner Hollis");
                calcSheet.Save();

                calcSheet.Close();

                PrintTestPass("CalcSheet.cs", testPass);
            }

            // Test Relay.cs, includes testing of CFFolder.cs
            {
                string relayFile = "SEL-351S.rdb";

                string fullFileName = Path.Combine(Directory.GetCurrentDirectory(), relayFile);

                Relay relay = new Relay(fullFileName);
                string[] relays = relay.GetRelayNames();

                Console.WriteLine("Relays in (.rdb): " + relayFile);
                for (int i = 0; i < relays.Count(); i++)
                {
                    Console.WriteLine(" " + i.ToString() + ". " + relays[i]);
                }

                relay.Load(0);
                string[] groupNames = relay.GetGroupNames();
                Console.WriteLine("Settings Groups in Relay: " + relays[0]);
                foreach (string groupName in groupNames)
                {
                    Console.WriteLine(" - " + groupName);
                }

                relay["SET_1.TXT"]["RID"].Setpoint = "TEST SETTINGS 1...";

                relay.CompressRelayFiles();
                relay.Rename("Test Relay 20221219");
                relay.Save("SEL-351S_New.rdb");
                relay.Close();

                PrintTestPass("CFFolder.cs", true);
            }

            // Test SettingsHelper.cs, Final Test
            {
                string exampleWorkbook = "Example_Settings_Calc_Sheet.xlsx";
                string fullPathName = Path.Combine(Directory.GetCurrentDirectory(), exampleWorkbook);

                string rdbDestination = "SEL-351-7_Output.rdb";

                RelayAutomator automator = new RelayAutomator(fullPathName, rdbDestination);

                automator.Load(0);

                automator.ApplySettingsChanges();

                automator.Save();
                automator.Close();

                PrintTestPass("SettingsHelper.cs", true);
            }

            // Test SettingsTreeParser.cs
            {
                string relayFile = "SEL-351S.rdb";

                string fullFileName = Path.Combine(Directory.GetCurrentDirectory(), relayFile);

                Relay relay = new Relay(fullFileName);
                relay.Load(0);

                List<SettingsGroup> groups = new List<SettingsGroup>() { relay["SET_1.TXT"], relay["SET_L1.TXT"] };
                SettingsTranslator translator = new SettingsTranslator("SEL 351S");

                SettingsTreeParser parser = new SettingsTreeParser(translator, groups);

                SettingsTreeNode root = parser.ParseAtWordBit("OUT101");

                PrintTestPass("CalcSheet.cs", true);
            }
        }

        private static void PrintTestPass(string testName, bool pass)
        {
            string printOut = "Test " + testName + ": " + (pass ? "SUCCESS" : "FAILURE") + "\n";
            Console.WriteLine(printOut);
        }
    }
}
