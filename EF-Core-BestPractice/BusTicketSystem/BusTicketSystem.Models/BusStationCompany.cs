namespace BusTicketSystem.Models
{
   public class BusStationCompany
    {
        public int BusStationId { get; set; }

        public BusStation BusStation { get; set; }

        public int CompanyId { get; set; }

        public BusCompany BusCompany { get; set; }
        
    }
}
