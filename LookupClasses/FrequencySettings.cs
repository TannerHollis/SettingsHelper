using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsHelper.LookupClasses
{
    public class FrequencySettings
    {
        public float maxDelayCycles { get; set; }
        public RelayElement F81U1 { get; set; }
        public RelayElement F81U2 { get; set; }
        public RelayElement F81O1 { get; set; }
        public RelayElement F81O2 { get; set; }
    }
}
