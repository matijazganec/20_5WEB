using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webshop_projekt.Models
{
    [Table("proizvodac")]
    public class Proizvodac
    {
        [Key]
        [Required]
        [Display(Name ="ID Proizvodaca")]
        public int idProizvodac { get; set; }

        [Required]
        [Display(Name ="Naziv proizvodaca")]
        public string ProizvodacName { get; set; }
    }
}