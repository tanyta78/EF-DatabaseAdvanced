namespace Stations.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class CustomerCardConfiguration : IEntityTypeConfiguration<CustomerCard>
    {
        public void Configure(EntityTypeBuilder<CustomerCard> builder)
        {
            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasMany(cc => cc.BoughtTickets)
                .WithOne(t => t.CustomerCard)
                .HasForeignKey(t => t.CustomerCardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(cc => cc.Type).HasDefaultValue(CardType.Normal).IsRequired();
        }

    }
}