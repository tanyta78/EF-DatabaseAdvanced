namespace TeamBuilder.Data
{
    using EntityConfig;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class TeamBuilderContext:DbContext
    {
        public TeamBuilderContext()
        {
            
        }

        public TeamBuilderContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ServerConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new TeamConfig());
            modelBuilder.ApplyConfiguration(new EventConfig());
            modelBuilder.ApplyConfiguration(new InvitationConfig());
            modelBuilder.ApplyConfiguration(new UserTeamConfig());
            modelBuilder.ApplyConfiguration(new TeamEventConfig());


        }
    }
}
