namespace Migrations
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Migrations;
    using Models;

    public class SalesContext : DbContext
    {
        public SalesContext()
            : base("name=SalesContext")
        {
            Database.SetInitializer<SalesContext>(new MigrateDatabaseToLatestVersion<SalesContext,Configuration>());
        }


        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<StoreLocation> StoreLocations { get; set; }
    }


}