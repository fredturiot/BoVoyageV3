namespace BoVoyageV3.Migrations
{
	using BoVoyageV3.Models;
	using System;
	using System.Collections.Generic;
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

			context.Clients.AddOrUpdate(x => x.ID,
				new Client() { ID = 1, Civilite = "Madame", Nom = "Chirac", Prenom = "Elisabeth", Adresse = "3 rue des casseroles", Telephone = "01.02.03.04.05", DateNaissance = DateTime.Parse("1955/02/24"), Email = "elisabeth.chirac@france.fr" },
				new Client() { ID = 2, Civilite = "Monsieur", Nom = "Bush", Prenom = "Gerard", Adresse = "3 rue de Bagdad", Telephone = "06.66.66.66.66", DateNaissance = DateTime.Parse("1980/09/01"), Email = "gerad.bush@usa.us" }
			);

			context.Assurances.AddOrUpdate(x => x.ID,
				new Assurance() { ID = 1, Montant = 100.0m, Type = TypeAssurance.Annulation },
				new Assurance() { ID = 2, Montant = 200.0m, Type = TypeAssurance.Annulation }
			);

			//Assurance assurance100 = new Assurance() { ID = 1, Montant = 100.0m, Type = TypeAssurance.Annulation };
			//Assurance assurance200 = new Assurance() { ID = 2, Montant = 200.0m, Type = TypeAssurance.Annulation };

			//List<Assurance> assurances = new List<Assurance>
			//{
			//	assurance100,
			//	assurance200
			//};

			context.DossiersReservations.AddOrUpdate(x => x.ID,
				new DossierReservation() { ID = 1, ClientID = 2, VoyageID = 3, NumeroCarteBancaire = "123456789", Etat = EtatDossierReservation.EnAttente },
				new DossierReservation()
				{
					ID = 2,
					ClientID = 1,
					VoyageID = 8,
					NumeroCarteBancaire = "777777777",
					Etat = EtatDossierReservation.EnAttente,
					//Assurances = {
					//	new Assurance() { ID = 1, Montant = 100.0m, Type = TypeAssurance.Annulation },
					//	new Assurance() { ID = 2, Montant = 200.0m, Type = TypeAssurance.Annulation }

					//}
				}
			);

			context.Participants.AddOrUpdate(x => x.ID,
				new Participant()
				{
					ID = 1,
					Civilite = "Mademoiselle", Nom = "Chirac", Prenom = "Fatima",
					Adresse = "3 rue des casseroles",
					Telephone = "01.02.03.04.05",
					DateNaissance = DateTime.Parse("1990/02/24"),
					DossierReservationID = 1
				},
				new Participant()
				{
					ID = 2,
					Civilite = "Mademoiselle", Nom = "Chirac", Prenom = "Murielle",
					Adresse = "3 rue des casseroles",
					Telephone = "01.02.03.04.05",
					DateNaissance = DateTime.Parse("2018/09/01"),
					DossierReservationID = 1
				},
				new Participant()
				{
					ID = 3,
					Civilite = "Monsieur", Nom = "Chirac", Prenom = "Babakar",
					Adresse = "3 rue des casseroles",
					Telephone = "01.02.03.04.05",
					DateNaissance = DateTime.Parse("1985/02/11"),
					DossierReservationID = 1
				},
				new Participant()
				{
					ID = 4,
					Civilite = "Monsieur", Nom = "Bush", Prenom = "Barack",
					Adresse = "1 rue de la paix",
					Telephone = "01.02.03.04.05",
					DateNaissance = DateTime.Parse("2000/02/24"),
					DossierReservationID = 2
				},
				new Participant()
				{
					ID = 5,
					Civilite = "Madame", Nom = "Bush", Prenom = "Clinton",
					Adresse = "1 rue de la paix",
					Telephone = "01.02.03.04.05",
					DateNaissance = DateTime.Parse("2018/02/24"),
					DossierReservationID = 2
				}
			);
		}
	}
}
