namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    
    public class FootballBettingContext:DbContext
    {
        public FootballBettingContext()
        {
            
        }

        public FootballBettingContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions  { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
           if (!builder.IsConfigured)
                {
                    builder.UseSqlServer(@"Server=DESKTOP-LAHCAG9\SQLEXPRESS;Database=FootballBookmakerSystem;Integrated Security=True;");
                }
                base.OnConfiguring(builder);
                }
           
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Team>()
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Team>()
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId);

            builder.Entity<Color>()
                .HasMany(c => c.PrimaryKitTeams)
                .WithOne(t => t.PrimaryKitColor)
                .HasForeignKey(t => t.PrimaryKitColorId);

            builder.Entity<Color>()
                .HasMany(c => c.SecondaryKitTeams)
                .WithOne(t => t.SecondaryKitColor)
                .HasForeignKey(t => t.SecondaryKitColorId);

            builder.Entity<Team>()
                .HasOne(t => t.Town)
                .WithMany(tt => tt.Teams)
                .HasForeignKey(t => t.TownId);

            builder.Entity<Town>()
                .HasMany(tt => tt.Teams)
                .WithOne(t => t.Town)
                .HasForeignKey(t => t.TownId);

            builder.Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Game>()
                .HasOne(g => g.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayTeamId);

            builder.Entity<Town>()
                .HasOne(t => t.Country)
                .WithMany(c => c.Towns)
                .HasForeignKey(c => c.CountryId);

            builder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId);

            builder.Entity<Player>()
                .HasOne(p => p.Position)
                .WithMany(po => po.Players)
                .HasForeignKey(p => p.PositionId);

            builder.Entity<PlayerStatistic>()
                .HasKey(ps => new
                {
                    ps.PlayerId,
                    ps.GameId
                });

            builder.Entity<Player>()
                .HasMany(p => p.PlayerStatistics)
                .WithOne(ps => ps.Player)
                .HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Bet>()
                .HasOne(b => b.Game)
                .WithMany(g => g.Bets)
                .HasForeignKey(b => b.GameId);

            builder.Entity<Bet>(b =>
            {
                b.Property(p => p.Prediction).IsRequired();
            });

            builder.Entity<Game>(g =>
            {
                g.Property(p => p.DrawBetRate).IsRequired();
            });

            builder.Entity<Bet>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(b => b.UserId);
               
            
            base.OnModelCreating(builder);
        }
    }
}
