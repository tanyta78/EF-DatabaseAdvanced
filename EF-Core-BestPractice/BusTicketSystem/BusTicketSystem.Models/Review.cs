namespace BusTicketSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int Id { get; set; }

        public string Content { get; set; }

        [Range(1,10,ErrorMessage = "Please give grade between 1 and 10")]
        public double Grade { get; set; }

       // public int BusStationId { get; set; }
       // public BusStation BusStation { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public int BusCompanyId { get; set; }
        public BusCompany BusCompany { get; set; }

        public DateTime PublishingDate { get; set; }
    }
}
