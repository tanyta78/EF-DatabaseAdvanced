namespace Photography.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PhotographerConfig : IEntityTypeConfiguration<Photographer>
    {
        public void Configure(EntityTypeBuilder<Photographer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName).IsRequired();

            builder.Property(e => e.LastName).IsRequired();

            builder.Property(e => e.PrimaryCamera)
                .IsRequired();

            builder.Property(e => e.SecondaryCamera).IsRequired();

            builder.HasOne(e => e.PrimaryCamera)
                .WithMany(c => c.PrimaryCameraPhotographers)
                .HasForeignKey(e => e.PrimaryCameraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.SecondaryCamera)
                .WithMany(c => c.SecondaryCameraPhotographers)
                .HasForeignKey(e => e.SecondaryCameraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Accessories)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Trainings)
                .WithOne(t => t.Trainer)
                .HasForeignKey(t => t.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}