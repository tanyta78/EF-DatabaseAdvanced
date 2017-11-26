namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class ExitCommand:ICommand
    {
        
        public string Execute(string command, params string[] arguments)
        {
            Console.WriteLine("Good Bye!");
            Environment.Exit(0);
            return "";
        }
    }
}
