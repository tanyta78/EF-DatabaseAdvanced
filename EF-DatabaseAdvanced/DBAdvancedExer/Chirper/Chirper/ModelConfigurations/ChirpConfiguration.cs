namespace Chirper.ModelConfigurations
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    public class ChirpConfiguration : EntityTypeConfiguration<Chirp>
    {
        public ChirpConfiguration()
        {
            this.HasKey(e => e.Id);

            this.Property(c => c.Content)
                 .HasColumnName("ChirpContent");

            this.Property(p => p.Content)
                .HasMaxLength(140).IsRequired();
        }
    }
}
