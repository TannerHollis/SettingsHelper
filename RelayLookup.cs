using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SettingsHelper
{
    public class RelayLookup
    {
        public string RelayType { get; set; }
        public object global = new 
        { 
            FREQ = "NA",
            ROT = "NA",
        };
        public RelayLookup() 
        { 
            
        }

        public static RelayLookup FromFile(string fileName)
        {
            return JsonSerializer.Deserialize<RelayLookup>(fileName);
        }
    }
}
