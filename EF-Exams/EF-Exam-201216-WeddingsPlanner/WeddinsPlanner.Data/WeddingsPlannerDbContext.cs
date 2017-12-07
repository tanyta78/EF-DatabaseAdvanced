namespace WeddinsPlanner.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class WeddingsPlannerDbContext:DbContext
    {
        public WeddingsPlannerDbContext()
        {
            
        }

        public WeddingsPlannerDbContext(DbContextOptions options):base(options)
        {
            
        }
        
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Cash> CashPresents { get; set; }
        public DbSet<Gift> GiftPresents { get; set; }
        public DbSet<WeddingsVenue> WeddingsVenues { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ServerConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeddingsVenue>().HasKey(wv => new {wv.WeddingId, wv.VenueId});

            modelBuilder.Entity<Person>()
                .HasMany(p => p.WeddingBrides)
                .WithOne(w => w.Bride)
                .HasForeignKey(w => w.BrideId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.WeddingBrooms)
                .WithOne(w => w.Bridegroom)
                .HasForeignKey(w => w.BridegroomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Present)
                .WithOne(p => p.Invitation)
                .HasForeignKey<Invitation>(i => i.PresentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
