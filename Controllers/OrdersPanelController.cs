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
    public class OrdersPanelController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: OrdersPanel
        public ActionResult Index()
        {
            var zamowienie = db.zamowienie.Include(z => z.dystans).Include(z => z.okres).Include(z => z.platnosc).Include(z => z.samochod).Include(z => z.uzytkownik).Include(z => z.status);
            return View(zamowienie.ToList());
        }

        // GET: OrdersPanel/Details/5
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

        // GET: OrdersPanel/Create
        public ActionResult Create()
        {
            ViewBag.dystans_id_dystans = new SelectList(db.dystans, "id_dystans", "id_dystans");
            ViewBag.okres_id_okres = new SelectList(db.okres, "id_okres", "id_okres");
            ViewBag.platnosc_id_platnosc = new SelectList(db.platnosc, "id_platnosc", "id_platnosc");
            ViewBag.samochod_id_samochod = new SelectList(db.samochod, "id_samochod", "id_samochod");
            ViewBag.uzytkownik_id_uzytkownik = new SelectList(db.uzytkownik, "id_uzytkownik", "imie");
            ViewBag.status_id_status = new SelectList(db.status, "id_status", "nazwa");
            return View();
        }

        // POST: OrdersPanel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_zamowienie,koszt,okres_id_okres,platnosc_id_platnosc,dystans_id_dystans,samochod_id_samochod,data_zlozenia,uzytkownik_id_uzytkownik,status_id_status")] zamowienie zamowienie)
        {
            if (ModelState.IsValid)
            {
                db.zamowienie.Add(zamowienie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dystans_id_dystans = new SelectList(db.dystans, "id_dystans", "id_dystans", zamowienie.dystans_id_dystans);
            ViewBag.okres_id_okres = new SelectList(db.okres, "id_okres", "id_okres", zamowienie.okres_id_okres);
            ViewBag.platnosc_id_platnosc = new SelectList(db.platnosc, "id_platnosc", "id_platnosc", zamowienie.platnosc_id_platnosc);
            ViewBag.samochod_id_samochod = new SelectList(db.samochod, "id_samochod", "id_samochod", zamowienie.samochod_id_samochod);
            ViewBag.uzytkownik_id_uzytkownik = new SelectList(db.uzytkownik, "id_uzytkownik", "imie", zamowienie.uzytkownik_id_uzytkownik);
            ViewBag.status_id_status = new SelectList(db.status, "id_status", "nazwa", zamowienie.status_id_status);
            return View(zamowienie);
        }

        // GET: OrdersPanel/Edit/5
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
            ViewBag.dystans_id_dystans = new SelectList(db.dystans, "id_dystans", "id_dystans", zamowienie.dystans_id_dystans);
            ViewBag.okres_id_okres = new SelectList(db.okres, "id_okres", "id_okres", zamowienie.okres_id_okres);
            ViewBag.platnosc_id_platnosc = new SelectList(db.platnosc, "id_platnosc", "id_platnosc", zamowienie.platnosc_id_platnosc);
            ViewBag.samochod_id_samochod = new SelectList(db.samochod, "id_samochod", "id_samochod", zamowienie.samochod_id_samochod);
            ViewBag.uzytkownik_id_uzytkownik = new SelectList(db.uzytkownik, "id_uzytkownik", "imie", zamowienie.uzytkownik_id_uzytkownik);
            ViewBag.status_id_status = new SelectList(db.status, "id_status", "nazwa", zamowienie.status_id_status);
            return View(zamowienie);
        }

        // POST: OrdersPanel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_zamowienie,koszt,okres_id_okres,platnosc_id_platnosc,dystans_id_dystans,samochod_id_samochod,data_zlozenia,uzytkownik_id_uzytkownik,status_id_status")] zamowienie zamowienie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zamowienie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dystans_id_dystans = new SelectList(db.dystans, "id_dystans", "id_dystans", zamowienie.dystans_id_dystans);
            ViewBag.okres_id_okres = new SelectList(db.okres, "id_okres", "id_okres", zamowienie.okres_id_okres);
            ViewBag.platnosc_id_platnosc = new SelectList(db.platnosc, "id_platnosc", "id_platnosc", zamowienie.platnosc_id_platnosc);
            ViewBag.samochod_id_samochod = new SelectList(db.samochod, "id_samochod", "id_samochod", zamowienie.samochod_id_samochod);
            ViewBag.uzytkownik_id_uzytkownik = new SelectList(db.uzytkownik, "id_uzytkownik", "imie", zamowienie.uzytkownik_id_uzytkownik);
            ViewBag.status_id_status = new SelectList(db.status, "id_status", "nazwa", zamowienie.status_id_status);
            return View(zamowienie);
        }

        // GET: OrdersPanel/Delete/5
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

        // POST: OrdersPanel/Delete/5
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
