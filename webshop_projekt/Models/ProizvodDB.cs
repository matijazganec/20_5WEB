using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace webshop_projekt.Models
{
    public class ProizvodDB
    {
        private List<Proizvod> proizvods = new List<Proizvod>();

        public ProizvodDB() {
            proizvods.Add(new Proizvod()
            {
                idProizvod = 1,
                Naziv = "NVidia RTX 4060Ti",
                Cijena = 650,
                Proizvodac = "NVidia",
                Kategorija = "Grafičke kartice",
                Opis = "Neki opis"
            });
            proizvods.Add(new Proizvod()
            {
                idProizvod = 2,
                Naziv = "Intel i7",
                Cijena = 450,
                Proizvodac = "INTEL",
                Kategorija = "Procesori",
                Opis = "Neki opis"
            });
            proizvods.Add(new Proizvod()
            {
                idProizvod = 3,
                Naziv = "AMD R7 3700x",
                Cijena = 350,
                Proizvodac = "AMD",
                Kategorija = "Procesori",
                Opis = "Neki opis"
            });
            proizvods.Add(new Proizvod()
            {
                idProizvod = 4,
                Naziv = "Samsung 1TB SSD",
                Cijena = 100,
                Proizvodac = "Samsung",
                Kategorija = "Diskovi",
                Opis = "Neki opis"
            });
        }
        public List<Proizvod> VratiListu() { return proizvods; }

    }
}