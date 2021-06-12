using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarLeasing.Models;

namespace CarLeasing.Controllers
{
    [AdminAuthentication]
    public class ManufacturersPanelController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: ManufacturersPanel
        public ActionResult Index()
        {
            return View(db.marka.ToList());
        }

        // GET: ManufacturersPanel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManufacturersPanel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(marka marka)
        {
            string extensionName = Path.GetExtension(marka.MarkaFile.FileName);
            string fileName = marka.nazwa + extensionName;
            marka.logo = "/Content/Images/Marka/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Content/Images/Marka/"), fileName);
            marka.MarkaFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                db.marka.Add(marka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marka);
        }

        // GET: ManufacturersPanel/Edit/5
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

        // POST: ManufacturersPanel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(marka marka)
        {
            marka oldMarka = db.marka.Find(marka.id_marka);
            oldMarka.nazwa = marka.nazwa;
            oldMarka.opis = marka.opis;
            if (marka.MarkaFile != null)
            {
                string fullPath = Request.MapPath(oldMarka.logo);
                string fileName = marka.nazwa + Path.GetExtension(marka.MarkaFile.FileName);
                oldMarka.logo = "/Content/Images/Marka/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Content/Images/Marka/"), fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    marka.MarkaFile.SaveAs(fileName);
                }
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marka);
        }

        // GET: ManufacturersPanel/Delete/5
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

        // POST: ManufacturersPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            marka marka = db.marka.Find(id);
            foreach (model model in db.model.Where(z => z.marka_id_marka == id))
            {
                foreach (silnik silnik in db.silnik.Where(z => z.id_silnik == model.silnik_id_silnik))
                {
                    db.silnik.Remove(silnik);
                }
                foreach (parametr parametr in db.parametr.Where(z => z.model_id_model == model.id_model))
                {
                    foreach (samochod samochod in db.samochod.Where(z => z.parametr_id_parametr == parametr.id_parametr))
                    {
                        foreach (zdjecie zdjecie in db.zdjecie.Where(z => z.samochod_id_samochod == samochod.id_samochod))
                        {
                            db.zdjecie.Remove(zdjecie);
                        }
                        foreach (zamowienie zamowienie in db.zamowienie.Where(z => z.samochod_id_samochod == samochod.id_samochod))
                        {
                            db.zamowienie.Remove(zamowienie);
                        }
                        db.samochod.Remove(samochod);
                    }
                    db.parametr.Remove(parametr);
                }
                db.model.Remove(model);
            }
            db.marka.Remove(marka);
            string fullPath = Request.MapPath(marka.logo);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
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
