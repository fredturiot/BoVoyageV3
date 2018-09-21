using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BoVoyageV3.Data;
using BoVoyageV3.Models;

namespace BoVoyageV3.Controllers
{
	public class AssurancesController : ApiController
	{
		private BoVoyageContext db = new BoVoyageContext();

		// GET: api/Assurances
		public IQueryable<Assurance> GetAssurances()
		{
			return db.Assurances;
		}

		// GET: api/Assurances/5
		[ResponseType(typeof(Assurance))]
		public IHttpActionResult GetAssurance(int id)
		{
			Assurance assurance = db.Assurances.Find(id);
			if (assurance == null)
			{
				return NotFound();
			}

			return Ok(assurance);
		}

		// PUT: api/Assurances/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutAssurance(int id, Assurance assurance)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != assurance.ID)
			{
				return BadRequest();
			}

			db.Entry(assurance).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AssuranceExists(id))
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
				ModelState.AddModelError("Erreur", ex.InnerException.InnerException.Message);
				return BadRequest(ModelState);
			}
			catch (DbEntityValidationException dbEx)
			{
				ModelState.AddModelError("Erreur", dbEx.EntityValidationErrors.ToString());
				return BadRequest(ModelState);
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Assurances
		[ResponseType(typeof(Assurance))]
		public IHttpActionResult PostAssurance(Assurance assurance)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Assurances.Add(assurance);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				ModelState.AddModelError("Erreur", ex.InnerException.InnerException.Message);
				return BadRequest(ModelState);
			}
			catch (DbEntityValidationException dbEx)
			{
				ModelState.AddModelError("Erreur", dbEx.EntityValidationErrors.ToString());
				return BadRequest(ModelState);
			}

			return CreatedAtRoute("DefaultApi", new { id = assurance.ID }, assurance);
		}

		// DELETE: api/Assurances/5
		[ResponseType(typeof(Assurance))]
		public IHttpActionResult DeleteAssurance(int id)
		{
			Assurance assurance = db.Assurances.Find(id);
			if (assurance == null)
			{
				return NotFound();
			}

			db.Assurances.Remove(assurance);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("Erreur", "L'assurance est la foreign key de un ou plusieurs dossier de reservation, veuillez les supprimer avant.");
				return BadRequest(ModelState);
			}
			return Ok(assurance);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool AssuranceExists(int id)
		{
			return db.Assurances.Count(e => e.ID == id) > 0;
		}
	}
}