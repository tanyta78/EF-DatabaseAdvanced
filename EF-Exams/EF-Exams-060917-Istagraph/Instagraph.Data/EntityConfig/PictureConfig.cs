namespace Instagraph.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class PictureConfig : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Path)
                .IsRequired();

            builder.HasMany(p => p.Users)
                .WithOne(u => u.ProfilePicture)
                .HasForeignKey(u => u.ProfilePictureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Posts)
                .WithOne(p => p.Picture)
                .HasForeignKey(p => p.PictureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}