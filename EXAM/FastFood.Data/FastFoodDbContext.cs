using Microsoft.EntityFrameworkCore;

namespace FastFood.Data
{
    using Models;

    public class FastFoodDbContext : DbContext
	{
		public FastFoodDbContext()
		{
		}

		public FastFoodDbContext(DbContextOptions options)
			: base(options)
		{
		}

	    public DbSet<Employee> Employees { get; set; }

	    public DbSet<Position> Positions { get; set; }

	    public DbSet<Category> Categories { get; set; }

	    public DbSet<Item> Items { get; set; }

	    public DbSet<Order> Orders { get; set; }

	    public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			if (!builder.IsConfigured)
			{
				builder.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
		    builder.ApplyConfiguration(new EmployeeConfig());
		    builder.ApplyConfiguration(new PositionConfig());
		    builder.ApplyConfiguration(new CategoryConfig());
		    builder.ApplyConfiguration(new ItemConfig());
		    builder.ApplyConfiguration(new OrderConfig());
		   
		    builder.Entity<OrderItem>().HasKey(i => new {i.OrderId, i.ItemId});

		    builder.Entity<OrderItem>().HasOne(oi => oi.Order)
		        .WithMany(o => o.OrderItems)
		        .HasForeignKey(oi => oi.OrderId);

		    builder.Entity<OrderItem>().HasOne(oi => oi.Item)
		        .WithMany(i => i.OrderItems)
		        .HasForeignKey(oi => oi.ItemId);
        }
	}

    
}