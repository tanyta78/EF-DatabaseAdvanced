namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class BankAccountConfiguration:IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(e => e.BankAccountId);
            
            builder.Property(b => b.BankName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(b => b.SwiftCode)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode(false);

            builder.Ignore(e => e.PaymentMetodId);

            /*  builder.HasOne(b => b.PaymentMethod)
                  .WithOne(pm => pm.BankAccount)
                  .HasForeignKey<BankAccount>(b => b.PaymentMetodId);*/
        }
    }
}
