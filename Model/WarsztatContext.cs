namespace bazadanych.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WarsztatContext : DbContext
    {
        // Your context has been configured to use a 'WarsztatContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'bazadanych.Model.WarsztatContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'WarsztatContext' 
        // connection string in the application configuration file.
        public WarsztatContext()
            : base("name=WarsztatContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<DostepneCzesci> DostepneCzesci { get; set; }
        public DbSet<Magazyn> Magazyny { get; set; }
        public DbSet<Czesc> Czesci { get; set; }
        public DbSet<PotrzebnaCzesc> PotrzebneCzesci { get; set; }
        public DbSet<Mechanik> Mechanicy { get; set; }
        public DbSet<Zlecenie> Zlecenia { get; set; }
        public DbSet<Auto> Auta { get; set; }
        public DbSet<Klient> Klienci { get; set; }
    }

}