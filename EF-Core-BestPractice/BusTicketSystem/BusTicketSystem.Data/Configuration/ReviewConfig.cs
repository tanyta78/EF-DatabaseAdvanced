namespace BusTicketSystem.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bc => bc.BusCompany)
                .WithMany(r => r.CompanyReviews)
                .HasForeignKey(r => r.BusCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

           /* builder.HasOne(bc => bc.BusStation)
                .WithMany(bs => bs.Reviews)
                .HasForeignKey(r => r.BusStationId)
                .OnDelete(DeleteBehavior.Restrict);*/

            builder.Property(r => r.PublishingDate)
                .HasDefaultValueSql("GetDate()");
        }
    }
}