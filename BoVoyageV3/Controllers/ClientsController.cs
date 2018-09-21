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
	public class ClientsController : ApiController
	{
		private BoVoyageContext db = new BoVoyageContext();

		// GET: api/Clients
		public IQueryable<Client> GetClients()
		{
			return db.Clients;
		}

		// GET: api/Clients/5
		[ResponseType(typeof(Client))]
		public IHttpActionResult GetClient(int id)
		{
			Client client = db.Clients.Find(id);
			if (client == null)
			{
				return NotFound();
			}

			return Ok(client);
		}

		// PUT: api/Clients/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutClient(int id, Client client)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != client.ID)
			{
				return BadRequest();
			}

			db.Entry(client).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ClientExists(id))
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

		// POST: api/Clients
		[ResponseType(typeof(Client))]
		public IHttpActionResult PostClient(Client client)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Clients.Add(client);

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

			return CreatedAtRoute("DefaultApi", new { id = client.ID }, client);
		}

		// DELETE: api/Clients/5
		[ResponseType(typeof(Client))]
		public IHttpActionResult DeleteClient(int id)
		{
			Client client = db.Clients.Find(id);
			if (client == null)
			{
				return NotFound();
			}

			db.Clients.Remove(client);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("Erreur", "Le client est la foreign key de un ou plusieurs dossier de reservation, veuillez les supprimer avant.");
				return BadRequest(ModelState);
			}

			return Ok(client);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool ClientExists(int id)
		{
			return db.Clients.Count(e => e.ID == id) > 0;
		}
	}
}