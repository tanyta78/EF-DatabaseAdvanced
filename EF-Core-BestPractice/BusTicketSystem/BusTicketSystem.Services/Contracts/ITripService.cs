namespace BusTicketSystem.Services.Contracts
{
    using Models;

    public interface ITripService
    {
        string ChangeTripStatus(int tripId, Status status);
    }
}
