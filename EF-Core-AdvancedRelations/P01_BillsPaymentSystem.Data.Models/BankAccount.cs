namespace P01_BillsPaymentSystem.Data.Models
{
    using System;
    using System.Text;

    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public decimal Balance { get; set; }

        public string BankName { get; set; }

        public string SwiftCode { get; set; }

        public int PaymentMetodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"--ID: {BankAccountId}");
            sb.AppendLine($"-- - Balance: {Balance}");
            sb.AppendLine($"-- - Bank: {BankName}");
            sb.AppendLine($"---SWIFT: {SwiftCode}");
            
            return sb.ToString();
        }

        public void Withdraw(decimal amount)
        {
            if (amount > this.Balance)
            {
                throw new InvalidOperationException("Insufficient funds!");
            }
            
            if (amount <= 0)
            {
                throw new InvalidOperationException("Value cannot be negative or zero!");
            }

            this.Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Value cannot be negative or zero!");
            }

            this.Balance += amount;
        }
    }
}
