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
    public class ShowsController : Controller
    {
        private VoyageContext db = new VoyageContext();

        // GET: Admin/Shows
        public ActionResult Index()
        {
            var shows = db.Shows.Include(s => s.Movie).Include(s => s.Theatre);
            return View(shows.ToList());
        }

        // GET: Admin/Shows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        // GET: Admin/Shows/Create
        public ActionResult Create()
        {
            ViewBag.MovieId = new SelectList(db.Movies, "ID", "Title");
            ViewBag.TheatreId = new SelectList(db.Theatres, "ID", "Name");
            return View();
        }

        // POST: Admin/Shows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Time,VIP,Price,MovieId,TheatreId")] Show show)
        {
            if (ModelState.IsValid)
            {
                db.Shows.Add(show);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieId = new SelectList(db.Movies, "ID", "Title", show.MovieId);
            ViewBag.TheatreId = new SelectList(db.Theatres, "ID", "Name", show.TheatreId);
            return View(show);
        }

        // GET: Admin/Shows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieId = new SelectList(db.Movies, "ID", "Title", show.MovieId);
            ViewBag.TheatreId = new SelectList(db.Theatres, "ID", "Name", show.TheatreId);
            return View(show);
        }

        // POST: Admin/Shows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Time,VIP,Price,MovieId,TheatreId")] Show show)
        {
            if (ModelState.IsValid)
            {
                db.Entry(show).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieId = new SelectList(db.Movies, "ID", "Title", show.MovieId);
            ViewBag.TheatreId = new SelectList(db.Theatres, "ID", "Name", show.TheatreId);
            return View(show);
        }

        // GET: Admin/Shows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        // POST: Admin/Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Show show = db.Shows.Find(id);
            db.Shows.Remove(show);
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
