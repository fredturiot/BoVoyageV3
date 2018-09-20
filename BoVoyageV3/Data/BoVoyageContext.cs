using BoVoyageV3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BoVoyageV3.Data
{
	public class BoVoyageContext : DbContext
	{
		public BoVoyageContext() : base("BoVoyageV3ConnectionString") {}

		public DbSet<AgenceVoyage> AgencesVoyages { get; set; }
		public DbSet<Destination> Destinations { get; set; }
		public DbSet<Voyage> Voyages { get; set; }
	}
}