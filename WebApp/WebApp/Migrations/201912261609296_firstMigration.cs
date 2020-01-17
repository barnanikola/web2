namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cenovniks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VaziOd = c.DateTime(nullable: false),
                        VaziDo = c.DateTime(nullable: false),
                        CenaVremenskeKarte = c.Double(nullable: false),
                        CenaDnevneKarte = c.Double(nullable: false),
                        CenaMesecneKarte = c.Double(nullable: false),
                        CenaGodisnjeKarte = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kartas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipKarte = c.String(),
                        Cena = c.Double(nullable: false),
                        EmailKorisnika = c.String(),
                        DatumIsteka = c.DateTime(nullable: false),
                        DatumIzdavanja = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Linijas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Broj = c.Int(nullable: false),
                        Boja = c.String(),
                        TipLinije = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stanicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Adresa = c.String(),
                        Position_lat = c.Double(nullable: false),
                        Position_lng = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RedVoznjes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dan = c.String(),
                        LinijaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Voznjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinijaId = c.Int(nullable: false),
                        Polazak = c.String(),
                        RedVoznje_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RedVoznjes", t => t.RedVoznje_Id)
                .Index(t => t.RedVoznje_Id);
            
            CreateTable(
                "dbo.StanicaLinijas",
                c => new
                    {
                        Stanica_Id = c.Int(nullable: false),
                        Linija_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Stanica_Id, t.Linija_Id })
                .ForeignKey("dbo.Stanicas", t => t.Stanica_Id, cascadeDelete: true)
                .ForeignKey("dbo.Linijas", t => t.Linija_Id, cascadeDelete: true)
                .Index(t => t.Stanica_Id)
                .Index(t => t.Linija_Id);
            
            AddColumn("dbo.AspNetUsers", "Ime", c => c.String());
            AddColumn("dbo.AspNetUsers", "Prezime", c => c.String());
            AddColumn("dbo.AspNetUsers", "DatRodj", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Adresa", c => c.String());
            AddColumn("dbo.AspNetUsers", "TipKorisnika", c => c.String());
            AddColumn("dbo.AspNetUsers", "Status", c => c.String());
            AddColumn("dbo.AspNetUsers", "VrstaNaloga", c => c.String());
            AddColumn("dbo.AspNetUsers", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Voznjas", "RedVoznje_Id", "dbo.RedVoznjes");
            DropForeignKey("dbo.StanicaLinijas", "Linija_Id", "dbo.Linijas");
            DropForeignKey("dbo.StanicaLinijas", "Stanica_Id", "dbo.Stanicas");
            DropIndex("dbo.StanicaLinijas", new[] { "Linija_Id" });
            DropIndex("dbo.StanicaLinijas", new[] { "Stanica_Id" });
            DropIndex("dbo.Voznjas", new[] { "RedVoznje_Id" });
            DropColumn("dbo.AspNetUsers", "Url");
            DropColumn("dbo.AspNetUsers", "VrstaNaloga");
            DropColumn("dbo.AspNetUsers", "Status");
            DropColumn("dbo.AspNetUsers", "TipKorisnika");
            DropColumn("dbo.AspNetUsers", "Adresa");
            DropColumn("dbo.AspNetUsers", "DatRodj");
            DropColumn("dbo.AspNetUsers", "Prezime");
            DropColumn("dbo.AspNetUsers", "Ime");
            DropTable("dbo.StanicaLinijas");
            DropTable("dbo.Voznjas");
            DropTable("dbo.RedVoznjes");
            DropTable("dbo.Stanicas");
            DropTable("dbo.Linijas");
            DropTable("dbo.Kartas");
            DropTable("dbo.Cenovniks");
        }
    }
}
