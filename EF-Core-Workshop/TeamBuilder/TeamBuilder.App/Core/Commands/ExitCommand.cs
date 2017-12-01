namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Utilities;

    public class ExitCommand:ICommand
    {
        public string Execute(string cmd, params string[] args)
        {
            Check.CheckLength(0,args);
            Console.WriteLine("Bye!");
            Environment.Exit(0);
            return ""; 
        }
    }
}
