namespace FastFood.Data
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.Id);
            
            builder.HasAlternateKey(i => i.Name);

            builder.Property(i => i.Name).IsRequired();

            builder.HasOne(e => e.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}