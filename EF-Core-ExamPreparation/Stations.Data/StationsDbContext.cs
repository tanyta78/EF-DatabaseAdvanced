using Microsoft.EntityFrameworkCore;

namespace Stations.Data
{
    using EntityConfigs;
    using Models;

    public class StationsDbContext : DbContext
	{
		public StationsDbContext()
		{
		}

		public StationsDbContext(DbContextOptions options)
			: base(options)
		{
		}

	    public DbSet<SeatingClass> SeatingClasses { get; set; }

	    public DbSet<TrainSeat> TrainSeats { get; set; }

	    public DbSet<Train> Trains { get; set; }

	    public DbSet<Station> Stations { get; set; }

	    public DbSet<Trip> Trips { get; set; }

	    public DbSet<Ticket> Tickets { get; set; }

	    public DbSet<CustomerCard> CustomerCards { get; set; }
	    
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		    modelBuilder.ApplyConfiguration(new StationConfiguration());
		    modelBuilder.ApplyConfiguration(new TrainConfiguration());
		    modelBuilder.ApplyConfiguration(new SeatingClassConfiguration());
		    modelBuilder.ApplyConfiguration(new TrainSeatConfiguration());
		    modelBuilder.ApplyConfiguration(new TripConfiguration());
		    modelBuilder.ApplyConfiguration(new TicketConfiguration());
		    modelBuilder.ApplyConfiguration(new CustomerCardConfiguration());

		}
    }
}