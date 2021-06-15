using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarLeasing.Models
{
    public class MakeOrder
    {
        public decimal koszt { get; set; }
        public int id_okres { get; set; }
        public int id_platnosc { get; set; }
        public int id_dystans { get; set; }
        public int id_opona { get; set; }
        public int id_firma { get; set; }
        public int id_sezon { get; set; }
        public int id_samochod { get; set; }

    }
}