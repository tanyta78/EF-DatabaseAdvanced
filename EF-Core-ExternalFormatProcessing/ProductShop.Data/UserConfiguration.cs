namespace ProductShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(false);

            builder.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.HasMany(u => u.BoughtProducts)
                .WithOne(p => p.Buyer)
                .HasForeignKey(p => p.BuyerId);
            
            builder.HasMany(u => u.SoldProducts)
                .WithOne(p => p.Seller)
                .HasForeignKey(p => p.SellerId);
        }
    }
}