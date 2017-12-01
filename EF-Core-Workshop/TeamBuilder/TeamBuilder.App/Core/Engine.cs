namespace TeamBuilder.App.Core
{
    using System;
    using System.Linq;

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
                Console.WriteLine("Please insert command!When finish type exit!");
                string input = Console.ReadLine().Trim();
                string[] cmd = input.Split(new []{' ','\t'},StringSplitOptions.RemoveEmptyEntries);
                var commandName = cmd.Length>0 ? cmd.First():String.Empty;
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
