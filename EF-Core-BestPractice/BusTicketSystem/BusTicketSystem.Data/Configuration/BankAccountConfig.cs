namespace BusTicketSystem.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Customer)
                .WithMany(c=>c.BankAccounts)
                .HasForeignKey(a=>a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(a => a.AccountNumber)
                .IsRequired()
                .IsUnicode();

            builder.HasIndex(a => a.AccountNumber)
                .IsUnique();

        }
    }
}