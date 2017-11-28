namespace BusTicketSystem.Client.Core.Commands.Contracts
{
   public interface ICommand
   {
       string Execute(string cmd, params string[]args);
   }
}
