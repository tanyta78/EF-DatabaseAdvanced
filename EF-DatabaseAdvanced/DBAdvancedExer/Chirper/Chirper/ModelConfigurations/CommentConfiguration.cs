namespace Chirper.ModelConfigurations
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

   public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
          this.HasRequired(c => c.Chirp)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.ChirpRefId);
        }
    }
}
