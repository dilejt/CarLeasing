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
            ViewBag.uzytkownik_id_uzytkownik = new SelectList(db.uzytkownik, "id_uzytkownik", "imie", zamowienie.uzytkownik_id_uzytkownik);
            ViewBag.status_id_status = new SelectList(db.status, "id_status", "nazwa", zamowienie.status_id_status);
            return View(zamowienie);
        }

        // POST: OrdersPanel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(zamowienie zamowienie)
        {
            zamowienie oldOrder = db.zamowienie.Find(zamowienie.id_zamowienie);
            oldOrder.status_id_status = zamowienie.status_id_status;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
