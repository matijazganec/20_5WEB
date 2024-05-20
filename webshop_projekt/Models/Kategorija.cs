using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webshop_projekt.Models
{
    [Table("kategorija")]
    public class Kategorija
    {
        [Key]
        [Required]
        [Column("idKategorija")]
        [Display(Name ="ID kategorije")]
        public int KategorijaId { get; set; }

        [Required]
        [Column("NazivKategorija")]
        [Display(Name ="Naziv kategorije")]
        public string KategorijaName { get; set; }
    }
}