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
    }
}
