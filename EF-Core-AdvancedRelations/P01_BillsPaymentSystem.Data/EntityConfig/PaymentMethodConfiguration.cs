namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    
   public class PaymentMethodConfiguration:IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(e => e.Id);

            /*  builder.HasAlternateKey(e => new {e.UserId, e.BankAccountId, e.CreditCardId});- но е ключ и не може да приема null,за това го правим с индекс*/

            builder.HasIndex(e => new { e.UserId, e.BankAccountId, e.CreditCardId }).IsUnique();

            builder
                .HasOne(e => e.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.BankAccount)
                .WithOne(ba => ba.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e => e.BankAccountId);

            builder.HasOne(e => e.CreditCard)
                .WithOne(ba => ba.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e => e.CreditCardId);
            
        }
    }
}
