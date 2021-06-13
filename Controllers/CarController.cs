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
    public class CarController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: Car
        public ActionResult Index()
        {
            var samochod = db.samochod.Include(s => s.kolor).Include(s => s.parametr);
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
            return View(samochod);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            ViewBag.kolor_id_kolor = new SelectList(db.kolor, "id_kolor", "nazwa");
            ViewBag.parametr_id_parametr = new SelectList(db.parametr, "id_parametr", "opis");
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_samochod,cena,kolor_id_kolor,parametr_id_parametr")] samochod samochod)
        {
            if (ModelState.IsValid)
            {
                db.samochod.Add(samochod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kolor_id_kolor = new SelectList(db.kolor, "id_kolor", "nazwa", samochod.kolor_id_kolor);
            ViewBag.parametr_id_parametr = new SelectList(db.parametr, "id_parametr", "opis", samochod.parametr_id_parametr);
            return View(samochod);
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
