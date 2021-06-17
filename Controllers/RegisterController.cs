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
            if (ModelState.IsValid)
            {
                uzytkownik.rola_id_rola = 1;
                db.uzytkownik.Add(uzytkownik);
                db.SaveChanges();
                return RedirectToAction("Index", "Index");
            }
            ModelState.Clear();
            return View(uzytkownik);
        }
        public ActionResult CheckTelExist(string telefon)
        {
            bool UserExist = db.uzytkownik.Where(u => u.telefon.Equals(telefon)).Any();
            if (UserExist == true)
            {
                return Content("false");
            }
            else
            {
                return Content("true");
            }
        }

        public ActionResult CheckEmailExist(string email)
        {
            bool UserExist = db.uzytkownik.Where(u => u.email.Equals(email)).Any();
            if (UserExist == true)
            {
                return Content("false");
            }
            else
            {
                return Content("true");
            }
        }
    }
}
