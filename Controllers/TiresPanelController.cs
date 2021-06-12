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
    [AdminAuthentication]
    public class TiresPanelController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: TiresPanel
        public ActionResult Index()
        {
            var opona = db.opona.Include(o => o.firma).Include(o => o.sezon);
            return View(opona.ToList());
        }

        // GET: TiresPanel/Create
        public ActionResult Create()
        {
            ViewBag.firma_id_firma = new SelectList(db.firma, "id_firma", "nazwa");
            ViewBag.sezon_id_sezon = new SelectList(db.sezon, "id_sezon", "nazwa");
            return View();
        }

        // POST: TiresPanel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_opona,nazwa,firma_id_firma,sezon_id_sezon")] opona opona)
        {
            if (ModelState.IsValid)
            {
                db.opona.Add(opona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.firma_id_firma = new SelectList(db.firma, "id_firma", "nazwa", opona.firma_id_firma);
            ViewBag.sezon_id_sezon = new SelectList(db.sezon, "id_sezon", "nazwa", opona.sezon_id_sezon);
            return View(opona);
        }

        // GET: TiresPanel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            opona opona = db.opona.Find(id);
            if (opona == null)
            {
                return HttpNotFound();
            }
            ViewBag.firma_id_firma = new SelectList(db.firma, "id_firma", "nazwa", opona.firma_id_firma);
            ViewBag.sezon_id_sezon = new SelectList(db.sezon, "id_sezon", "nazwa", opona.sezon_id_sezon);
            return View(opona);
        }

        // POST: TiresPanel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_opona,nazwa,firma_id_firma,sezon_id_sezon")] opona opona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.firma_id_firma = new SelectList(db.firma, "id_firma", "nazwa", opona.firma_id_firma);
            ViewBag.sezon_id_sezon = new SelectList(db.sezon, "id_sezon", "nazwa", opona.sezon_id_sezon);
            return View(opona);
        }

        // GET: TiresPanel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            opona opona = db.opona.Find(id);
            if (opona == null)
            {
                return HttpNotFound();
            }
            return View(opona);
        }

        // POST: TiresPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            opona opona = db.opona.Find(id);
            db.opona.Remove(opona);
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
