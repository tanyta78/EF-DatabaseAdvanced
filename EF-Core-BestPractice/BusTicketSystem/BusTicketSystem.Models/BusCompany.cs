namespace BusTicketSystem.Models
{
    using System.Collections.Generic;

    public class BusCompany
    {
        public BusCompany()
        {
            this.CompanyReviews=new List<Review>();
            this.Trips=new List<Trip>();
            this.OperatingStations=new List<BusStationCompany>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        //maybe depend on customer reviews
        public double Rating { get; set; }

        public ICollection<Review> CompanyReviews { get; set; }

        public ICollection<Trip> Trips { get; set; }

        public ICollection<BusStationCompany> OperatingStations { get; set; }
    }
}
