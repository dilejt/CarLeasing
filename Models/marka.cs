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
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class marka
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public marka()
        {
            this.model = new HashSet<model>();
        }
    
        public int id_marka { get; set; }
        [Required(ErrorMessage = "Podaj nazw?")]
        public string nazwa { get; set; }
        [Required(ErrorMessage = "Podaj opis")]
        public string opis { get; set; }
        public string logo { get; set; }
        [Required(ErrorMessage = "Za??cz plik")]
        public HttpPostedFileBase MarkaFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<model> model { get; set; }
    }
}
