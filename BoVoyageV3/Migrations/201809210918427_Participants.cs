namespace BoVoyageV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Participants : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DossierReservationAssurances", newName: "AssuranceDossierReservations");
            DropPrimaryKey("dbo.AssuranceDossierReservations");
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DossierReservationID = c.Int(nullable: false),
                        Civilite = c.String(nullable: false, maxLength: 20),
                        Nom = c.String(nullable: false, maxLength: 30),
                        Prenom = c.String(nullable: false, maxLength: 30),
                        Adresse = c.String(nullable: false, maxLength: 60),
                        Telephone = c.String(nullable: false, maxLength: 20),
                        DateNaissance = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DossiersReservations", t => t.DossierReservationID, cascadeDelete: false)
                .Index(t => t.DossierReservationID)
                .Index(t => new { t.Civilite, t.Nom, t.Prenom, t.Adresse }, unique: true, name: "IX_PersonneUnique");
            
            AddPrimaryKey("dbo.AssuranceDossierReservations", new[] { "Assurance_ID", "DossierReservation_ID" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participants", "DossierReservationID", "dbo.DossiersReservations");
            DropIndex("dbo.Participants", "IX_PersonneUnique");
            DropIndex("dbo.Participants", new[] { "DossierReservationID" });
            DropPrimaryKey("dbo.AssuranceDossierReservations");
            DropTable("dbo.Participants");
            AddPrimaryKey("dbo.AssuranceDossierReservations", new[] { "DossierReservation_ID", "Assurance_ID" });
            RenameTable(name: "dbo.AssuranceDossierReservations", newName: "DossierReservationAssurances");
        }
    }
}
