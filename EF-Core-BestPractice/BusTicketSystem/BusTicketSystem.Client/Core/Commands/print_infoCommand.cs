namespace BusTicketSystem.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;
    using Services;
    
    public class print_infoCommand:ICommand
    {
        private readonly IBusStationService busStationService;

        public print_infoCommand(IBusStationService busStationService)
        {
            this.busStationService = busStationService;
        }
        
        
        
        public string Execute(string cmd, params string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException($"Command {cmd} not valid");
            }

            return this.busStationService.PrintInfo(int.Parse(args[0]));
        }
    }
}
