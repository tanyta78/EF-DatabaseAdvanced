namespace Chirper
{
    using System.Data.Entity;
    using Migrations;
    using ModelConfigurations;
    using Models;

    public class ChirperContext : DbContext
    {
        public ChirperContext()
            : base("name=ChirperContext")
        {
           // Database.SetInitializer(new MyInitializer()); seed v my initializer

            //pri seed v configurations - този сийд го прави при всяка миграция и при всяко стартиране на приложението
          // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChirperContext,Configuration>());
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Chirp> Chirps { get; set; }

        public virtual DbSet<ChImg> ChImgs { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Town> Towns { get; set; }

        public virtual DbSet<Person> People { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Configurations.Add(new ChirpConfiguration());

            modelBuilder.Configurations.Add(new ChImgConfiguration());

            modelBuilder.Configurations.Add(new TagConfiguration());

            modelBuilder.Configurations.Add(new CommentConfiguration());

            modelBuilder.Entity<Person>()
                .HasOptional(p => p.PlaceOfBirth)
                .WithMany(t => t.Natives)
                .HasForeignKey(t => t.PlaceOfBirthId);

            modelBuilder.Entity<Person>()
                .HasOptional(p => p.CurrentResidence)
                .WithMany(t => t.Residents)
                .HasForeignKey(t => t.CurrentResidenceId);



            base.OnModelCreating(modelBuilder);
        }
    }
}