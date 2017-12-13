namespace FastFood.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Customer).IsRequired();

            builder.Property(i => i.DateTime).IsRequired();

            builder.Property(i => i.Type).IsRequired();

            builder.HasOne(e => e.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.EmployeeId);
        }
    }
}