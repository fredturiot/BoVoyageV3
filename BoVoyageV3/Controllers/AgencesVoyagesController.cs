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
using Newtonsoft.Json;

namespace BoVoyageV3.Controllers
{
	public class AgencesVoyagesController : ApiController
	{
		private BoVoyageContext db = new BoVoyageContext();

		// GET: api/AgencesVoyages
		public IQueryable<AgenceVoyage> GetAgencesVoyages()
		{
			return db.AgencesVoyages;
		}

		// GET: api/AgencesVoyages/5
		[ResponseType(typeof(AgenceVoyage))]
		public IHttpActionResult GetAgenceVoyage(int id)
		{
			AgenceVoyage agenceVoyage = db.AgencesVoyages.Find(id);
			if (agenceVoyage == null)
			{
				return NotFound();
			}

			return Ok(agenceVoyage);
		}

		// GET: api/AgencesVoyages/5?details=[true or false]
		[ResponseType(typeof(AgenceVoyage))]
		public IHttpActionResult GetAgenceVoyage(int id, bool details)
		{
			IQueryable<AgenceVoyage> requete = db.AgencesVoyages.AsQueryable();

			if (details)
			{
				requete = requete
					.Include(x => x.Voyages);

				/*db.Entry(agenceVoyage).Collection(x => x.Voyages).Query()
					.Include(y => y.Destination)
					.Load();*/
			}

			AgenceVoyage agenceVoyage = requete.SingleOrDefault(x => x.ID == id);

			if (agenceVoyage == null)
				return NotFound();

			return Ok(agenceVoyage);
		}

		// PUT: api/AgencesVoyages/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutAgenceVoyage(int id, AgenceVoyage agenceVoyage)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != agenceVoyage.ID)
			{
				return BadRequest();
			}

			db.Entry(agenceVoyage).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AgenceVoyageExists(id))
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
				ModelState.AddModelError("Unique Constraint", ex.InnerException.InnerException.Message.Split('.')[2].TrimStart());
				return BadRequest(ModelState);
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/AgencesVoyages
		[ResponseType(typeof(AgenceVoyage))]
		public IHttpActionResult PostAgenceVoyage(AgenceVoyage agenceVoyage)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.AgencesVoyages.Add(agenceVoyage);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				ModelState.AddModelError("Unique Constraint", ex.InnerException.InnerException.Message.Split('.')[2].TrimStart());
				return BadRequest(ModelState);
			}

			return CreatedAtRoute("DefaultApi", new { id = agenceVoyage.ID }, agenceVoyage);
		}

		// DELETE: api/AgencesVoyages/5
		[ResponseType(typeof(AgenceVoyage))]
		public IHttpActionResult DeleteAgenceVoyage(int id)
		{
			AgenceVoyage agenceVoyage = db.AgencesVoyages.Find(id);
			if (agenceVoyage == null)
			{
				return NotFound();
			}

			db.AgencesVoyages.Remove(agenceVoyage);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				ModelState.AddModelError("Foreign Key", "L'agence est la foreign key de un ou plusieurs voyage, veuillez les supprimer avant.");
				return BadRequest(ModelState);
			}


			return Ok(agenceVoyage);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool AgenceVoyageExists(int id)
		{
			return db.AgencesVoyages.Count(e => e.ID == id) > 0;
		}
	}
}