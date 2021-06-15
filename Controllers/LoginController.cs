using CarLeasing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CarLeasing.Controllers
{
    public class LoginController : Controller
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string email, string haslo)
        {
            if (ModelState.IsValid)
            {
                var data = db.uzytkownik.Where(s => s.email.Equals(email) && s.haslo.Equals(haslo)).ToList();
                if (data.Count() > 0)
                {
                    Session["FullName"] = data.FirstOrDefault().imie + " " + data.FirstOrDefault().nazwisko;
                    Session["Name"] = data.FirstOrDefault().imie;
                    Session["Surname"] = data.FirstOrDefault().nazwisko;
                    Session["Email"] = data.FirstOrDefault().email;
                    Session["Role"] = db.rola.Find(data.FirstOrDefault().rola_id_rola).nazwa;
                    Session["UserId"] = data.FirstOrDefault().id_uzytkownik;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Złe dane logowania!";
                }
            }
            ModelState.Clear();
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

    }
}