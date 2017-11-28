namespace BusTicketSystem.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class buy_ticketCommand:ICommand
    {
        private readonly ITicketService ticketService;

        public buy_ticketCommand(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        public string Execute(string cmd, params string[] args)
        {
            if (args.Length != 4)
            {
                throw new InvalidOperationException($"Command {cmd} not valid");
            }

            var customerId = int.Parse(args[0]);
            var tripId = int.Parse(args[1]);
            var price = double.Parse(args[2]);
            var seat = args[3];
            

            return this.ticketService.BuyTicket(customerId,tripId,price,seat);
        }
    }
}
