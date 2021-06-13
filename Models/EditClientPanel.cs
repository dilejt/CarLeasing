using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarLeasing.Models
{
    public class EditClientPanel
    {
        public int id_zamowienie { get; set; }
        public int okres_id_okres { get; set; }
        public int platnosc_id_platnosc { get; set; }
        public int dystans_id_dystans { get; set; }
        public int samochod_id_samochod { get; set; }
        public int uzytkownik_id_uzytkownik { get; set; }
        public int kolor_id_kolor { get; set; }
    }
}