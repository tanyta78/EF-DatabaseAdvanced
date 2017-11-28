namespace BusTicketSystem.Data
{
    using Configuration;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BusTicketSystemContext : DbContext
    {
        public BusTicketSystemContext()
        {
        }

        public BusTicketSystemContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Town> Towns { get; set; }

        public DbSet<BusStation> BusStations { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<BusCompany> BusCompanies { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<BusStationCompany> StationCompanies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TownConfig());

            modelBuilder.ApplyConfiguration(new BusStationConfig());

            modelBuilder.ApplyConfiguration(new CustomerConfig());

            modelBuilder.ApplyConfiguration(new BankAccountConfig());

            modelBuilder.ApplyConfiguration(new BusCompanyConfig());
            modelBuilder.ApplyConfiguration(new ReviewConfig());
            modelBuilder.ApplyConfiguration(new TripConfig());
            modelBuilder.ApplyConfiguration(new TicketConfig());
            modelBuilder.ApplyConfiguration(new StationCompanyConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ServerConfig.ConnectionString);
        }
    }
}
