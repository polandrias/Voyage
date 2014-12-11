using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Voyage.DAL;
using Voyage.Models;

namespace Voyage.Areas.Admin.Controllers
{
    public class MovieController : Controller
    {
        private VoyageContext db = new VoyageContext();

        // GET: Admin/Movies
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Genre);
            return View(movies.ToList());
        }

        // GET: Admin/Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Admin/Movies/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "ID", "Name");
            return View();
        }

        // POST: Admin/Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Duration,Embed,Rating,Actor,C3D,Language,Premiere,Release,GenreId,Highlighted")] Movie movie, HttpPostedFileBase image_p, HttpPostedFileBase image_l)
        {
            if (ModelState.IsValid)
            {

                db.Movies.Add(movie);

                // list all movies in db
                var movies = db.Movies.ToList();

                // if this movie is highligheted
                if (movie.Highlighted == true)
                {
                    // un-highlight all movies in db
                    foreach (Movie m in movies)
                    {
                        var mo = db.Movies.Find(m.ID);
                        mo.Highlighted = false;
                    }
                    // highlight current movie
                    movie.Highlighted = true;
                }

                // upload image_p to server + filename to db
                if (image_p != null && image_p.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(image_p.FileName);
                    // get extension from filename
                    string ext = Path.GetExtension(pic);
                    // give the file the movie's title_p + ext without spaces
                    string file = Regex.Replace(movie.Title, @"\s+", "")+"_p"+ext;
                    // lav stien til billedmappen
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Content/Images"), file);
                    // file is uploaded
                    image_p.SaveAs(path);
                    // sæt filnavnet i databasen
                    movie.PosterPath = file;
                }

                // upload image_l to server + filename to db
                if (image_l != null && image_l.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(image_l.FileName);
                    // get extension from filename
                    string ext = Path.GetExtension(pic);
                    // give the file the movie's title_p + ext without spaces
                    string file = Regex.Replace(movie.Title, @"\s+", "") + "_l" + ext;
                    // lav stien til billedmappen
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Content/Images"), file);
                    // file is uploaded
                    image_l.SaveAs(path);
                    // sæt filnavnet i databasen
                    movie.BigPosterPath = file;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Fejl i formular");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "ID", "Name", movie.GenreId);
            return View(movie);
        }

        // GET: Admin/Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "ID", "Name", movie.GenreId);
            return View(movie);
        }

        // POST: Admin/Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,PosterPath,BigPosterPath,Duration,Embed,Rating,Actor,C3D,Language,Premiere,Release,GenreId,Highlighted")] Movie movie, HttpPostedFileBase image_p, HttpPostedFileBase image_l)
        {
            System.Diagnostics.Debug.WriteLine("Before ModelState");
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;

                // list all movies in db
                var movies = db.Movies.ToList();

                // if this movie is highligheted
                if (movie.Highlighted == true)
                {
                    // un-highlight all movies in db
                    foreach (Movie m in movies)
                    {
                        var mo = db.Movies.Find(m.ID);
                        mo.Highlighted = false;
                    }
                    // highlight current movie
                    movie.Highlighted = true;
                }

                // upload image_p to server + filename to db
                if (image_p != null && image_p.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(image_p.FileName);
                    // get extension from filename
                    string ext = Path.GetExtension(pic);
                    // give the file the movie's title_p + ext without spaces
                    string file = Regex.Replace(movie.Title, @"\s+", "") + "_p" + ext;
                    // lav stien til billedmappen
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Content/Images"), file);
                    // file is uploaded
                    image_p.SaveAs(path);
                    // sæt filnavnet i databasen
                    movie.PosterPath = file;

                    System.Diagnostics.Debug.WriteLine(file + " - " + movie.PosterPath);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Else");
                }

                // upload image_l to server + filename to db
                if (image_l != null && image_l.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(image_l.FileName);
                    // get extension from filename
                    string ext = Path.GetExtension(pic);
                    // give the file the movie's title_p + ext without spaces
                    string file = Regex.Replace(movie.Title, @"\s+", "") + "_l" + ext;
                    // lav stien til billedmappen
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/Content/Images"), file);
                    // file is uploaded
                    image_l.SaveAs(path);
                    // sæt filnavnet i databasen
                    movie.BigPosterPath = file;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "ID", "Name", movie.GenreId);
            return View(movie);
        }

        // GET: Admin/Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Admin/Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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
