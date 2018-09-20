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
using BoVoyageV3.Data;
using BoVoyageV3.Models;

namespace BoVoyageV3.Controllers
{
    public class VoyagesController : ApiController
    {
        private BoVoyageContext db = new BoVoyageContext();

        // GET: api/Voyages
        public IQueryable<Voyage> GetVoyages()
        {
            return db.Voyages;
        }

        // GET: api/Voyages/5
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult GetVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            return Ok(voyage);
        }

        // PUT: api/Voyages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVoyage(int id, Voyage voyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voyage.ID)
            {
                return BadRequest();
            }

            db.Entry(voyage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
			catch (DbUpdateException ex)
			{
				bool testErreur = ex.InnerException.InnerException.Message.Contains("ERROR");
				string[] errorArray = testErreur ? ex.InnerException.InnerException.Message.Split('"') : ex.InnerException.InnerException.Message.Split('.');

				string error = testErreur ? errorArray[1] : errorArray[2].TrimStart();
				ModelState.AddModelError("Constraint", error);
				return BadRequest(ModelState);
			}

			return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Voyages
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult PostVoyage(Voyage voyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Voyages.Add(voyage);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				bool testErreur = ex.InnerException.InnerException.Message.Contains("ERROR");
				string[] errorArray = testErreur ? ex.InnerException.InnerException.Message.Split('"') : ex.InnerException.InnerException.Message.Split('.');

				string error = testErreur ? errorArray[1] : errorArray[2].TrimStart();
				ModelState.AddModelError("Constraint", error);
				return BadRequest(ModelState);
			}

			return CreatedAtRoute("DefaultApi", new { id = voyage.ID }, voyage);
        }

        // DELETE: api/Voyages/5
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult DeleteVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            db.Voyages.Remove(voyage);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("Foreign Key", "Le voyage est la foreign key de un ou plusieurs dosier de reservation, veuillez les supprimer avant.");
				return BadRequest(ModelState);
			}
			return Ok(voyage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoyageExists(int id)
        {
            return db.Voyages.Count(e => e.ID == id) > 0;
        }
    }
}