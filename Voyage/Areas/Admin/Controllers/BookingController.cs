using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Voyage.DAL;
using Voyage.Models;

namespace Voyage.Areas.Admin.Controllers
{
    public class BookingController : Controller
    {
        private VoyageContext db = new VoyageContext();

        // GET: Admin/Booking
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Customer).Include(b => b.Show).Include(b => b.Status);
            return View(bookings.ToList());
        }

        // GET: Admin/Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Admin/Booking/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "Firstname");
            ViewBag.ShowId = new SelectList(db.Shows, "ID", "ID");
            ViewBag.StatusId = new SelectList(db.Status, "ID", "Name");
            return View();
        }

        // POST: Admin/Booking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Price,Seats,ShowId,StatusId,CustomerId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "Firstname", booking.CustomerId);
            ViewBag.ShowId = new SelectList(db.Shows, "ID", "ID", booking.ShowId);
            ViewBag.StatusId = new SelectList(db.Status, "ID", "Name", booking.StatusId);
            return View(booking);
        }

        // GET: Admin/Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "Firstname", booking.CustomerId);
            ViewBag.ShowId = new SelectList(db.Shows, "ID", "ID", booking.ShowId);
            ViewBag.StatusId = new SelectList(db.Status, "ID", "Name", booking.StatusId);
            return View(booking);
        }

        // POST: Admin/Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Price,Seats,ShowId,StatusId,CustomerId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "ID", "Firstname", booking.CustomerId);
            ViewBag.ShowId = new SelectList(db.Shows, "ID", "ID", booking.ShowId);
            ViewBag.StatusId = new SelectList(db.Status, "ID", "Name", booking.StatusId);
            return View(booking);
        }

        // GET: Admin/Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Admin/Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
