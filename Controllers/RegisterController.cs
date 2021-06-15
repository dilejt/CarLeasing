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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(uzytkownik uzytkownik)
        {
            var check = db.uzytkownik.FirstOrDefault(s => s.email == uzytkownik.email);
            if (check == null)
            {
                if (ModelState.IsValid)
                {
                    uzytkownik.rola_id_rola = 1;
                    db.uzytkownik.Add(uzytkownik);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                ViewBag.errorEmail = "Podany email jest zajęty!";
            }
            ModelState.Clear();
            return View(uzytkownik);
        }
    }
}
