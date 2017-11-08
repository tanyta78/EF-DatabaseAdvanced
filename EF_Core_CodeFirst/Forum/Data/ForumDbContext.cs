namespace Forum.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ForumDbContext : DbContext
    {
        public ForumDbContext()
        {

        }

        public ForumDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Reply> Replies { get; set; }
        
        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostsTags { get; set; }


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
            builder.Entity<Category>()
                .HasMany(c => c.Posts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            builder.Entity<Post>()
                .HasMany(p => p.Replies)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);

            builder.Entity<User>()
                .HasMany(u => u.Replies)
                .WithOne(r => r.Author)
                .HasForeignKey(r => r.AuthorId);

            builder.Entity<PostTag>()
                .ToTable("PostsTags")
                .HasKey(pt => new {pt.PostId, pt.TagId});

            builder.Entity<Tag>()
                .ToTable("Tags");

        }
    }
}
