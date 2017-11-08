namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SalesContext:DbContext
    {
        public DbSet<Sale> Sales { get; set; }
        
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Store> Stores { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(ep =>
            {
                ep.Property(p => p.Name).HasColumnType("nvarchar(50)");
                ep.Property(p => p.Description).HasColumnType("varchar(250)").HasDefaultValue("No description");
            });

            builder.Entity<Customer>(ec =>
            {
                ec.Property(p => p.Name).HasColumnType("nvarchar(100)");
                ec.Property(p => p.Email).HasColumnType("varchar(80)");
            });

            builder.Entity<Store>(es =>
            {
                es.Property(p => p.Name).HasColumnType("varchar(80)");
            });

            builder.Entity<Sale>(esa =>
            {
                esa.Property(p => p.Date).HasDefaultValueSql("getdate()");
            });
        }
    }
}
