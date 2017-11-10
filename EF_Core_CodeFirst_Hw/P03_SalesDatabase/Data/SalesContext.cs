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
            builder.Entity<Product>(pr =>
            {
                pr.Property(p => p.Name).HasMaxLength(50).IsUnicode(true);
                pr.Property(p => p.Description).HasMaxLength(250).HasDefaultValue("No description");
                pr.ToTable("Products");
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
                esa.Property(p => p.Date).HasDefaultValueSql("GETDATE()");
            });

            builder.Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany(pr => pr.Sales)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(pr => pr.Sales)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sale>()
                .HasOne(s => s.Store)
                .WithMany(pr => pr.Sales)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
