using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarLeasing.Models;

namespace CarLeasing.Controllers
{
    public class CarController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: Car/Index/5
        public ActionResult Index(int? id)
        {
            var samochod = db.samochod.Include(s => s.kolor)
                .Include(s => s.parametr)
                .Where(s => s.parametr.model_id_model == id)
                .Where(s => db.zamowienie.Where(z => z.status_id_status == 2).Select(z => z.samochod_id_samochod).Contains(s.id_samochod) || !db.zamowienie.Select(z => z.samochod_id_samochod).Contains(s.id_samochod));

            return View(samochod.ToList());
        }

        // GET: Car/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            samochod samochod = db.samochod.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_firma = new SelectList(db.firma.Where(f => db.opona.Where(o => o.firma_id_firma == f.id_firma).Any()).OrderBy(n => n.nazwa), "id_firma", "nazwa");
            ViewBag.id_opona = new SelectList(db.opona, "id_opona", "nazwa");
            ViewBag.id_sezon = new SelectList(db.sezon, "id_sezon", "nazwa");
            ViewBag.id_okres = new SelectList(db.okres, "id_okres", "ilosc");
            ViewBag.id_platnosc = new SelectList(db.platnosc, "id_platnosc", "ilosc");
            ViewBag.id_dystans = new SelectList(db.dystans, "id_dystans", "ilosc");
            return View(samochod);
        }

        public JsonResult GetModelList(int id)
        {
            var result = new { Result = new SelectList(db.opona.Where(item => (item.firma_id_firma == id)), "id_opona", "nazwa"), ID = id };
            return Json(result);
        }
        public JsonResult GetSezonList(int id)
        {
            opona opona = db.opona.Find(id);
            return Json(new SelectList(db.sezon.Where(item => (item.id_sezon == opona.sezon_id_sezon)), "id_sezon", "nazwa"));
        }

        // POST: Car/Create
        [UserAuthentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(MakeOrder order)
        {
            samochod samochod = db.samochod.Find(order.id_samochod);
            samochod.parametr.opona_id_opona = order.id_opona;
            opona opona = db.opona.Find(order.id_opona);
            opona.sezon_id_sezon = order.id_sezon;
            opona.firma_id_firma = order.id_firma;
            if (ModelState.IsValid)
            {
                zamowienie zamowienie = new zamowienie();
                zamowienie.dystans_id_dystans = order.id_dystans;
                zamowienie.okres_id_okres = order.id_okres;
                zamowienie.platnosc_id_platnosc = order.id_platnosc;
                zamowienie.samochod_id_samochod = samochod.id_samochod;
                zamowienie.status_id_status = 1; //status 1==aktywne
                zamowienie.uzytkownik_id_uzytkownik = (int)Session["UserId"];
                zamowienie.data_zlozenia = DateTime.Now;
                zamowienie.koszt = samochod.cena + db.dystans.Find(order.id_dystans).ilosc + Math.Round(db.okres.Find(order.id_okres).ilosc / db.platnosc.Find(order.id_platnosc).ilosc / 100 * samochod.cena, 2);
                db.zamowienie.Add(zamowienie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            samochod samochod = db.samochod.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            ViewBag.kolor_id_kolor = new SelectList(db.kolor, "id_kolor", "nazwa", samochod.kolor_id_kolor);
            ViewBag.parametr_id_parametr = new SelectList(db.parametr, "id_parametr", "opis", samochod.parametr_id_parametr);
            return View(samochod);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_samochod,cena,kolor_id_kolor,parametr_id_parametr")] samochod samochod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(samochod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kolor_id_kolor = new SelectList(db.kolor, "id_kolor", "nazwa", samochod.kolor_id_kolor);
            ViewBag.parametr_id_parametr = new SelectList(db.parametr, "id_parametr", "opis", samochod.parametr_id_parametr);
            return View(samochod);
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            samochod samochod = db.samochod.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            return View(samochod);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            samochod samochod = db.samochod.Find(id);
            db.samochod.Remove(samochod);
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
