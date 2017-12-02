namespace TeamBuilder.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TeamEventConfig : IEntityTypeConfiguration<TeamEvent>
    {
        public void Configure(EntityTypeBuilder<TeamEvent> builder)
        {
            builder.ToTable("EventTeams");
            
            builder.HasKey(te => new {te.TeamId, te.EventId});

            builder.HasOne(te => te.Team)
                .WithMany(t => t.ParticipatedEvents)
                .HasForeignKey(te => te.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(te => te.Event)
                .WithMany(e => e.ParticipatingTeams)
                .HasForeignKey(te => te.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}