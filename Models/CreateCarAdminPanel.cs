using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarLeasing.Models
{
    public class CreateCarAdminPanel
    {
        public int id_kolor { get; set; }
        public string url { get; set; }
        public decimal cena { get; set; }
        public string opis { get; set; }
        public int rok_produkcji { get; set; }
        public int przebieg { get; set; }
        public int pojemnosc_bagaznika { get; set; }
        public int id_paliwo { get; set; }
        public double moc { get; set; }
        public double spalanie { get; set; }
        public double pojemnosc { get; set; }
        public double emisja { get; set; }
        public double przyspieszenie { get; set; }
        public int id_marka { get; set; }
        public string model { get; set; }
        public int id_siedzenie { get; set; }
        public int id_nadwozie { get; set; }
        public int id_skrzynia { get; set; }
        public int id_opona { get; set; }
    }
}