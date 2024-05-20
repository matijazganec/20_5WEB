using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using webshop_projekt.Data;
using webshop_projekt.Misc;
using webshop_projekt.Models;

namespace webshop_projekt.Controllers
{
    [Authorize(Roles = OvlastiKorisnik.Administrator)]
    public class KorisniksController : Controller
    {
        
        KorisnikDbContext db = new KorisnikDbContext();

        // GET: Korisniks
        public ActionResult Index()
        {
            var korisniks = db.Korisniks.Include(k => k.Ovlast);
            return View(korisniks.ToList());
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Prijava(string returnUrl) { 
            KorisnikPrijava model = new KorisnikPrijava();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Prijava(KorisnikPrijava model, string returnUrl) {
            if (ModelState.IsValid) {
                var korisnikBaza = db.Korisniks.FirstOrDefault(x => x.KorisnickoIme == model.KorisnickoIme);
                if (korisnikBaza != null) {
                    var passwordOK = korisnikBaza.Lozinka == Misc.PasswordHelper.IzracunajHash(model.Lozinka);
                    if (passwordOK) {
                        LogiraniKorisnik prijavljeniKorisnik = new LogiraniKorisnik(korisnikBaza);
                        LogiraniKorisnikSerializeModel serializeModel = new LogiraniKorisnikSerializeModel();
                        serializeModel.CopyFromUser(prijavljeniKorisnik);
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string korisnickiPodaci = serializer.Serialize(serializeModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, prijavljeniKorisnik.Identity.Name, DateTime.Now, DateTime.Now.AddDays(1), false, korisnickiPodaci);
                        string tickedEncrypted = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, tickedEncrypted);
                        Response.Cookies.Add(cookie);
                        if(!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Neispravno korisnicko ime ili lozinka");
            return View(model);
        }
        [OverrideAuthorization]
        [Authorize]
        public ActionResult Odjava() {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        // GET: Korisniks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnik korisnik = db.Korisniks.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        // GET: Korisniks/Create
        public ActionResult Create()
        {
            ViewBag.SifraOvlasti = new SelectList(db.Ovlasts, "Sifra", "Naziv");
            return View();
        }

        // POST: Korisniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KorisnickoIme,Email,Lozinka,Prezime,Ime,SifraOvlasti")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                db.Korisniks.Add(korisnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SifraOvlasti = new SelectList(db.Ovlasts, "Sifra", "Naziv", korisnik.SifraOvlasti);
            return View(korisnik);
        }

        // GET: Korisniks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnik korisnik = db.Korisniks.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            ViewBag.SifraOvlasti = new SelectList(db.Ovlasts, "Sifra", "Naziv", korisnik.SifraOvlasti);
            return View(korisnik);
        }

        // POST: Korisniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KorisnickoIme,Email,Lozinka,Prezime,Ime,SifraOvlasti")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(korisnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SifraOvlasti = new SelectList(db.Ovlasts, "Sifra", "Naziv", korisnik.SifraOvlasti);
            return View(korisnik);
        }

        // GET: Korisniks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Korisnik korisnik = db.Korisniks.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        // POST: Korisniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Korisnik korisnik = db.Korisniks.Find(id);
            db.Korisniks.Remove(korisnik);
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
