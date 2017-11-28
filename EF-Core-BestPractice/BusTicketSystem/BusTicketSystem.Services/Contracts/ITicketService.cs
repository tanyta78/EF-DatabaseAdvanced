namespace BusTicketSystem.Services.Contracts
{
   public interface ITicketService
   {
       string BuyTicket(int customerId, int tripId, double price, string seat);
   }
}
