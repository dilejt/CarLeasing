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
    
    public partial class samochod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public samochod()
        {
            this.zamowienie = new HashSet<zamowienie>();
            this.zdjecie = new HashSet<zdjecie>();
        }
    
        public int id_samochod { get; set; }
        public decimal cena { get; set; }
        public int kolor_id_kolor { get; set; }
        public int parametr_id_parametr { get; set; }
    
        public virtual kolor kolor { get; set; }
        public virtual parametr parametr { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zamowienie> zamowienie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zdjecie> zdjecie { get; set; }
        public string SelectedModel { get; set; }
    }
}
