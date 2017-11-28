namespace BusTicketSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Trip
    {
        public Trip()
        {
            this.Tickets=new List<Ticket>();
        }
        
        public int Id { get; set; }

        public DateTime DepartureTime { get; set; }
        
        public DateTime ArrivalTime { get; set; }

        public Status Status { get; set; }

        public int OriginBusStationId { get; set; }
        public BusStation OriginBusStation { get; set;}

        public int DestinationBusStationId { get; set; }
        public BusStation DestinationBusStation { get; set;}

        public int BusCompanyId { get; set; }
        public BusCompany BusCompany { get; set; }

        public ICollection<Ticket> Tickets { get; set; }


    }
}
