namespace Photography.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    
    public class LenConfig : IEntityTypeConfiguration<Len>
    {
        public void Configure(EntityTypeBuilder<Len> builder)
        {
            builder.HasKey(l => l.Id);

            builder.HasOne(l => l.Owner)
                .WithMany(o => o.Lens)
                .HasForeignKey(l => l.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
        }
    }
}