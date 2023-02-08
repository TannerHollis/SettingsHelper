using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SettingsHelperUI
{
    public class ApplicationSettings
    {
        public static readonly string SettingsFileLocation = "settings.json";

        public string XLSFileLocation { get; set; }
        public string XLSFile { get; set; }
        public string RDBSaveLocation { get; set; }
        public string RDBSaveFile { get; set; }
        public string SettingsTreeRDBLocation { get; set; }
        public string SettingsTreeRDBFile { get; set; }
        public string SettingsTreeWordBit { get; set; }

        public static void Init()
        {
            if(!File.Exists(SettingsFileLocation))
            {
                Default.ToFile();
            }
        }

        private static ApplicationSettings FromFile()
        {
            string jsonString = File.ReadAllText(SettingsFileLocation);
            return JsonSerializer.Deserialize<ApplicationSettings>(jsonString);
        }

        private void ToFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(SettingsFileLocation, jsonString);
        }

        public static string GetString(string stringName)
        {
            ApplicationSettings settings = FromFile();

            switch(stringName)
            {
                case "XLSFileLocation":
                    return settings.XLSFileLocation;

                case "XLSFile":
                    return settings.XLSFile;

                case "RDBSaveLocation":
                    return settings.RDBSaveLocation;

                case "RDBSaveFile":
                    return settings.RDBSaveFile;

                case "SettingsTreeRDBLocation":
                    return settings.SettingsTreeRDBLocation;

                case "SettingsTreeRDBFile":
                    return settings.SettingsTreeRDBFile;

                case "SettingsTreeWordBit":
                    return settings.SettingsTreeWordBit;

                default:
                    return string.Empty;
            }
        }

        public static void SetString(string stringName, string setting)
        {
            ApplicationSettings settings = FromFile();

            switch (stringName)
            {
                case "XLSFileLocation":
                    settings.XLSFileLocation = setting;
                    break;

                case "XLSFile":
                    settings.XLSFile = setting;
                    break;

                case "RDBSaveLocation":
                    settings.RDBSaveLocation = setting;
                    break;

                case "RDBSaveFile":
                    settings.RDBSaveFile = setting;
                    break;

                case "SettingsTreeRDBLocation":
                    settings.SettingsTreeRDBLocation = setting;
                    break;

                case "SettingsTreeRDBFile":
                    settings.SettingsTreeRDBFile = setting;
                    break;

                case "SettingsTreeWordBit":
                    settings.SettingsTreeWordBit = setting;
                    break;

                default:
                    return;
            }

            settings.ToFile();
        }
        private static ApplicationSettings Default
        {
            get
            {
                ApplicationSettings appSettings = new ApplicationSettings();

                appSettings.XLSFileLocation = Directory.GetCurrentDirectory();
                appSettings.XLSFile = "";
                appSettings.RDBSaveLocation = Directory.GetCurrentDirectory();
                appSettings.RDBSaveFile = "";
                appSettings.SettingsTreeRDBLocation = Directory.GetCurrentDirectory();
                appSettings.SettingsTreeRDBFile = "";
                appSettings.SettingsTreeWordBit = "";
                
                return appSettings;
            }
        }
    }
}
