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
    public class ProizvodacsController : Controller
    {
        ProizvodacDbContext db = new ProizvodacDbContext();

        // GET: Proizvodacs
        public ActionResult Index()
        {
            return View(db.Proizvodacs.ToList());
        }

        // GET: Proizvodacs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvodac proizvodac = db.Proizvodacs.FirstOrDefault(x => x.idProizvodac == id);
            if (proizvodac == null)
            {
                return HttpNotFound();
            }
            return View(proizvodac);
        }

        // GET: Proizvodacs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proizvodacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proizvodac proizvodac)
        {
            if (ModelState.IsValid)
            {
                db.Proizvodacs.Add(proizvodac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proizvodac);
        }

        // GET: Proizvodacs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvodac proizvodac = db.Proizvodacs.FirstOrDefault(x => x.idProizvodac == id);
            if (proizvodac == null)
            {
                return HttpNotFound();
            }
            return View(proizvodac);
        }

        // POST: Proizvodacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProizvodac,ProizvodacName")] Proizvodac proizvodac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proizvodac).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proizvodac);
        }

        // GET: Proizvodacs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvodac proizvodac = db.Proizvodacs.FirstOrDefault(x => x.idProizvodac == id);
            if (proizvodac == null)
            {
                return HttpNotFound();
            }
            return View(proizvodac);
        }

        // POST: Proizvodacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proizvodac proizvodac = db.Proizvodacs.FirstOrDefault(x => x.idProizvodac == id);
            db.Proizvodacs.Remove(proizvodac);
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
