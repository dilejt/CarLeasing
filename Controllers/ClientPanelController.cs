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
    public class ClientPanelController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: ClientPanel
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["UserId"]);
            var zamowienie = db.zamowienie
                .Include(z => z.dystans)
                .Include(z => z.okres)
                .Include(z => z.platnosc)
                .Include(z => z.samochod)
                .Include(z => z.uzytkownik)
                .Include(z => z.status)
                .Where(z => z.uzytkownik_id_uzytkownik == id);
            return View(zamowienie.ToList());
        }

        // GET: ClientPanel/Details/5
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

        // GET: ClientPanel/Edit/5
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
            EditClientPanel order = new EditClientPanel();
            order.id_zamowienie = (int)id;
            order.kolor_id_kolor = zamowienie.samochod.kolor_id_kolor;
            order.platnosc_id_platnosc = zamowienie.platnosc_id_platnosc;
            order.dystans_id_dystans = zamowienie.dystans_id_dystans;
            order.okres_id_okres = zamowienie.okres_id_okres;
            ViewBag.dystans_id_dystans = new SelectList(db.dystans, "id_dystans", "ilosc", zamowienie.dystans_id_dystans);
            ViewBag.okres_id_okres = new SelectList(db.okres, "id_okres", "ilosc", zamowienie.okres_id_okres);
            ViewBag.platnosc_id_platnosc = new SelectList(db.platnosc, "id_platnosc", "ilosc", zamowienie.platnosc_id_platnosc);
            ViewBag.kolor_id_kolor = new SelectList(db.kolor, "id_kolor", "nazwa", zamowienie.samochod.kolor_id_kolor);
            return View(order);
        }

        // POST: ClientPanel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditClientPanel zamowienie)
        {
            if (ModelState.IsValid)
            {
                zamowienie order = db.zamowienie.Find(zamowienie.id_zamowienie);
                order.okres_id_okres = zamowienie.okres_id_okres;
                order.platnosc_id_platnosc = zamowienie.platnosc_id_platnosc;
                order.dystans_id_dystans = zamowienie.dystans_id_dystans;
                samochod car = db.samochod.Find(order.samochod_id_samochod);
                car.kolor_id_kolor = zamowienie.kolor_id_kolor;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dystans_id_dystans = new SelectList(db.dystans, "id_dystans", "ilosc", zamowienie.dystans_id_dystans);
            ViewBag.okres_id_okres = new SelectList(db.okres, "id_okres", "ilosc", zamowienie.okres_id_okres);
            ViewBag.platnosc_id_platnosc = new SelectList(db.platnosc, "id_platnosc", "ilosc", zamowienie.platnosc_id_platnosc);
            ViewBag.kolor_id_kolor = new SelectList(db.kolor, "id_kolor", "nazwa", zamowienie.kolor_id_kolor);
            return View(zamowienie);
        }

        // GET: ClientPanel/Delete/5
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

        // POST: ClientPanel/Delete/5
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
