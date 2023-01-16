using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using SettingsHelper.LookupClasses;

namespace SettingsHelper
{
    public class RelayLookup
    {
        public string relayType { get; set; }
        public GlobalSettings global { get; set; }
        public LogicSettings logic { get; set; }
        public GroupSettings group { get; set; }

        public static RelayLookup FromFile(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<RelayLookup>(jsonString);
        }

        public void ToFile(string fileName)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
