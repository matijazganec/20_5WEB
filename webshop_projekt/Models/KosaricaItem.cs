using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webshop_projekt.Models
{
    [Table("kosarica")]
    public class KosaricaItem
    {
        [Display(Name ="Id")]
        [Required]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naziv")]
        [Required]
        public string Naziv { get; set; }

        [Display(Name = "Cijena")]
        [Required]
        public decimal Cijena { get; set; }

        [Display(Name = "Kolicina")]
        [Required]
        public int Kolicina { get; set; }
    }
    public class Kosarica
    {
        public List<KosaricaItem> Items { get; set; }

        public Kosarica()
        {
            Items = new List<KosaricaItem>();
        }

        public void AddItem(KosaricaItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(int itemId)
        {
            var itemToRemove = Items.FirstOrDefault(i => i.Id == itemId);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
            }
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Cijena;
            }
            return total;
        }
    }
}