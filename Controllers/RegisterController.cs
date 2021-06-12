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

        // GET: Index
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Index");
            }
            else
            {
                return View();
            }
        }

        // POST: Register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(/*[Bind(Include = "id_uzytkownik,imie,nazwisko,email,telefon,haslo,rola_id_rola")]*/ uzytkownik _uzytkownik)
        {
            var check = db.uzytkownik.FirstOrDefault(s => s.email == _uzytkownik.email);
            if (check == null)
            {
                if (ModelState.IsValid)
                {
                    _uzytkownik.rola_id_rola = 1;
                    db.uzytkownik.Add(_uzytkownik);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                ViewBag.errorEmail = "Podany email jest zajęty!";
            }
            ModelState.Clear();
            return View(_uzytkownik);
        }
    }
}
