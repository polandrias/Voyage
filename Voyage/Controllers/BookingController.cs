using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Voyage.DAL;
using Voyage.Models;

namespace Voyage.Controllers
{
    public class BookingController : Controller
    {

        private VoyageContext db = new VoyageContext();

        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }


        // ID = Movie.ID
        public ActionResult StepSelectShow(int ID)
        {

            Movie movie = db.Movies.Find(ID);

            Session["movieID"] = movie.ID;
            ViewBag.Movie = movie;

            if (Request.IsAjaxRequest())
            {
                return PartialView("StepSelectShow", movie);
            }
            return View(movie);
        }


        // ID = Show.ID
        public ActionResult StepSeats(int ID)
        {

            var movieID = Session["movieID"];
            ViewBag.Movie = db.Movies.Find(movieID);

            Show show = db.Shows.Find(ID);

            if (Request.IsAjaxRequest())
            {
                return PartialView("StepSeats", show);
            }
            return View(show);
        }


        public ActionResult StepFindCustomer()
        {

            var movieID = Session["movieID"];
            ViewBag.Movie = db.Movies.Find(movieID);

            if (Request.IsAjaxRequest())
            {
                return PartialView("StepFindCustomer");
            }
            return View();
        }


        [HttpPost]
        public ActionResult customerCheck(string phone)
        {

            var movieID = Session["movieID"];
            ViewBag.Movie = db.Movies.Find(movieID);

            if (Request.IsAjaxRequest())
            {
                Customer customer = db.Customers.SingleOrDefault(c => c.Phone == phone);
                if (customer == null)
                {
                    return PartialView("createCustomer");
                }
                else
                {
                    return PartialView("editCustomer", customer);
                }

            }

            return PartialView("createCustomer");
        }


        // ID = Customer.ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int ajaxSaveCustomer(Customer customer)
        {
            if (Request.IsAjaxRequest())
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();
                        return customer.ID;
                    }
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }

                return 0;
            }
            return customer.ID;
        }

    }
}