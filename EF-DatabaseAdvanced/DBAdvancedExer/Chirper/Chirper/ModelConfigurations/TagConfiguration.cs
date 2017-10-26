namespace Chirper.ModelConfigurations
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            this.HasKey(t => t.TagRef)
                .HasMany(t => t.Chirps)
                .WithMany(c => c.Tags)
                .Map(m =>
                {
                    m.ToTable("ChirpTags");
                    m.MapLeftKey("TagRef");
                    m.MapRightKey("ChirpId");
                });
        }
    }
}
