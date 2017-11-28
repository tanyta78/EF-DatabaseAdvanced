namespace BusTicketSystem.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.BusCompany)
                .WithMany(c => c.Trips)
                .HasForeignKey(t => t.BusCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.DestinationBusStation)
                .WithMany(bs => bs.ArrivalTrips)
                .HasForeignKey(t => t.DestinationBusStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.OriginBusStation)
                .WithMany(bs => bs.DepartureTrips)
                .HasForeignKey(t => t.OriginBusStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}