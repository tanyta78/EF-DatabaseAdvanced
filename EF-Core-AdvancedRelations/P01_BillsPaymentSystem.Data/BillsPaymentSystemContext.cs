namespace P01_BillsPaymentSystem.Data
{
    using EntityConfig;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BillsPaymentSystemContext:DbContext
    {
        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder
        )
        {
            builder.ApplyConfiguration(new BankAccountConfiguration());
            
            base.OnModelCreating(builder);
        }
    }
}
