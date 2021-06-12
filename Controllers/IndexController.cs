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
    public class IndexController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();

        // GET: Index
        public ActionResult Index()
        {
            ViewBag.model = new SelectList(db.model, "id_model", "nazwa");
            ViewBag.marka = new SelectList(db.marka, "id_marka", "nazwa");
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

        public JsonResult GetModelList(int id)
        {
            return Json(new SelectList(db.model.Where(empt => (empt.marka_id_marka == id)), "id_model", "nazwa"));
        }

        // GET: Index/Details/5
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