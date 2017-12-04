namespace Photography.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class AccessoryConfig : IEntityTypeConfiguration<Accessory>
    {
        public void Configure(EntityTypeBuilder<Accessory> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Owner)
                .WithMany(o => o.Accessories)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.OwnerId).IsRequired(false);
        }
    }
}