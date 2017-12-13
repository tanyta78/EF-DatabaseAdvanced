
namespace FastFood.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(c => c.Items)
                .WithOne(i => i.Category)
                .HasForeignKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired();
        }
    }
}
