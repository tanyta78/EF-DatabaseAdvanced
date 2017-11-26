namespace PhotoShare.Client.Core.Commands.Contracts
{
   public interface ICommand
   {
       string Execute(string command, params string[] arguments);
   }
}
