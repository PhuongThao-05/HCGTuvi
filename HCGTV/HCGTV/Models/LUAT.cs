//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HCGTV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LUAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LUAT()
        {
            this.CTLUAT = new HashSet<CTLUAT>();
        }
    
        public int MALUAT { get; set; }
        public Nullable<int> MAKL { get; set; }
        public string NDLUAT { get; set; }
        public string LOIKHUYEN { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTLUAT> CTLUAT { get; set; }
        public virtual KETLUAN KETLUAN { get; set; }
    }
}
