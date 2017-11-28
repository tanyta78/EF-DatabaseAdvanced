namespace BusTicketSystem.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.TicketId);

            builder.HasOne(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Trip)
                .WithMany(c=>c.Tickets)
                .HasForeignKey(t => t.TripId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}