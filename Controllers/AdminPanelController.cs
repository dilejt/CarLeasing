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
    public class AdminPanelController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: AdminPanel
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminPanel/Details/5
        public ActionResult Details()
        {
            var samochod = db.samochod.Include(s => s.kolor)
                .Include(s => s.parametr)
                .Include(s => s.parametr.opona)
                .Include(s => s.parametr.opona.firma)
                .Include(s => s.parametr.opona.sezon)
                .Include(s => s.parametr.skrzynia)
                .Include(s => s.parametr.siedzenie)
                .Include(s => s.parametr.model)
                .Include(s => s.parametr.model.silnik)
                .Include(s => s.parametr.model.silnik.paliwo);
            return View(samochod.ToList());
        }

        // GET: AdminPanel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminPanel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminPanel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminPanel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminPanel/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
