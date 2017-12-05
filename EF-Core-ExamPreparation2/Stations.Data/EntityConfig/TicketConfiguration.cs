﻿namespace Stations.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Price)
                .IsRequired();

            builder.Property(t => t.SeatingPlace)
                .IsRequired();

            builder.Property(t => t.TripId)
                .IsRequired();

            builder.Property(t => t.CustomerCardId)
                .IsRequired(false);
        }
    }
}