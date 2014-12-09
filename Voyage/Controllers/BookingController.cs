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

            // Save movie to session
            Session["movie"] = movie;

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

            ViewBag.Movie = Session["movie"];

            Show show = db.Shows.Find(ID);

            // Save show to session
            Session["show"] = show;

            if (Request.IsAjaxRequest())
            {
                return PartialView("StepSeats", show);
            }
            return View(show);
        }


        public ActionResult StepFindCustomer(int seats)
        {

            // Save amount seats to session
            Session["seats"] = seats;

            ViewBag.Movie = Session["movie"];

            if (Request.IsAjaxRequest())
            {
                return PartialView("StepFindCustomer");
            }
            return View();
        }


        [HttpPost]
        public ActionResult customerCheck(string phone)
        {

            ViewBag.Movie = Session["movie"];

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

                        // Save customer to session
                        Session["customer"] = customer;

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


        public ActionResult StepConfirm()
        {

            ViewBag.Show = Session["show"];
            ViewBag.Seats = Session["seats"];
            ViewBag.Movie = Session["movie"];

            return PartialView();

        }

    }
}