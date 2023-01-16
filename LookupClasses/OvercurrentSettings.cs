using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsHelper.LookupClasses
{
    public class OvercurrentSettings
    {
        public InstantaneousSettings instantaneous { get; set; }

        public TimeSettings[] time { get; set; }

        public class TimeSettings
        {
            public CurveSettings phase { get; set; }
            public CurveSettings ground { get; set; }
            public CurveSettings neutral { get; set; }
            public CurveSettings negative { get; set; }
        }

        public class CurveSettings
        {
            public bool? is3P { get; set; }
            public string pickup { get; set; }
            public string curve { get; set; }
            public string timeDial { get; set; }
            public string emReset { get; set; }
            public string timeAdder { get; set; }
            public string minimumTime { get; set; }
        }

        public class InstantaneousSettings
        {
            public string I50P1P { get; set; }
            public string I50P1D { get; set; }
            public string I50P2P { get; set; }
            public string I50P2D { get; set; }
            public string I50P3P { get; set; }
            public string I50P3D { get; set; }
            public string I50P4P { get; set; }
            public string I50G1P { get; set; }
            public string I50G1D { get; set; }
            public string I50G2P { get; set; }
            public string I50G2D { get; set; }
            public string I50G3P { get; set; }
            public string I50G3D { get; set; }
            public string I50G4P { get; set; }
            public string I50G4D { get; set; }
        }
    }
}
