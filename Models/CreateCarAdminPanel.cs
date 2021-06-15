using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarLeasing.Models
{
    public class CreateCarAdminPanel
    {
        [Required(ErrorMessage = "Podaj kolor")]
        public int id_kolor { get; set; }
        [Required(ErrorMessage = "Podaj cenę")]
        public decimal cena { get; set; }
        [Required(ErrorMessage = "Podaj opis")]
        public string opis { get; set; }
        [Required(ErrorMessage = "Podaj rok produkcji")]
        public int rok_produkcji { get; set; }
        [Required(ErrorMessage = "Podaj przebieg")]
        public int przebieg { get; set; }
        [Required(ErrorMessage = "Podaj pojemność bagażnika")]
        public int pojemnosc_bagaznika { get; set; }
        [Required(ErrorMessage = "Podaj rodzaj silnika")]
        public int id_paliwo { get; set; }
        [Required(ErrorMessage = "Podaj moc silnika")]
        public double moc { get; set; }
        [Required(ErrorMessage = "Podaj spalanie silnika")]
        public double spalanie { get; set; }
        [Required(ErrorMessage = "Podaj pojemność silnika")]
        public double pojemnosc { get; set; }
        [Required(ErrorMessage = "Podaj emisje silnika")]
        public double emisja { get; set; }
        [Required(ErrorMessage = "Podaj przyspieszenie silnika")]
        public double przyspieszenie { get; set; }
        [Required(ErrorMessage = "Podaj markę")]
        public int id_marka { get; set; }
        [Required(ErrorMessage = "Podaj model")]
        public string model { get; set; }
        [Required(ErrorMessage = "Podaj ilość miejsc")]
        public int id_siedzenie { get; set; }
        [Required(ErrorMessage = "Podaj rodzaj nadwozia")]
        public int id_nadwozie { get; set; }
        [Required(ErrorMessage = "Podaj rodzaj skrzyni")]
        public int id_skrzynia { get; set; }
        [Required(ErrorMessage = "Podaj model opon")]
        public int id_opona { get; set; }
    }
}