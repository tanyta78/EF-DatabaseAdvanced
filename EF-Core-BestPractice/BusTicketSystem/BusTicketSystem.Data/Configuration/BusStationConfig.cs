namespace BusTicketSystem.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    
    internal class BusStationConfig:IEntityTypeConfiguration<BusStation>
    {
        public void Configure(EntityTypeBuilder<BusStation> builder)
        {
            builder.HasKey(bs => bs.Id);

            builder.HasOne(bs => bs.Town)
                .WithMany(t => t.BusStations)
                .HasForeignKey(bs => bs.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(bs => bs.ArrivalTrips)
                .WithOne(tr => tr.DestinationBusStation)
                .HasForeignKey(tr => tr.DestinationBusStationId);
            
            builder.HasMany(bs => bs.DepartureTrips)
                .WithOne(tr => tr.OriginBusStation)
                .HasForeignKey(tr => tr.OriginBusStationId);

        }
    }
}