namespace Instagraph.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasAlternateKey(u => u.Username);

            builder.Property(u => u.Username)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(u => u.ProfilePicture)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.ProfilePictureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}