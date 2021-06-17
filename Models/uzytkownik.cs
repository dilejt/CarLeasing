namespace CarLeasing.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class uzytkownik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public uzytkownik()
        {
            this.zamowienie = new HashSet<zamowienie>();
        }
    
        public int id_uzytkownik { get; set; }

        [Required(ErrorMessage = "Podaj imiê")]
        public string imie { get; set; }

        [Required(ErrorMessage = "Podaj nazwisko")]
        public string nazwisko { get; set; }

        [Required(ErrorMessage = "Podaj email")]
        [Remote("CheckEmailExist", "Register", ErrorMessage = "Podany email jest zajêty!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Podaj telefon")]
        [Remote("CheckTelExist", "Register", ErrorMessage = "Podany telefon jest zajêty!")]
        public string telefon { get; set; }

        [DataType(DataType.Password)]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Bezpieczne has³o powinno sk³adaæ siê od 8 do 32 znaków")]
        [Required(ErrorMessage = "Podaj has³o")]
        public string haslo { get; set; }
        public int rola_id_rola { get; set; }
    
        public virtual rola rola { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zamowienie> zamowienie { get; set; }
    }
}
