namespace Stations.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.OriginStationId).IsRequired();
            builder.Property(t => t.DestinationStationId).IsRequired();
            builder.Property(t => t.DepartureTime).IsRequired();
            builder.Property(t => t.ArrivalTime).IsRequired();

            builder.Property(t => t.Status).HasDefaultValue(TripStatus.OnTime);

            builder.HasMany(t => t.Tickets)
                .WithOne(t => t.Trip)
                .HasForeignKey(t => t.TripId);
        }
    }
}