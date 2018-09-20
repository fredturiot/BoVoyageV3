namespace BoVoyageV3.Migrations
{
	using BoVoyageV3.Models;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<BoVoyageV3.Data.BoVoyageContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(BoVoyageV3.Data.BoVoyageContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.

			context.AgencesVoyages.AddOrUpdate(x => x.ID,
				new AgenceVoyage() { ID = 1, Nom = "Les voyages de Charly" },
				new AgenceVoyage() { ID = 2, Nom = "Les merveilles du monde" },
				new AgenceVoyage() { ID = 3, Nom = "Les destinations de reves" }
			);

			context.Destinations.AddOrUpdate(x => x.ID,
				new Destination() { ID = 1, Continent = "Europe", Pays = "France", Region = "Paris", Description = "La ville lumiere" },
				new Destination() { ID = 2, Continent = "Asie", Pays = "Chine", Region = "Shanghai", Description = "Visite dans le monde moderne" },
				new Destination() { ID = 3, Continent = "Afrique", Pays = "Togo", Region = "Lome" },
				new Destination() { ID = 4, Continent = "Amerique du Sud", Pays = "Argentine", Region = "Bueno Aires", Description = "De la viande et du tango" }
			);

			context.Voyages.AddOrUpdate(x => x.ID,
				new Voyage() { ID = 1, DateAller = DateTime.Parse("2018/10/20"), DateRetour = DateTime.Parse("2018/11/01"), PlacesDisponibles = 150, PrixParPersonne = 1999.0m, AgenceVoyageID = 3, DestinationID = 1 },
				new Voyage() { ID = 2, DateAller = DateTime.Parse("2018/11/05"), DateRetour = DateTime.Parse("2018/11/15"), PlacesDisponibles = 44, PrixParPersonne = 450.0m, AgenceVoyageID = 2, DestinationID = 2 },
				new Voyage() { ID = 3, DateAller = DateTime.Parse("2018/10/01"), DateRetour = DateTime.Parse("2018/10/25"), PlacesDisponibles = 15, PrixParPersonne = 680.0m, AgenceVoyageID = 3, DestinationID = 3 },
				new Voyage() { ID = 4, DateAller = DateTime.Parse("2018/10/10"), DateRetour = DateTime.Parse("2018/10/20"), PlacesDisponibles = 25, PrixParPersonne = 479.0m, AgenceVoyageID = 1, DestinationID = 4 },
				new Voyage() { ID = 5, DateAller = DateTime.Parse("2018/11/01"), DateRetour = DateTime.Parse("2018/11/30"), PlacesDisponibles = 20, PrixParPersonne = 800.0m, AgenceVoyageID = 1, DestinationID = 4 },
				new Voyage() { ID = 6, DateAller = DateTime.Parse("2018/11/20"), DateRetour = DateTime.Parse("2018/12/01"), PlacesDisponibles = 100, PrixParPersonne = 3500.0m, AgenceVoyageID = 3, DestinationID = 3 },
				new Voyage() { ID = 7, DateAller = DateTime.Parse("2018/10/15"), DateRetour = DateTime.Parse("2018/11/05"), PlacesDisponibles = 350, PrixParPersonne = 1000.0m, AgenceVoyageID = 2, DestinationID = 2 },
				new Voyage() { ID = 8, DateAller = DateTime.Parse("2018/12/01"), DateRetour = DateTime.Parse("2018/12/31"), PlacesDisponibles = 10, PrixParPersonne = 2000.0m, AgenceVoyageID = 2, DestinationID = 1 }
			);
		}
	}
}
