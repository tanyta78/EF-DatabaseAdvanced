namespace ProductShop.Data
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    using Microsoft.EntityFrameworkCore;

    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            //builder.HasMany(p => p.CategoriesProducts)
            //    .WithOne(cp => cp.Product)
            //    .HasForeignKey(cp => cp.ProductId);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.BuyerId)
                .IsRequired(false);
        }
    }
}