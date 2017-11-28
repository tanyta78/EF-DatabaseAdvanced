namespace BusTicketSystem.Services.Contracts
{
   public interface IReviewService
   {
       string PublishReview(int customerId, double grade, string busCompanyName, string content);

       string PrintReview(int busCompanyId);
   }
}
