namespace TeamBuilder.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(25)
                .IsRequired()
                .IsUnicode();

            builder.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode();

        }
    }
}