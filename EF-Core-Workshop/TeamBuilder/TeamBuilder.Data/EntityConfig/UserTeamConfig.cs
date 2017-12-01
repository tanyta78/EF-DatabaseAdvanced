namespace TeamBuilder.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class UserTeamConfig : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasAlternateKey(ut => new {ut.UserId, ut.TeamId});

            builder.HasOne(ut => ut.User)
                .WithMany(u => u.Teams)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ut => ut.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(ut => ut.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}