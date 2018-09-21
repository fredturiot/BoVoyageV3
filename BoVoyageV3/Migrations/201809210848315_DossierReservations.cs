namespace BoVoyageV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DossierReservations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DossiersReservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroCarteBancaire = c.String(nullable: false, maxLength: 20),
                        Etat = c.Byte(nullable: false),
                        ClientID = c.Int(nullable: false),
                        VoyageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: false)
                .ForeignKey("dbo.Voyages", t => t.VoyageID, cascadeDelete: false)
                .Index(t => t.ClientID)
                .Index(t => t.VoyageID);
            
            CreateTable(
                "dbo.DossierReservationAssurances",
                c => new
                    {
                        DossierReservation_ID = c.Int(nullable: false),
                        Assurance_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DossierReservation_ID, t.Assurance_ID })
                .ForeignKey("dbo.DossiersReservations", t => t.DossierReservation_ID, cascadeDelete: false)
                .ForeignKey("dbo.Assurances", t => t.Assurance_ID, cascadeDelete: false)
                .Index(t => t.DossierReservation_ID)
                .Index(t => t.Assurance_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DossiersReservations", "VoyageID", "dbo.Voyages");
            DropForeignKey("dbo.DossiersReservations", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.DossierReservationAssurances", "Assurance_ID", "dbo.Assurances");
            DropForeignKey("dbo.DossierReservationAssurances", "DossierReservation_ID", "dbo.DossiersReservations");
            DropIndex("dbo.DossierReservationAssurances", new[] { "Assurance_ID" });
            DropIndex("dbo.DossierReservationAssurances", new[] { "DossierReservation_ID" });
            DropIndex("dbo.DossiersReservations", new[] { "VoyageID" });
            DropIndex("dbo.DossiersReservations", new[] { "ClientID" });
            DropTable("dbo.DossierReservationAssurances");
            DropTable("dbo.DossiersReservations");
        }
    }
}
