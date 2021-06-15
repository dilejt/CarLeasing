using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarLeasing.Models
{
    public class EditClientPanel
    {
        public int id_zamowienie { get; set; }
        [Required(ErrorMessage = "Podaj długość wypożyczenia")]
        public int okres_id_okres { get; set; }
        [Required(ErrorMessage = "Podaj płatność okresową")]
        public int platnosc_id_platnosc { get; set; }
        [Required(ErrorMessage = "Podaj dystans")]
        public int dystans_id_dystans { get; set; }
        public int samochod_id_samochod { get; set; }
        public int uzytkownik_id_uzytkownik { get; set; }
        [Required(ErrorMessage = "Podaj kolor samochodu")]
        public int kolor_id_kolor { get; set; }
    }
}