namespace BusTicketSystem.Client.Core.Commands
{
    using System;
    using Contracts;
   public class exitCommand:ICommand
    {
        public string Execute(string cmd, params string[] args)
        {
            Console.WriteLine("Thanks for using our product!");
            Environment.Exit(0);
            return "";
        }
    }
}
