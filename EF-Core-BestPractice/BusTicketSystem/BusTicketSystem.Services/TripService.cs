namespace BusTicketSystem.Services
{
    using Contracts;
    using Data;
    using Models;

    public class TripService:ITripService
    {

        private readonly BusTicketSystemContext db;

        public TripService(BusTicketSystemContext db)
        {
            this.db = db;
        }

        public string ChangeTripStatus(int tripId, Status status)
        {
            return "";
        }
    }
}
