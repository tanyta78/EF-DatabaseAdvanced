namespace Stations.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class SeatingClassConfiguration : IEntityTypeConfiguration<SeatingClass>
    {
        public void Configure(EntityTypeBuilder<SeatingClass> builder)
        {
            builder.HasKey(sc => sc.Id);

            builder.HasAlternateKey(sc => sc.Name);

            builder.HasAlternateKey(sc => sc.Abbreviation);

            builder.Property(sc => sc.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(sc => sc.Abbreviation)
                .HasMaxLength(2)
                .IsRequired();
        }
    }
}