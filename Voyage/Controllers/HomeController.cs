using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Voyage.Models;
using Voyage.DAL;

namespace Voyage.Controllers
{
    public class HomeController : Controller
    {

        private VoyageContext db = new VoyageContext();

        // GET: Home
        public ActionResult Index()
        {

            var Movies = db.Movies.ToList();

            ViewBag.currentDate = DateTime.Now;

            return View(Movies);
        }
    }
}