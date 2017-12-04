namespace Photography.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PhotographerWorkshopsConfig : IEntityTypeConfiguration<PhotographersWorkshop>
    {
       public void Configure(EntityTypeBuilder<PhotographersWorkshop> builder)
        {
            builder.HasKey(e => new {e.PhotographerId, e.WorkshopId});

            builder.HasOne(e => e.Photographer)
                .WithMany(p => p.WorkshopsParticipated)
                .HasForeignKey(e => e.PhotographerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Workshop)
                .WithMany(w => w.Participants)
                .HasForeignKey(e => e.WorkshopId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}