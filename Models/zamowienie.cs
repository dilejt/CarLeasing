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
    
    public partial class zamowienie
    {
        public int id_zamowienie { get; set; }
        public decimal koszt { get; set; }
        public int okres_id_okres { get; set; }
        public int platnosc_id_platnosc { get; set; }
        public int dystans_id_dystans { get; set; }
        public int samochod_id_samochod { get; set; }
        public System.DateTime data_zlozenia { get; set; }
        public int uzytkownik_id_uzytkownik { get; set; }
    
        public virtual dystans dystans { get; set; }
        public virtual okres okres { get; set; }
        public virtual platnosc platnosc { get; set; }
        public virtual samochod samochod { get; set; }
        public virtual uzytkownik uzytkownik { get; set; }
    }
}
