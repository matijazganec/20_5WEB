using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webshop_projekt.Data;
using webshop_projekt.Misc;
using webshop_projekt.Models;
using webshop_projekt.Reports;

namespace webshop_projekt.Controllers
{
    [Authorize(Roles = OvlastiKorisnik.Administrator)]
    public class KosaricaItemsController : Controller
    {
        KosaricaDbContext db = new KosaricaDbContext();
        ProizvodDbContext proizvodi = new ProizvodDbContext();

        [AllowAnonymous]
        // GET: KosaricaItems
        public ActionResult Index()
        {
            return View(db.KosaricaItems.ToList());
        }

        // GET: KosaricaItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KosaricaItem kosaricaItem = db.KosaricaItems.FirstOrDefault(x => x.Id == id);
            if (kosaricaItem == null)
            {
                return HttpNotFound();
            }
            return View(kosaricaItem);
        }

        [AllowAnonymous]
        public ActionResult DodajUKosaricu(int id)
        {
            /*if (id == null) { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/

            Proizvod proizvod = proizvodi.Proizvods.Find(id);

            if (proizvod == null)
            {
                return HttpNotFound();
            }

            KosaricaItem kosaricaItem = new KosaricaItem { Id = id, Cijena = (decimal)proizvod.Cijena, Naziv = proizvod.Naziv, Kolicina = 1 };
            db.KosaricaItems.Add(kosaricaItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: KosaricaItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KosaricaItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Cijena")] KosaricaItem kosaricaItem)
        {
            if (ModelState.IsValid)
            {
                db.KosaricaItems.Add(kosaricaItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kosaricaItem);
        }

        [AllowAnonymous]
        // GET: KosaricaItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KosaricaItem kosaricaItem = db.KosaricaItems.FirstOrDefault(x => x.Id == id);
            if (kosaricaItem == null)
            {
                return HttpNotFound();
            }
            return View(kosaricaItem);
        }

        [AllowAnonymous]
        // POST: KosaricaItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KosaricaItem kosaricaItem = db.KosaricaItems.FirstOrDefault(x => x.Id == id);
            db.KosaricaItems.Remove(kosaricaItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AllowAnonymous]
        public ActionResult IspisRacuna(string Naziv, int? Kolicina, decimal? Cijena)
        {
            var stvari = db.KosaricaItems.ToList();

            if(!String.IsNullOrEmpty(Naziv))
            {
                stvari = stvari.Where(x => x.Naziv.ToUpper().Contains(Naziv.ToUpper())).ToList();
            }
            if (!String.IsNullOrEmpty(Naziv))
            {
                stvari = stvari.Where(x => x.Kolicina == Kolicina).ToList();
            }
            if (!String.IsNullOrEmpty(Naziv))
            {
                stvari = stvari.Where(x => x.Cijena == Cijena).ToList();
            }
            KosaricasReport kosaricasReport = new KosaricasReport();
            kosaricasReport.ListaKosarica(stvari);

            return File(kosaricasReport.Podaci, System.Net.Mime.MediaTypeNames.Application.Pdf, "RacunKosarica.pdf");
        }

        [AllowAnonymous]
        public ActionResult ObrisiKosaricu()
        {
            var listaKosarica = db.KosaricaItems.ToList();

            foreach (var kosaricaItem in listaKosarica)
            {
                db.KosaricaItems.Remove(kosaricaItem);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}

