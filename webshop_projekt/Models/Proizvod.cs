using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webshop_projekt.Models
{
    [Table("proizvod")]
    public class Proizvod
    {
        [Key]
        [Required]
        [Display(Name ="Id prozivoda")]
        public int idProizvod { get; set; }

        [Required(ErrorMessage = "Molimo upisite naziv proizvoda")]
        [Display(Name ="Naziv proizvoda")]
        public string Naziv {  get; set; }

        [Required(ErrorMessage = "Molimo upisite cijenu proizvoda")]
        [Display(Name = "Cijena proizvoda")]
        [DataType(DataType.Currency)]
        public float Cijena { get; set; }

        [Required(ErrorMessage = "Molimo upisite naziv proizvodaca")]
        [Display(Name = "Naziv proizvodaca")]
        public string Proizvodac { get; set; }

        [Required(ErrorMessage = "Molimo upisite kategoriju proizvoda")]
        [Display(Name = "Naziv kategorije prozvoda")]
        public string Kategorija { get; set; }

        [Display(Name = "Opis proizvoda")]
        public string Opis { get; set; }
    }
}