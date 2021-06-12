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
    [UserAuthentication]
    public class UserPanelController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: UserPanel
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var zamowienie = db.zamowienie
                .Include(z => z.dystans)
                .Include(z => z.okres)
                .Include(z => z.platnosc)
                .Include(z => z.samochod)
                .Include(z => z.uzytkownik)
                .Where(z => z.uzytkownik_id_uzytkownik == userId);
            return View(zamowienie.ToList());
        }

        // GET: UserPanel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zamowienie zamowienie = db.zamowienie.Find(id);
            if (zamowienie == null)
            {
                return HttpNotFound();
            }
            return View(zamowienie);
        }

        // GET: UserPanel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zamowienie zamowienie = db.zamowienie.Find(id);
            if (zamowienie == null)
            {
                return HttpNotFound();
            }
            ViewBag.dystans_id_dystans = new SelectList(db.dystans, "id_dystans", "ilosc", zamowienie.dystans_id_dystans);
            ViewBag.okres_id_okres = new SelectList(db.okres, "id_okres", "ilosc", zamowienie.okres_id_okres);
            ViewBag.platnosc_id_platnosc = new SelectList(db.platnosc, "id_platnosc", "ilosc", zamowienie.platnosc_id_platnosc);
            ViewBag.cena = zamowienie.samochod.cena + zamowienie.dystans.ilosc + zamowienie.okres.ilosc + zamowienie.platnosc.ilosc;
            return View(zamowienie);
        }

        // POST: UserPanel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_zamowienie,koszt,okres_id_okres,platnosc_id_platnosc,dystans_id_dystans,samochod_id_samochod,data_zlozenia,uzytkownik_id_uzytkownik")] zamowienie zamowienie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zamowienie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dystans_id_dystans = new SelectList(db.dystans, "id_dystans", "ilosc", zamowienie.dystans_id_dystans);
            ViewBag.okres_id_okres = new SelectList(db.okres, "id_okres", "ilosc", zamowienie.okres_id_okres);
            ViewBag.platnosc_id_platnosc = new SelectList(db.platnosc, "id_platnosc", "ilosc", zamowienie.platnosc_id_platnosc);
            ViewBag.cena = zamowienie.samochod.cena + zamowienie.dystans.ilosc + zamowienie.okres.ilosc + zamowienie.platnosc.ilosc;
            return View(zamowienie);
        }

        // GET: UserPanel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zamowienie zamowienie = db.zamowienie.Find(id);
            if (zamowienie == null)
            {
                return HttpNotFound();
            }
            return View(zamowienie);
        }

        // POST: UserPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            zamowienie zamowienie = db.zamowienie.Find(id);
            db.zamowienie.Remove(zamowienie);
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
