namespace Photography.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    
    public class WorkshopConfig : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Name).IsRequired();
            
            builder.Property(e => e.Location).IsRequired();
            
            builder.Property(e => e.PricePerParticipant).IsRequired();
            
            builder.Property(e => e.TrainerId).IsRequired();

            builder.Property(e => e.StartDate).IsRequired(false);
            
            builder.Property(e => e.EndDate).IsRequired(false);

            builder.HasOne(e => e.Trainer)
                .WithMany(t => t.Trainings)
                .HasForeignKey(e => e.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}