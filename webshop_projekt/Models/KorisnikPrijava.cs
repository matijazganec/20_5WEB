using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace webshop_projekt.Models
{
    public class KorisnikPrijava
    {
        [Display(Name="Korisnicko ime")]
        [Required]
        public string KorisnickoIme { get; set; }
        [Display(Name ="Lozinka")]
        [Required]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
    }
}