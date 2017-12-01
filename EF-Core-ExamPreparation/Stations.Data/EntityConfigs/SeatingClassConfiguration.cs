namespace Stations.Data.EntityConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class SeatingClassConfiguration : IEntityTypeConfiguration<SeatingClass>
    {
        public void Configure(EntityTypeBuilder<SeatingClass> builder)
        {
            builder.HasKey(sc => sc.Id);

            builder.HasIndex(sc => sc.Name).IsUnique();

            builder.Property(sc => sc.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(sc => sc.Abbreviation)
                .IsRequired();
            
        }
    }
}