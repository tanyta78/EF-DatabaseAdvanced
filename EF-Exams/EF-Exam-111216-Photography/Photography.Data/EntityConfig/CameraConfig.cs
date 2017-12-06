namespace Photography.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    
    public class CameraConfig : IEntityTypeConfiguration<Camera>
    {
        public void Configure(EntityTypeBuilder<Camera> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Make).IsRequired();

            builder.Property(c => c.Model).IsRequired();

            builder.Property(c => c.MinISO).IsRequired();

            builder.Property(c => c.MaxISO).IsRequired(false);

            builder.Property(c => c.IsFullFrame).IsRequired(false);

            builder.HasMany(c => c.PrimaryCameraPhotographers)
                .WithOne(p => p.PrimaryCamera)
                .HasForeignKey(p => p.PrimaryCameraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.SecondaryCameraPhotographers)
                .WithOne(p => p.SecondaryCamera)
                .HasForeignKey(p => p.SecondaryCameraId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}