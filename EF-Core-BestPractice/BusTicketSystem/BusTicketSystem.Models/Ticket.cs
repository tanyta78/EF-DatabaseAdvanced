namespace BusTicketSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Ticket
    {
        public int TicketId { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public string Seat { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
