namespace Stations.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TrainSeatConfiguration : IEntityTypeConfiguration<TrainSeat>
    {
        public void Configure(EntityTypeBuilder<TrainSeat> builder)
        {
            builder.HasKey(ts => ts.Id);

            builder.HasAlternateKey(ts => new { ts.TrainId, ts.SeatingClassId });

            builder.Property(ts => ts.Quantity)
                .IsRequired();

            builder.Property(ts => ts.TrainId)
                .IsRequired();

            builder.Property(ts => ts.SeatingClassId)
                .IsRequired();

            builder.HasOne(ts => ts.Train)
                .WithMany(t => t.TrainSeats)
                .HasForeignKey(tr => tr.TrainId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(ts => ts.SeatingClass)
                .WithMany(sc => sc.TrainSeats)
                .HasForeignKey(ts => ts.SeatingClassId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}