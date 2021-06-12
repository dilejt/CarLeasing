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
    public class ManufacturersController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: Manufacturers
        public ActionResult Index()
        {
            return View(db.marka.ToList());
        }

        // GET: Manufacturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marka marka = db.marka.Find(id);
            if (marka == null)
            {
                return HttpNotFound();
            }
            return View(marka);
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_marka,nazwa,opis,logo")] marka marka)
        {
            if (ModelState.IsValid)
            {
                db.marka.Add(marka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marka);
        }

        // GET: Manufacturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marka marka = db.marka.Find(id);
            if (marka == null)
            {
                return HttpNotFound();
            }
            return View(marka);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_marka,nazwa,opis,logo")] marka marka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marka);
        }

        // GET: Manufacturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marka marka = db.marka.Find(id);
            if (marka == null)
            {
                return HttpNotFound();
            }
            return View(marka);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            marka marka = db.marka.Find(id);
            db.marka.Remove(marka);
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
