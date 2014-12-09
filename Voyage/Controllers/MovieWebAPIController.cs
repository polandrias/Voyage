using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Voyage.DAL;
using Voyage.Models;

namespace Voyage.Controllers
{
    public class MovieWebAPIController : ApiController
    {
        private VoyageContext db = new VoyageContext();
        
        // something ... delete if need be :)

        // GET: api/MovieWebAPI  
        public IQueryable<Movie> GetMovies()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Movies;
        }

        // GET: api/MovieWebAPI/5
        [ResponseType(typeof(Movie))]
        public IHttpActionResult GetMovie(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.ID == id) > 0;
        }
    }
}