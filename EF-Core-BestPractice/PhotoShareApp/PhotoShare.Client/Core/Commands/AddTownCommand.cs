namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Models;
    using Data;
    using Services.Contracts;

    public class AddTownCommand:ICommand
    {
         private readonly ITownService townService;

        public AddTownCommand(ITownService townService)
        {
            this.townService = townService;
        }
        
        
        // AddTown <townName> <countryName>
        public string Execute(string command,params string[] data)
        {
            if (data.Length != 2)
            {
                throw new InvalidOperationException($"Command {data[0]} not valid");
            }

            string townName = data[0];
            string country = data[1];

            return this.townService.AddTown(townName, country);
        }
    }
}
