using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsHelper.LookupClasses
{
    public class GlobalSettings
    {
        public string FREQ { get; set; }
        public string ROT { get; set; }
        public string PTCONN { get; set; }
        public string PTR { get; set; }
        public string CTR { get; set; }
        public string CTRN { get; set; }

        public VNOMTypeDef VNOM { get; set; }

        public class VNOMTypeDef
        {
            public string wordBit { get; set; }
            public bool LL { get; set; }
        }

        public string RID { get; set; }
        public string TID { get; set; }
    }
}
