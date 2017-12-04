namespace ProductShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //builder.HasMany(c => c.CategoryProducts)
            //    .WithOne(cp => cp.Category)
            //    .HasForeignKey(cp => cp.CategoryId);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}