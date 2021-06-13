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
            return View(uzytkownik);
        }

        // POST: Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                uzytkownik user = db.uzytkownik.Find(Session["UserId"]);
                user.email = uzytkownik.email;
                user.imie = uzytkownik.imie;
                user.nazwisko = uzytkownik.nazwisko;
                user.telefon = uzytkownik.telefon;
                user.haslo = uzytkownik.haslo;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
