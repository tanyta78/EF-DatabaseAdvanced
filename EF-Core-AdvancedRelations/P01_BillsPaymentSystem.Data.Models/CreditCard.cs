namespace P01_BillsPaymentSystem.Data.Models
{
    using System;

    public class CreditCard
    {
        public int CreditCardId { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Limit { get; set; }

        public decimal MoneyOwed { get; set; }

        public decimal LimitLeft => this.Limit - this.MoneyOwed;
       
        public int PaymentMetodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            if (amount > this.LimitLeft)
            {
                throw new InvalidOperationException("Insufficient funds!");
            }

            if (amount <= 0)
            {
                throw new InvalidOperationException("Value cannot be zero or negative!");
            }

            this.MoneyOwed += amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Value cannot be zero or negative!");
            }

            this.MoneyOwed -= amount;
        }
    }
}
