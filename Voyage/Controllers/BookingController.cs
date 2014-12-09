using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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
            Session["movieID"] = movie.ID;
            Session["movie"] = movie;

            ViewBag.Movie = Session["movie"];

            if (Request.IsAjaxRequest())
            {
                return PartialView("StepSelectShow", movie);
            }
            return View(movie);
        }


        // ID = Show.ID
        public ActionResult StepSeats(int ID)
        {

            Show show = db.Shows.Find(ID);

            // Save show to session
            Session["showID"] = show.ID;
            Session["show"] = show;

            ViewBag.Movie = Session["movie"];

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


        public ActionResult BackToCustomer()
        {
            ViewBag.Movie = Session["movie"];

            return PartialView("StepFindCustomer");
        }


        [HttpPost]
        public async Task<ActionResult> customerCheck(string phone)
        {

            ViewBag.Movie = Session["movie"];

            if (Request.IsAjaxRequest())
            {
                Customer customer = await db.Customers.SingleOrDefaultAsync(c => c.Phone == phone);
                if (customer == null)
                {
                    Customer newCustomer = new Customer();

                    return PartialView("createCustomer", newCustomer);
                }
                else
                {
                    return PartialView("editCustomer", customer);
                }

            }

            return PartialView("createCustomer");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public int ajaxCreateCustomer([Bind(Include = "Firstname,Lastname,Email,Phone")] Customer customer)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return customer.ID;
                }
            }

            return customer.ID;
        }


        // ID = Customer.ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<int> ajaxSaveCustomer([Bind(Include = "ID,Firstname,Lastname,Email,Phone")] Customer customer)
        {
            if (Request.IsAjaxRequest())
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(customer).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                        // Save customer to session
                        Session["customer"] = customer;
                        Session["customerID"] = customer.ID;

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


        public async Task<ActionResult> StepComplete(int statusID)
        {

            ViewBag.Show = Session["show"];
            ViewBag.Movie = Session["movie"];

            var customerID = Session["customerID"];
            var showID = Session["showID"];
            var seats = Session["seats"];

            Customer customer = await db.Customers.FindAsync(customerID);
            Show show = await db.Shows.FindAsync(showID);

            if (Request.IsAjaxRequest())
            {
                var CreateBooking = db.Database.ExecuteSqlCommand("SP_CreateBooking @firstname, @lastname, @phone, @email, @showId, @statusId, @seats",
                new SqlParameter("@firstname", customer.Firstname),
                new SqlParameter("@lastname", customer.Lastname),
                new SqlParameter("@phone", customer.Phone),
                new SqlParameter("@email", customer.Email),
                new SqlParameter("@showId", show.ID),
                new SqlParameter("@statusId", statusID),
                new SqlParameter("@seats", seats));

                return PartialView("StepComplete");
            }

            return PartialView();
        }

    }
}