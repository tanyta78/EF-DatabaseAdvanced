
namespace FastFood.Data
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(30);

            builder.Property(e => e.Age).IsRequired();

           
            builder.HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId);
        }
    }
}