namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Services.Contracts;


    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            var databaseInitializeService = this.serviceProvider.GetService<IDatabaseInitializeService>();
            databaseInitializeService.DatabaseInitialize();

            while (true)
            {
                string input = Console.ReadLine().Trim();
                string[] cmd = input.Split(' ');
                var commandName = cmd.First();
                var commandArgs = cmd.Skip(1).ToArray();

                try
                {
                    var command = CommandParser.ParseCommand(this.serviceProvider, commandName);

                    var result = command.Execute(commandName, commandArgs);

                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
