

namespace FastFood.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    internal class PositionConfig : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder.HasAlternateKey(p => p.Name);

            builder.Property(p => p.Name).IsRequired();

            builder.HasMany(p => p.Employees)
                .WithOne(e => e.Position)
                .HasForeignKey(e => e.PositionId);
        }
    }
}