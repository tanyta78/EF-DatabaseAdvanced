namespace ProductShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ProductShopDbContext:DbContext
    {
        public ProductShopDbContext()
        {
            
        }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server = DESKTOP-LAHCAG9\SQLEXPRESS; Database = Shop; Integrated Security = True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CategoryProductsConfiguration());
        }
    }
}
