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
    public class DossiersReservationsController : ApiController
    {
        private BoVoyageContext db = new BoVoyageContext();

        // GET: api/DossiersReservations
        public IQueryable<DossierReservation> GetDossiersReservations()
        {
            return db.DossiersReservations;
        }

        // GET: api/DossiersReservations/5
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult GetDossierReservation(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation == null)
            {
                return NotFound();
            }

            return Ok(dossierReservation);
        }

        // PUT: api/DossiersReservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDossierReservation(int id, DossierReservation dossierReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dossierReservation.ID)
            {
                return BadRequest();
            }

            db.Entry(dossierReservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DossierReservationExists(id))
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
				ModelState.AddModelError("Constraint", ex.InnerException.InnerException.Message);
				return BadRequest(ModelState);
			}

			return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DossiersReservations
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult PostDossierReservation(DossierReservation dossierReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DossiersReservations.Add(dossierReservation);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				ModelState.AddModelError("Constraint", ex.InnerException.InnerException.Message);
				return BadRequest(ModelState);
			}

			return CreatedAtRoute("DefaultApi", new { id = dossierReservation.ID }, dossierReservation);
        }

        // DELETE: api/DossiersReservations/5
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult DeleteDossierReservation(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation == null)
            {
                return NotFound();
            }

            db.DossiersReservations.Remove(dossierReservation);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("Foreign Key", "Le dossier de reservation est la foreign key de un ou plusieurs participant, veuillez les supprimer avant.");
				return BadRequest(ModelState);
			}
			return Ok(dossierReservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DossierReservationExists(int id)
        {
            return db.DossiersReservations.Count(e => e.ID == id) > 0;
        }
    }
}