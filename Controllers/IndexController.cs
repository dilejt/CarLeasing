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
            ViewBag.marka = new SelectList(db.marka.Where(m => db.samochod.Select(s => s.parametr.model.marka_id_marka).Contains(m.id_marka)), "id_marka", "nazwa");
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
            return Json(new SelectList(db.model.Where(m => (m.marka_id_marka == id)), "id_model", "nazwa"));
        }

        [HttpPost]
        public RedirectToRouteResult GetCarsByModel()
        {
            int id = Convert.ToInt32(Request.Form["model"]);
            return RedirectToAction("Index", "Car", new { id = id });
        }

      
    }
}