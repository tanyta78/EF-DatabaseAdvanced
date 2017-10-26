namespace EFAdvancedQuering.Data
{
    using System.Data.Entity;
    using Migrations;
    using Models;

    public class QueryContext : DbContext
    {

        public QueryContext()
            : base("name=QueryContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QueryContext, Configuration>());
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Client> Clients { get; set; }

    }


}