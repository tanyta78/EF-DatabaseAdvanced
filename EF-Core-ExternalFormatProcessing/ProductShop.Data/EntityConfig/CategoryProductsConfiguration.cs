namespace ProductShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    
    internal class CategoryProductsConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(k => new
            {
                k.CategoryId,k.ProductId
            });
            
            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.CategoriesProducts)
                .HasForeignKey(cp => cp.ProductId);

            builder.HasOne(cp => cp.Category)
                .WithMany(c => c.CategoryProducts)
                .HasForeignKey(cp => cp.CategoryId);
        }
    }
}