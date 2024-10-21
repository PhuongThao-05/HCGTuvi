using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCGTV.Models.Metadata
{
    public class LUATMetadata
    {
        public int MALUAT { get; set; }
        public Nullable<int> MAKL { get; set; }
        public string NDLUAT { get; set; }
        public string LOIKHUYEN { get; set; }

        public KETLUAN KETLUAN;
    }
}