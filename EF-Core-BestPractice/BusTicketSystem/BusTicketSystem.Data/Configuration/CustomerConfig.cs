namespace BusTicketSystem.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    
   internal class CustomerConfig:IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Gender)
                .IsRequired();
                
            builder.HasMany(c => c.BankAccounts)
                .WithOne(a=>a.Customer)
                .HasForeignKey(a=>a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.HomeTown)
                .WithMany(t => t.HomeTownCustomers)
                .HasForeignKey(c => c.HomeTownId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Reviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(c => c.Tickets)
                .WithOne(t => t.Customer)
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Ignore(c => c.FullName);
        }
    }
}
