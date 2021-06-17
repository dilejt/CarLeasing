using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarLeasing.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Podaj email")]
        public string email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Podaj hasło")]
        public string haslo { get; set; }
    }
}