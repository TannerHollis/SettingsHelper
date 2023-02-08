namespace SettingsHelper
{
    public class LoggerFileSettings
    {
        public string LogFolder { get; set; }
        public string LogFileName { get; set; }

        public LoggerFileSettings() { }

        public LoggerFileSettings(string LogFolder, string LogFileName)
        {
            this.LogFolder = LogFolder;
            this.LogFileName = LogFileName;
        }
    }
}
