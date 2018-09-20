namespace BoVoyageV3.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class AgencesDestinationVoyage : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.AgencesVoyages",
				c => new
				{
					ID = c.Int(nullable: false, identity: true),
					Nom = c.String(nullable: false, maxLength: 60),
				})
				.PrimaryKey(t => t.ID)
				.Index(t => t.Nom, unique: true);

			CreateTable(
				"dbo.Voyages",
				c => new
				{
					ID = c.Int(nullable: false, identity: true),
					DateAller = c.DateTime(nullable: false),
					DateRetour = c.DateTime(nullable: false),
					PlacesDisponibles = c.Int(nullable: false),
					PrixParPersonne = c.Decimal(nullable: false, storeType: "money"),
					AgenceVoyageID = c.Int(nullable: false),
					DestinationID = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.ID)
				.ForeignKey("dbo.AgencesVoyages", t => t.AgenceVoyageID, cascadeDelete: false)
				.ForeignKey("dbo.Destinations", t => t.DestinationID, cascadeDelete: false)
				.Index(t => new { t.DateAller, t.DateRetour, t.AgenceVoyageID, t.DestinationID }, unique: true, name: "IX_DatesAgenceDestination");

			CreateTable(
				"dbo.Destinations",
				c => new
				{
					ID = c.Int(nullable: false, identity: true),
					Continent = c.String(nullable: false, maxLength: 40),
					Pays = c.String(nullable: false, maxLength: 40),
					Region = c.String(nullable: false, maxLength: 40),
					Description = c.String(),
				})
				.PrimaryKey(t => t.ID)
				.Index(t => new { t.Continent, t.Pays, t.Region }, unique: true, name: "IX_ContinentPaysRegion");

			Sql(@"
ALTER TABLE dbo.Voyages WITH CHECK ADD
	CONSTRAINT [ERROR: Le nombre de place doit etre superieur a 0.]
		CHECK (PlacesDisponibles > 0),
	CONSTRAINT [ERROR: Le prix par personne doit etre superieur a 0.]
		CHECK (PrixParPersonne > 0),
	CONSTRAINT[ERROR: La date de depart doit etre superieure ou egale a aujourd'hui.]
		CHECK (DateAller >= GETDATE()),
	CONSTRAINT[ERROR: La date de retour doit etre superieure a la date aller.]
		CHECK (DateAller < DateRetour)");
		}

		public override void Down()
		{
			DropForeignKey("dbo.Voyages", "DestinationID", "dbo.Destinations");
			DropForeignKey("dbo.Voyages", "AgenceVoyageID", "dbo.AgencesVoyages");
			DropIndex("dbo.Destinations", "IX_ContinentPaysRegion");
			DropIndex("dbo.Voyages", "IX_DatesAgenceDestination");
			DropIndex("dbo.AgencesVoyages", new[] { "Nom" });
			DropTable("dbo.Destinations");
			DropTable("dbo.Voyages");
			DropTable("dbo.AgencesVoyages");
		}
	}
}
