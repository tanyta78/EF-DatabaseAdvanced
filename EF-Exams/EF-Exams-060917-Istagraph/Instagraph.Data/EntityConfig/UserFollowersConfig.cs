namespace Instagraph.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class UserFollowersConfig : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.HasKey(uf => new {uf.UserId, uf.FollowerId});

            builder.HasOne(uf => uf.User)
                .WithMany(u => u.Followers)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uf => uf.Follower)
                .WithMany(u => u.UsersFollowing)
                .HasForeignKey(u => u.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}