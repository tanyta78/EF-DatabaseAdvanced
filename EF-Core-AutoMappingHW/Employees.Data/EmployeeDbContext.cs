namespace Employees.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext() { }
        
        public EmployeeDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode()
                    .IsRequired();
                
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode()
                    .IsRequired();

                entity.Property(e => e.Address)
                    .HasMaxLength(200);

                entity.HasOne(d => d.Manager)
                    .WithMany(m => m.Subordinates)
                    .HasForeignKey(d => d.ManagerId);

            });


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ServerConfig.ConnectionString);
            }
        }
    }
}
