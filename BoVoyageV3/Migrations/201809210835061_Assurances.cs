namespace BoVoyageV3.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class Assurances : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Assurances",
				c => new
				{
					ID = c.Int(nullable: false, identity: true),
					Montant = c.Decimal(nullable: false, storeType: "money"),
					Type = c.Byte(nullable: false),
				})
				.PrimaryKey(t => t.ID)
				.Index(t => new { t.Montant, t.Type }, unique: true, name: "IX_MontantType");

			Sql(@"
ALTER TABLE dbo.Assurances WITH CHECK ADD
	CONSTRAINT [ERROR: Le prix de l'assurance doit etre superieur a 0.]
		CHECK (Montant > 0);");
		}

		public override void Down()
		{
			DropIndex("dbo.Assurances", "IX_MontantType");
			DropTable("dbo.Assurances");
		}
	}
}
