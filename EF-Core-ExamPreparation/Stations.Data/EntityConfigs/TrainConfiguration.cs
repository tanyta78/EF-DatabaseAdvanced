namespace Stations.Data.EntityConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TrainConfiguration : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasAlternateKey(t => t.TrainNumber);

            builder.Property(t => t.TrainNumber)
                .HasMaxLength(10)
                .IsRequired();

            builder.HasMany(t => t.Trips)
                .WithOne(tr => tr.Train)
                .HasForeignKey(tr => tr.TrainId)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}