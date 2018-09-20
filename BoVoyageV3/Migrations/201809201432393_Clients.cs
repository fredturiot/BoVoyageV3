namespace BoVoyageV3.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class Clients : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Clients",
				c => new
				{
					ID = c.Int(nullable: false, identity: true),
					Email = c.String(nullable: false, maxLength: 60),
					Civilite = c.String(nullable: false, maxLength: 20),
					Nom = c.String(nullable: false, maxLength: 30),
					Prenom = c.String(nullable: false, maxLength: 30),
					Adresse = c.String(nullable: false, maxLength: 60),
					Telephone = c.String(nullable: false, maxLength: 20),
					DateNaissance = c.DateTime(nullable: false),
				})
				.PrimaryKey(t => t.ID)
				.Index(t => t.Email, unique: true)
				.Index(t => new { t.Civilite, t.Nom, t.Prenom, t.Adresse }, unique: true, name: "IX_PersonneUnique");

			Sql(@"
ALTER TABLE dbo.Clients WITH CHECK ADD
	CONSTRAINT [ERROR: La date de naissance doit etre inferieure a aujourd'hui.]
		CHECK (DateNaissance < GETDATE())");
		}

		public override void Down()
		{
			DropIndex("dbo.Clients", "IX_PersonneUnique");
			DropIndex("dbo.Clients", new[] { "Email" });
			DropTable("dbo.Clients");
		}
	}
}
