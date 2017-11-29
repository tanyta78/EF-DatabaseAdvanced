namespace Employees.App.Core.Commands
{
    using System;
    using Contracts;
    
    public class ExitCommand:ICommand
    {
        public string Execute(params string[] arguments)
        {
            Console.WriteLine("Thanks for using our app!");
            Environment.Exit(0);
            return "";
        }
    }
}
