using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsHelper.LookupClasses
{
    public class RelayElement
    {
        public string pickup { get; set; }
        public string delay { get; set; }
        public string delay2 { get; set; }
        public string tripWord { get; set; }
        public string tripWordIfDelay { get; set; }
        public string tripWordIfDelay2 { get; set; }
        public float maxDelayCycles { get; set; }

        public string GetTripWord(string pickup, float setting)
        {
            if (pickup.ToUpper() == "OFF")
                return null;

            if(setting == 0.00)
            {
                return tripWord;
            }
            else if(setting > 0 && setting <= maxDelayCycles)
            {
                return tripWordIfDelay;
            }
            else
            {
                return tripWordIfDelay2;
            }
        }
    }
}
