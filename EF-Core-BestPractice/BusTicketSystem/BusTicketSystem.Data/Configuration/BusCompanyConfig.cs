namespace BusTicketSystem.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class BusCompanyConfig : IEntityTypeConfiguration<BusCompany>
    {
        public void Configure(EntityTypeBuilder<BusCompany> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.CompanyReviews)
                .WithOne(r => r.BusCompany)
                .HasForeignKey(r => r.BusCompanyId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
        }
    }
}