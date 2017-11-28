namespace BusTicketSystem.Models
{
    using System;

    public class BankAccount
    {
        private double balance;
        
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public double Balance
        {
            get => this.balance;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException($"Value must be positive number!");
                }
                this.balance = value;
            }
        }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public void Withdraw(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount should not be negative.");
            }
            if (amount > this.Balance)
            {
                throw new ArgumentException("Insufficient funds!");
            }
            this.Balance -= amount;
        }

        public void Deposit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount should not be negative.");
            }
            this.Balance += amount;
        }
    }
}
