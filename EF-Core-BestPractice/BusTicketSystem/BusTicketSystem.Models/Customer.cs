namespace BusTicketSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Customer
    {
        public Customer()
        {
            this.Reviews=new List<Review>();
            this.Tickets=new List<Ticket>();
            this.BankAccounts=new List<BankAccount>();
        }
        
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public int HomeTownId { get; set; }
        public Town HomeTown { get; set; }

       //make it one to many because of seed problems
        public ICollection<BankAccount> BankAccounts { get; set; }

        public ICollection<Review> Reviews { get; set; }
        
        public ICollection<Ticket> Tickets { get; set; }
        
       
    }
}
