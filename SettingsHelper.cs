using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;

namespace SettingsHelper
{
    public class SettingsHelper
    {
        public static List<Setting> LoadSettingsFromFile(string fileName)
        {
            List<Setting> settings = new List<Setting>();

            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                try
                {
                    Setting setting = Setting.FromLine(line);
                    settings.Add(setting);
                }
                catch 
                { 
                    // Do nothing... 
                }
            }

            return settings;
        }

        public static void WriteSettingsToFile(List<Setting> settings, string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            List<Setting> changedSettings = GetChangedSettings(settings);

            Console.WriteLine("Writing " + changedSettings.Count + " change(s) to file: " + fileName);

            foreach (Setting setting in changedSettings)
            {
                Console.WriteLine(" - Writing setting: " + setting.GetWordbit());

                for(int i = 0; i < lines.Length; i++)
                {
                    try
                    {
                        Setting existingSetting = Setting.FromLine(lines[i]);
                        if(setting.WordBitEquals(existingSetting))
                        {
                            lines[i] = setting.ToString();
                        }
                    }
                    catch
                    {
                        // Do nothing...
                    }
                }
            }

            File.WriteAllLines(fileName, lines);
        }

        public static List<Setting> GetChangedSettings(List<Setting> settings)
        {
            List<Setting> changedSettings = new List<Setting>();

            foreach(Setting setting in settings)
            {
                if(setting.HasChanged())
                {
                    changedSettings.Add(setting);
                }
            }

            return changedSettings;
        }

        public static void FindAndReplace(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText)
        {
            //options
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            //execute find and replace
            doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
            Console.ReadLine();
        }
    }
}
