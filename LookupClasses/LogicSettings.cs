using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsHelper.LookupClasses
{
    public class LogicSettings
    {
        public TimerSettings timers { get; set; }
        public bool isShortHand { get; set; }
        public bool logicVariables { get; set; }

        public class TimerSettings
        {
            public string inputFormat { get; set; }
            public string pickupFormat { get; set; }
            public string dropoutFormat { get; set; }
            public int maxTimers { get; set; }
            public float maxCycles { get; set; }
            public bool isCycles { get; set; }
        }
    }
}
