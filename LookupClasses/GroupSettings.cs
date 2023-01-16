using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsHelper.LookupClasses
{
    public class GroupSettings
    {
        public OvercurrentSettings overcurrent { get; set; }
        public HarmonicBlockingSettings harmonicBlocking { get; set; }
        public DirectionalSettings directional { get; set; }
        public VoltageSettings voltage { get; set; }
        public FrequencySettings frequency { get; set; }
    }
}
