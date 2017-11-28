namespace BusTicketSystem.Models
{
    using System.Collections.Generic;

    public class BusStation
    {
        public BusStation()
        {
            this.DepartureTrips = new List<Trip>();
            this.ArrivalTrips = new List<Trip>();
          //  this.Reviews=new List<Review>();
            this.OperatingBusCompanies=new List<BusStationCompany>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public ICollection<Trip> DepartureTrips { get; set; }

        public ICollection<Trip> ArrivalTrips { get; set; }

       // public ICollection<Review> Reviews { get; set; }
        
        public ICollection<BusStationCompany> OperatingBusCompanies { get; set; }
    }
}
