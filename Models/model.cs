//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarLeasing.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class model
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public model()
        {
            this.parametr = new HashSet<parametr>();
        }
    
        public int id_model { get; set; }
        public string nazwa { get; set; }
        public int marka_id_marka { get; set; }
        public int silnik_id_silnik { get; set; }
    
        public virtual marka marka { get; set; }
        public virtual silnik silnik { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parametr> parametr { get; set; }
    }
}
