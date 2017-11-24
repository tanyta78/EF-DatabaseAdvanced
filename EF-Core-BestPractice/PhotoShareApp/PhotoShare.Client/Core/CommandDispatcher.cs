namespace PhotoShare.Client.Core
{
    using System;
    using Commands;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            var command = commandParameters[0].ToLower();
            string result;

            switch (command)
            {
                case "registeruser":
                    result = RegisterUserCommand.Execute(commandParameters);
                    break;
                case "addtown":
                    result = AddTownCommand.Execute(commandParameters);
                    break;
                case "modifyuser":
                    result = ModifyUserCommand.Execute(commandParameters);
                    break;
                case "":
                    result = DeleteUser.Execute(commandParameters);
                    break;
                default: throw new InvalidOperationException($"Command {command} not valid!.");


            }

            return result;
        }
    }
}
