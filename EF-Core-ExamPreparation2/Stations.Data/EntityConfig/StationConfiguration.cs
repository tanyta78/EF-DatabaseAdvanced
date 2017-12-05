namespace Stations.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class StationConfiguration : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasAlternateKey(s => s.Name);
                
            builder.Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.Town)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(s => s.TripsFrom)
                .WithOne(t => t.OriginStation)
                .HasForeignKey(s => s.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.TripsTo)
                .WithOne(t => t.DestinationStation)
                .HasForeignKey(s => s.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}