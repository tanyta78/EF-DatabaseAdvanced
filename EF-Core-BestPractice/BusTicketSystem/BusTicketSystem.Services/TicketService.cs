namespace BusTicketSystem.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class TicketService:ITicketService
    {
        private readonly BusTicketSystemContext db;

        public TicketService(BusTicketSystemContext db)
        {
            this.db = db;
        }


        public string BuyTicket(int customerId, int tripId, double price, string seat)
        {

            var customer = this.db.Customers
                .Include(c=>c.BankAccounts)
                .FirstOrDefault(c => c.Id == customerId);

            if (customer==null)
            {
                throw new ArgumentException($"No such customer in db!");
            }

            var trip = this.db.Trips.FirstOrDefault(t => t.Id == tripId);

            if (trip == null)
            {
                throw new ArgumentException($"No such trip in db!");
            }

            if (price<=0)
            {
                throw new ArgumentException("Invalid price!");
            }

            var account = customer.BankAccounts.FirstOrDefault();

            if (account==null)
            {
                throw new ArgumentException("No bank account for this customer");
            }
            
            if (account.Balance<price)
            {
                throw new ArgumentException($"Insufficient amount of money for customer {customer.FullName} with bank account number {account.AccountNumber}");
            }

            var ticket = new Ticket
            {
                Customer = customer,
                Price = price,
                Seat = seat,
                Trip = trip
            };
            
            account.Withdraw(price);
            this.db.Tickets.Add(ticket);
            this.db.SaveChanges();

            var result = $"Customer {customer.FullName} bought ticket for trip {trip.Id} for {price} on seat {seat}";
            
            return result;
        }
    }
}
