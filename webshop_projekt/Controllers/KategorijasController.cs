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

namespace webshop_projekt.Controllers
{
    [Authorize(Roles = OvlastiKorisnik.Administrator)]
    public class KategorijasController : Controller
    {
        KategorijaDbContext db = new KategorijaDbContext();

        // GET: Kategorijas
        public ActionResult Index()
        {
            return View(db.Kategorijas.ToList());
        }

        // GET: Kategorijas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorijas.FirstOrDefault(x => x.KategorijaId == id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }

        // GET: Kategorijas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kategorijas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                db.Kategorijas.Add(kategorija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategorija);
        }

        // GET: Kategorijas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorijas.FirstOrDefault(x => x.KategorijaId == id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }

        // POST: Kategorijas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategorijaId,KategorijaName")] Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategorija).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategorija);
        }

        // GET: Kategorijas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorijas.FirstOrDefault(x => x.KategorijaId == id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }

        // POST: Kategorijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategorija kategorija = db.Kategorijas.FirstOrDefault(x => x.KategorijaId == id);
            db.Kategorijas.Remove(kategorija);
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
    }
}
