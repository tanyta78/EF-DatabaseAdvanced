namespace Photography.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PhotographyDbContext:DbContext
    {
        public PhotographyDbContext()
        {
            
        }

        public PhotographyDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ServerConfig.ConnectionString);
            }
        }

        public DbSet<Len> Lens { get; set; }

        public DbSet<Camera> Cameras { get; set; }

        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<Photographer> Photographers { get; set; }

        public DbSet<Workshop> Workshops { get; set; }

        public DbSet<PhotographersWorkshop> PhotorgaphersWorkshops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LenConfig());
            modelBuilder.ApplyConfiguration(new CameraConfig());
            modelBuilder.ApplyConfiguration(new AccessoryConfig());
            modelBuilder.ApplyConfiguration(new PhotographerWorkshopsConfig());
            modelBuilder.ApplyConfiguration(new PhotographerConfig());
        }
    }
}
