namespace bazadanych.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auta",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        marka = c.String(),
                        model = c.String(),
                        rocznik = c.Int(nullable: false),
                        vin = c.String(),
                        idKlienta_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Klienci", t => t.idKlienta_id)
                .Index(t => t.idKlienta_id);
            
            CreateTable(
                "dbo.Klienci",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        imie = c.String(),
                        nazwisko = c.String(),
                        telefon = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Czesci",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nazwa = c.String(),
                        opis = c.String(),
                        cenaCzesci = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.DostepneCzesci",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ilosc = c.Int(nullable: false),
                        idCzesci_id = c.Int(),
                        idMagazynu_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Czesci", t => t.idCzesci_id)
                .ForeignKey("dbo.Magazyny", t => t.idMagazynu_id)
                .Index(t => t.idCzesci_id)
                .Index(t => t.idMagazynu_id);
            
            CreateTable(
                "dbo.Magazyny",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        adres = c.String(),
                        nazwa = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PotrzebneCzesci",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ilosc = c.Int(nullable: false),
                        idCzesci_id = c.Int(),
                        idZlecenia_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Czesci", t => t.idCzesci_id)
                .ForeignKey("dbo.Zlecenia", t => t.idZlecenia_id)
                .Index(t => t.idCzesci_id)
                .Index(t => t.idZlecenia_id);
            
            CreateTable(
                "dbo.Zlecenia",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        opisUsterki = c.String(),
                        opisNaprawy = c.String(),
                        dataZlecenia = c.DateTime(nullable: false),
                        koszt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        idAuta_id = c.Int(),
                        idMechanika_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Auta", t => t.idAuta_id)
                .ForeignKey("dbo.Mechanicy", t => t.idMechanika_id)
                .Index(t => t.idAuta_id)
                .Index(t => t.idMechanika_id);
            
            CreateTable(
                "dbo.Mechanicy",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        imie = c.String(),
                        nazwisko = c.String(),
                        pensja = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zlecenia", "idMechanika_id", "dbo.Mechanicy");
            DropForeignKey("dbo.Zlecenia", "idAuta_id", "dbo.Auta");
            DropForeignKey("dbo.PotrzebneCzesci", "idZlecenia_id", "dbo.Zlecenia");
            DropForeignKey("dbo.PotrzebneCzesci", "idCzesci_id", "dbo.Czesci");
            DropForeignKey("dbo.DostepneCzesci", "idMagazynu_id", "dbo.Magazyny");
            DropForeignKey("dbo.DostepneCzesci", "idCzesci_id", "dbo.Czesci");
            DropForeignKey("dbo.Auta", "idKlienta_id", "dbo.Klienci");
            DropIndex("dbo.Zlecenia", new[] { "idMechanika_id" });
            DropIndex("dbo.Zlecenia", new[] { "idAuta_id" });
            DropIndex("dbo.PotrzebneCzesci", new[] { "idZlecenia_id" });
            DropIndex("dbo.PotrzebneCzesci", new[] { "idCzesci_id" });
            DropIndex("dbo.DostepneCzesci", new[] { "idMagazynu_id" });
            DropIndex("dbo.DostepneCzesci", new[] { "idCzesci_id" });
            DropIndex("dbo.Auta", new[] { "idKlienta_id" });
            DropTable("dbo.Mechanicy");
            DropTable("dbo.Zlecenia");
            DropTable("dbo.PotrzebneCzesci");
            DropTable("dbo.Magazyny");
            DropTable("dbo.DostepneCzesci");
            DropTable("dbo.Czesci");
            DropTable("dbo.Klienci");
            DropTable("dbo.Auta");
        }
    }
}
