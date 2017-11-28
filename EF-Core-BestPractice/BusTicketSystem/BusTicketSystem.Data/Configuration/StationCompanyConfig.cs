namespace BusTicketSystem.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class StationCompanyConfig : IEntityTypeConfiguration<BusStationCompany>
    {
        public void Configure(EntityTypeBuilder<BusStationCompany> builder)
        {
            builder.HasKey(sc => new {sc.BusStationId, sc.CompanyId});

            builder.HasOne(bsc => bsc.BusStation)
                .WithMany(bs => bs.OperatingBusCompanies)
                .HasForeignKey(bsc => bsc.BusStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bsc => bsc.BusCompany)
                .WithMany(c => c.OperatingStations)
                .HasForeignKey(bsc => bsc.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}