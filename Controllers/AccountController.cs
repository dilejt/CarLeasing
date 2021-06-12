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
    [LoginAuthentication]
    public class AccountController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: Account/Index
        public ActionResult Index()
        {
            uzytkownik uzytkownik = db.uzytkownik.Find(Session["UserId"]);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // GET: Account/Edit
        public ActionResult Edit()
        {
            uzytkownik uzytkownik = db.uzytkownik.Find(Session["UserId"]);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            ViewBag.rola_id_rola = new SelectList(db.rola, "id_rola", "nazwa", uzytkownik.rola_id_rola);
            return View(uzytkownik);
        }

        // POST: Account/Edit
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

        // GET: Account/Delete
        public ActionResult Delete()
        {
            uzytkownik uzytkownik = db.uzytkownik.Find(Session["UserId"]);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            uzytkownik uzytkownik = db.uzytkownik.Find(Session["UserId"]);
            db.uzytkownik.Remove(uzytkownik);
            db.SaveChanges();
            Session.Clear();
            return RedirectToAction("Index","Index");
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
