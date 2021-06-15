using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
    public class CarsPanelController : Controller
    {
        private CarLeasingEntities db = new CarLeasingEntities();
        static volatile public int tireId; //for selected option in select list
        // GET: CarsPanel
        public ActionResult Index()
        {
            var samochod = db.samochod.Include(s => s.kolor)
                .Include(s => s.parametr)
                .Include(s => s.parametr.model)
                .Include(s => s.parametr.model.marka);
            return View(samochod.ToList());
        }

        // GET: CarsPanel/Details/5
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

        // GET: CarsPanel/Create
        public ActionResult Create()
        {
            ViewBag.id_kolor = new SelectList(db.kolor, "id_kolor", "nazwa");
            ViewBag.id_skrzynia = new SelectList(db.skrzynia, "id_skrzynia", "nazwa");
            ViewBag.id_siedzenie = new SelectList(db.siedzenie, "id_siedzenie", "ilosc");
            ViewBag.id_nadwozie = new SelectList(db.nadwozie, "id_nadwozie", "nazwa");
            ViewBag.id_opona = new SelectList(db.opona, "id_opona", "nazwa");
            ViewBag.id_marka = new SelectList(db.marka, "id_marka", "nazwa");
            ViewBag.id_paliwo = new SelectList(db.paliwo, "id_paliwo", "nazwa");
            return View();
        }

        // POST: CarsPanel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCarAdminPanel data)
        {
            if (ModelState.IsValid)
            {
                samochod samochod = new samochod();
                parametr parametr = new parametr();
                zdjecie zdjecie = new zdjecie();
                model model = new model();
                silnik silnik = new silnik();
                samochod.kolor_id_kolor = data.id_kolor;
                samochod.parametr_id_parametr = parametr.id_parametr;
                samochod.cena = data.cena;
                zdjecie.samochod_id_samochod = samochod.id_samochod;
                parametr.opis = data.opis;
                parametr.rok_produkcji = data.rok_produkcji;
                parametr.przebieg = data.przebieg;
                parametr.pojemnosc_bagaznika = data.pojemnosc_bagaznika;
                parametr.skrzynia_id_skrzynia = data.id_skrzynia;
                parametr.model_id_model = model.id_model;
                parametr.siedzenie_id_siedzenie = data.id_siedzenie;
                parametr.nadwozie_id_nadwozie = data.id_nadwozie;
                parametr.opona_id_opona = data.id_opona;
                model.nazwa = data.model;
                model.marka_id_marka = data.id_marka;
                model.silnik_id_silnik = silnik.id_silnik;
                silnik.moc = data.moc;
                silnik.spalanie = data.spalanie;
                silnik.pojemnosc = data.pojemnosc;
                silnik.emisja = data.emisja;
                silnik.przyspieszenie = data.przyspieszenie;
                silnik.paliwo_id_paliwo = data.id_paliwo;
                db.silnik.Add(silnik);
                db.model.Add(model);
                db.parametr.Add(parametr);
                db.samochod.Add(samochod);
                db.zdjecie.Add(zdjecie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_kolor = new SelectList(db.kolor, "id_kolor", "nazwa", data.id_kolor);
            ViewBag.id_skrzynia = new SelectList(db.skrzynia, "id_skrzynia", "nazwa", data.id_skrzynia);
            ViewBag.id_siedzenie = new SelectList(db.siedzenie, "id_siedzenie", "ilosc", data.id_siedzenie);
            ViewBag.id_nadwozie = new SelectList(db.nadwozie, "id_nadwozie", "nazwa", data.id_nadwozie);
            ViewBag.id_opona = new SelectList(db.opona, "id_opona", "nazwa", data.id_opona);
            ViewBag.id_marka = new SelectList(db.marka, "id_marka", "nazwa", data.id_marka);
            ViewBag.id_paliwo = new SelectList(db.paliwo, "id_paliwo", "nazwa", data.id_paliwo);
            return View(data);
        }

        // GET: CarsPanel/Edit/5
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

            EditCarAdminPanel car = new EditCarAdminPanel();
            car.id_samochod = (int)id;
            car.cena = samochod.cena;
            car.rok_produkcji = samochod.parametr.rok_produkcji;
            car.id_kolor = samochod.kolor_id_kolor;
            car.pojemnosc_bagaznika = samochod.parametr.pojemnosc_bagaznika;
            car.przebieg = samochod.parametr.przebieg;
            car.opis = samochod.parametr.opis;
            car.id_siedzenie = samochod.parametr.siedzenie_id_siedzenie;
            car.id_nadwozie = samochod.parametr.nadwozie_id_nadwozie;
            car.id_skrzynia = samochod.parametr.skrzynia_id_skrzynia;
            car.id_opona = samochod.parametr.opona_id_opona;
            car.id_sezon = samochod.parametr.opona.sezon_id_sezon;
            car.id_firma = samochod.parametr.opona.firma_id_firma;
            car.spalanie = samochod.parametr.model.silnik.spalanie;
            car.moc = samochod.parametr.model.silnik.moc;
            car.pojemnosc = samochod.parametr.model.silnik.pojemnosc;
            car.emisja = samochod.parametr.model.silnik.emisja;
            car.przyspieszenie = samochod.parametr.model.silnik.przyspieszenie;
            car.id_paliwo = samochod.parametr.model.silnik.paliwo_id_paliwo;
            tireId = samochod.parametr.opona_id_opona;
            car.zdjecie = new Dictionary<int, string>();
            foreach (var item in db.zdjecie.Where(z => z.samochod_id_samochod == samochod.id_samochod))
            {
                car.zdjecie.Add(item.id_zdjecie,item.url);
            }
            ViewBag.id_kolor = new SelectList(db.kolor, "id_kolor", "nazwa", samochod.kolor_id_kolor);
            ViewBag.id_parametr = new SelectList(db.parametr, "id_parametr", "opis", samochod.parametr_id_parametr);
            ViewBag.id_siedzenie = new SelectList(db.siedzenie, "id_siedzenie", "ilosc", samochod.parametr.siedzenie_id_siedzenie);
            ViewBag.id_nadwozie = new SelectList(db.nadwozie, "id_nadwozie", "nazwa", samochod.parametr.nadwozie_id_nadwozie);
            ViewBag.id_skrzynia = new SelectList(db.skrzynia, "id_skrzynia", "nazwa", samochod.parametr.skrzynia_id_skrzynia);
            ViewBag.id_firma = new SelectList(db.firma.Where(f => db.opona.Where(o => o.firma_id_firma == f.id_firma).Any()).OrderBy(n => n.nazwa), "id_firma", "nazwa", samochod.parametr.opona.firma_id_firma);
            ViewBag.id_opona = new SelectList(db.opona, "id_opona", "nazwa");
            ViewBag.id_sezon = new SelectList(db.sezon, "id_sezon", "nazwa");
            ViewBag.id_paliwo = new SelectList(db.paliwo, "id_paliwo", "nazwa", samochod.parametr.model.silnik.paliwo_id_paliwo);
            return View(car);
        }

        public JsonResult DeleteImage(string url, int id)
        {
            //protection against id change
            if (db.zdjecie.Where(z => z.id_zdjecie.Equals(id)).Any())
            {

                zdjecie zdjecie = db.zdjecie.Find(id);
                string fullPath = Request.MapPath(zdjecie.url);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                db.zdjecie.Remove(zdjecie);
                db.SaveChanges();
            }
            return Json("");
        }
        public JsonResult GetModelList(int id)
        {
            var result = new { Result = new SelectList(db.opona.Where(item => (item.firma_id_firma == id)), "id_opona", "nazwa"), ID = tireId };
            return Json(result);
        }
        public JsonResult GetSezonList(int id)
        {
            opona opona = db.opona.Find(id);
            return Json(new SelectList(db.sezon.Where(item => (item.id_sezon == opona.sezon_id_sezon)), "id_sezon", "nazwa"));
        }
        
        // POST: CarsPanel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCarAdminPanel data)
        {
            samochod oldSamochod = db.samochod.Find(data.id_samochod);
            oldSamochod.cena = data.cena;
            oldSamochod.parametr.rok_produkcji = data.rok_produkcji;
            oldSamochod.kolor_id_kolor = data.id_kolor;
            oldSamochod.parametr.pojemnosc_bagaznika = data.pojemnosc_bagaznika;
            oldSamochod.parametr.przebieg = data.przebieg;
            oldSamochod.parametr.opis = data.opis;
            oldSamochod.parametr.siedzenie_id_siedzenie = data.id_siedzenie;
            oldSamochod.parametr.nadwozie_id_nadwozie = data.id_nadwozie;
            oldSamochod.parametr.skrzynia_id_skrzynia = data.id_skrzynia;
            oldSamochod.parametr.opona_id_opona = data.id_opona;
            opona opona = db.opona.Find(data.id_opona);
            opona.sezon_id_sezon = data.id_sezon;
            opona.firma_id_firma = data.id_firma;
            oldSamochod.parametr.model.silnik.spalanie = data.spalanie;
            oldSamochod.parametr.model.silnik.moc = data.moc;
            oldSamochod.parametr.model.silnik.pojemnosc = data.pojemnosc;
            oldSamochod.parametr.model.silnik.emisja = data.emisja;
            oldSamochod.parametr.model.silnik.przyspieszenie = data.przyspieszenie;
            oldSamochod.parametr.model.silnik.paliwo_id_paliwo = data.id_paliwo;
            if (ModelState.IsValid)
            {
                foreach (HttpPostedFileBase file in data.files)
                {
                    if (file != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extensionName = Path.GetExtension(file.FileName);
                        fileName = fileName + extensionName;
                        fileName = String.Concat(fileName.Where(c => !Char.IsWhiteSpace(c)));
                        string directoryPath = "/Content/Images/Model/" + oldSamochod.parametr.model.nazwa;
                        if (!Directory.Exists(Server.MapPath(directoryPath)))
                        {
                            Directory.CreateDirectory(Server.MapPath(directoryPath));
                        }
                        else
                        {
                            if(System.IO.File.Exists(Server.MapPath(directoryPath + "/") + fileName))
                            {
                                continue;
                            }
                        }
                        string destinationPath = Path.Combine(Server.MapPath(directoryPath + "/")  + fileName);
                        file.SaveAs(destinationPath);
                        zdjecie zdjecie = new zdjecie();
                        zdjecie.samochod_id_samochod = oldSamochod.id_samochod;
                        zdjecie.url = directoryPath + "/" + fileName;
                        db.zdjecie.Add(zdjecie);
                    }
                }
                db.SaveChanges();
                ViewBag.UploadStatus = "Przesłano " + data.files.Count().ToString() + " plik/i/ów.";
                return RedirectToAction("Edit");
            }
            ViewBag.id_kolor = new SelectList(db.kolor, "id_kolor", "nazwa", data.id_kolor);
            ViewBag.id_parametr = new SelectList(db.parametr, "id_parametr", "opis");
            ViewBag.id_siedzenie = new SelectList(db.siedzenie, "id_siedzenie", "ilosc", data.id_siedzenie);
            ViewBag.id_nadwozie = new SelectList(db.nadwozie, "id_nadwozie", "nazwa", data.id_nadwozie);
            ViewBag.id_skrzynia = new SelectList(db.skrzynia, "id_skrzynia", "nazwa", data.id_skrzynia);
            ViewBag.id_firma = new SelectList(db.firma, "id_firma", "nazwa", data.id_firma);
            ViewBag.id_opona = new SelectList(db.opona, "id_opona", "nazwa");
            ViewBag.id_sezon = new SelectList(db.sezon, "id_sezon", "nazwa");
            ViewBag.id_paliwo = new SelectList(db.paliwo, "id_paliwo", "nazwa");
            return View(data);
        }

        // GET: CarsPanel/Delete/5
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

        // POST: CarsPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            samochod samochod = db.samochod.Find(id);
            int modelId = samochod.parametr.model_id_model;
            int engineId = samochod.parametr.model.silnik_id_silnik;
            int parameterId = samochod.parametr_id_parametr;
            foreach (zdjecie zdjecie in db.zdjecie.Where(z => z.samochod_id_samochod == id))
            {
                db.zdjecie.Remove(zdjecie);
            }
            db.samochod.Remove(samochod);
            db.parametr.Remove(db.parametr.Where(z => z.id_parametr == parameterId).FirstOrDefault());
            db.model.Remove(db.model.Where(z => z.id_model == modelId).FirstOrDefault());
            db.silnik.Remove(db.silnik.Where(z => z.id_silnik == engineId).FirstOrDefault());
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
