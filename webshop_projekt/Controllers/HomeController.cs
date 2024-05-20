using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webshop_projekt.Data;
using webshop_projekt.Misc;
using webshop_projekt.Models;

namespace webshop_projekt.Controllers
{
    [Authorize(Roles =OvlastiKorisnik.Administrator + "," + OvlastiKorisnik.Moderator)]
    public class HomeController : Controller
    {
        ProizvodDbContext db = new ProizvodDbContext();
        KategorijaDbContext kategorija = new KategorijaDbContext();
        ProizvodDbContext proizvod = new ProizvodDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Model = new Proizvod();
            ViewBag.Kategorije = kategorija.Kategorijas.ToList();
            ViewBag.Proizvodi = proizvod.Proizvods.ToList();
            return View(db.Proizvods.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}