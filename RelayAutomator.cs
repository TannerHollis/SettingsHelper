using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;

namespace SettingsHelper
{
    public class RelayAutomator
    {
        private string _calcSheetFileName;
        private string _relayType;

        private CalcSheet _calcSheet;
        private SettingsTranslator _settingsTranslator;
        private Relay _relay;

        private string _rdbFileDestination;

        public RelayAutomator(string calcSheetFileName, string rdbFileDestination)
        {
            _calcSheetFileName = calcSheetFileName;
            _rdbFileDestination = rdbFileDestination;
            Initialize();
        }

        public string CalcSheetFileName
        {
            get { return _calcSheetFileName; }
        }

        public string RDBFileName
        {
            get { return _rdbFileDestination; }
        }

        public string RelayType
        {
            get { return _relayType; }
        }

        public List<SettingChange> SettingsChanges
        {
            get { return GetSettingsChanges(); }
        }

        public void ApplySettingsChanges()
        {
            List<SettingChange> settingsChanges = GetSettingsChanges();
            _relay.MergeSettingsChanges(settingsChanges);
            _relay.CompressRelayFiles();

            string newRelayName = _calcSheet.GetName("RELAYNAME");
            if(_calcSheet.GetName("RELAYNAMEAPPENDDATE").Equals("Yes"))
            {
                 newRelayName += " " + _calcSheet.GetName("DATE");
            }

            _relay.Rename(newRelayName);
        }

        private List<SettingChange> GetSettingsChanges()
        {
            List<SettingChange> settingChanges = new List<SettingChange>();

            string[] genericWordBits = FileManager.GetGenericWordBits();

            foreach(string genericWordBit in genericWordBits)
            {
                string setting = _calcSheet.GetName(genericWordBit);
                if (setting.Equals(string.Empty))
                {
                    FileManager.Log("Could not get Tag: " + genericWordBit + " from calc sheet.", FileManager.LogLevel.Warning);
                    continue;
                }
                List<SettingChange> changes = _settingsTranslator.SetGenericSetting(genericWordBit, setting);
                settingChanges.AddRange(changes);
            }

            string[] complexWordBits = FileManager.GetComplexWordBits();

            foreach(string complexWordBit in complexWordBits)
            {
                string setting = _calcSheet.GetName(complexWordBit);
                if (setting.Equals(string.Empty))
                {
                    FileManager.Log("Could not get Tag: " + complexWordBit + " from calc sheet.", FileManager.LogLevel.Warning);
                    continue;
                }
                FileManager.Log(complexWordBit, FileManager.LogLevel.Info);
            }

            return settingChanges;
        }

        private void Initialize()
        {
            _calcSheet = new CalcSheet(_calcSheetFileName);

            // Get Relay Type name from Excel
            _relayType = _calcSheet.GetName("RELAYTYPE");

            // Open Settings Associated Translation layer file
            _settingsTranslator = new SettingsTranslator(_relayType);

            // Copy template RDB file to destination and open in place
            CopyRDBFileToDest();
            _relay = new Relay(_rdbFileDestination);
        }

        public void Load(int index)
        {
            _relay.Load(index);
        }

        private void CopyRDBFileToDest()
        {
            File.Copy(FileManager.GetRDBTemplateFileName(_relayType), _rdbFileDestination, true);
        }

        public void Save()
        {
            _relay.Save(_rdbFileDestination);
        }

        public void Close()
        {
            _calcSheet.Close();
            _relay.Close();
        }
    }
}
