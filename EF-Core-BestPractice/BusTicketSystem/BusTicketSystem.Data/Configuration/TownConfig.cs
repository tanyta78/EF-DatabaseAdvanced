namespace BusTicketSystem.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.HasMany(t => t.BusStations)
                .WithOne(bs => bs.Town)
                .HasForeignKey(bs => bs.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.HomeTownCustomers)
                .WithOne(c => c.HomeTown)
                .HasForeignKey(c => c.HomeTownId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}