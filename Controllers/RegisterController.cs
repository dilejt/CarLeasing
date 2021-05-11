using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarLeasing.Models;

namespace CarLeasing.Controllers
{
    public class RegisterController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: Register
        public ActionResult Index()
        {
            var uzytkownik = db.uzytkownik.Include(u => u.rola);
            return View(uzytkownik.ToList());
        }

        // GET: Register/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uzytkownik uzytkownik = db.uzytkownik.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // GET: Register/Create
        public ActionResult Create()
        {
            ViewBag.rola_id_rola = new SelectList(db.rola, "id_rola", "nazwa");
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_uzytkownik,imie,nazwisko,email,telefon,haslo,rola_id_rola")] uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                db.uzytkownik.Add(uzytkownik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rola_id_rola = new SelectList(db.rola, "id_rola", "nazwa", uzytkownik.rola_id_rola);
            return View(uzytkownik);
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uzytkownik uzytkownik = db.uzytkownik.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            ViewBag.rola_id_rola = new SelectList(db.rola, "id_rola", "nazwa", uzytkownik.rola_id_rola);
            return View(uzytkownik);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_uzytkownik,imie,nazwisko,email,telefon,haslo,rola_id_rola")] uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uzytkownik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rola_id_rola = new SelectList(db.rola, "id_rola", "nazwa", uzytkownik.rola_id_rola);
            return View(uzytkownik);
        }

        // GET: Register/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uzytkownik uzytkownik = db.uzytkownik.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            uzytkownik uzytkownik = db.uzytkownik.Find(id);
            db.uzytkownik.Remove(uzytkownik);
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
