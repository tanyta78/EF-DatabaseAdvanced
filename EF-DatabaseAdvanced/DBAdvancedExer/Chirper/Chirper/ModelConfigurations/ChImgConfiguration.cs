namespace Chirper.ModelConfigurations
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    public class ChImgConfiguration : EntityTypeConfiguration<ChImg>
    {
        public ChImgConfiguration()
        {
            this.HasKey(c => c.ChirpId)
                .HasRequired(c => c.Chirp)
                .WithRequiredDependent(c => c.Image);
        }
    }
}
