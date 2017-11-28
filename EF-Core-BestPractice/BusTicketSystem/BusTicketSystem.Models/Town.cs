namespace BusTicketSystem.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public Town()
        {
            this.BusStations=new List<BusStation>();
            this.HomeTownCustomers=new List<Customer>();
        }
        
        public int Id { get; set; }

        //this will be unique
        public string Name { get; set; }

        public string Country { get; set; }
        
        public ICollection<BusStation> BusStations { get; set; }

        public ICollection<Customer> HomeTownCustomers { get; set; }
    }
}
