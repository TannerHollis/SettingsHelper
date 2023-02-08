namespace SettingsHelper
{
    public class RelayFileAssociation
    {
        public string RelayType { get; set; }
        public string RDBTemplate { get; set; }
        public string TranslationLayerFile { get; set; }

        public RelayFileAssociation() { }

        public RelayFileAssociation(string RelayType, string RDBTemplate, string TranslationLayerFile)
        {
            this.RelayType = RelayType;
            this.RDBTemplate = RDBTemplate;
            this.TranslationLayerFile = TranslationLayerFile;
        }
    }
}
