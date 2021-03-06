﻿using System;
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
	public class DestinationsController : ApiController
	{
		private BoVoyageContext db = new BoVoyageContext();

		// GET: api/Destinations
		public IQueryable<Destination> GetDestinations()
		{
			return db.Destinations;
		}

		// GET: api/Destinations/5
		[ResponseType(typeof(Destination))]
		public IHttpActionResult GetDestination(int id)
		{
			Destination destination = db.Destinations.Find(id);
			if (destination == null)
			{
				return NotFound();
			}

			return Ok(destination);
		}

		// GET: api/Destinations/5?details=[true or false]
		[ResponseType(typeof(Destination))]
		public IHttpActionResult GetDestination(int id, bool details)
		{
			Destination destination = db.Destinations.Find(id);
			if (destination == null)
			{
				return NotFound();
			}

			if (details)
				db.Entry(destination).Collection("Voyages").Load();

			return Ok(destination);
		}

		// PUT: api/Destinations/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutDestination(int id, Destination destination)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != destination.ID)
			{
				return BadRequest();
			}

			db.Entry(destination).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DestinationExists(id))
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

		// POST: api/Destinations
		[ResponseType(typeof(Destination))]
		public IHttpActionResult PostDestination(Destination destination)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Destinations.Add(destination);

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

			return CreatedAtRoute("DefaultApi", new { id = destination.ID }, destination);
		}

		// DELETE: api/Destinations/5
		[ResponseType(typeof(Destination))]
		public IHttpActionResult DeleteDestination(int id)
		{
			Destination destination = db.Destinations.Find(id);
			if (destination == null)
			{
				return NotFound();
			}

			db.Destinations.Remove(destination);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("Erreur", "L'agence est la foreign key de un ou plusieurs voyage, veuillez les supprimer avant.");
				return BadRequest(ModelState);
			}

			return Ok(destination);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool DestinationExists(int id)
		{
			return db.Destinations.Count(e => e.ID == id) > 0;
		}
	}
}