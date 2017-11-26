namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class ModifyUserCommand:ICommand
    {
        private readonly IUserService userService;

        public ModifyUserCommand(IUserService userService)
        {
            this.userService = userService;
        }
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string command,params string[] data)
        {
            if (data.Length != 3)
            {
                throw new InvalidOperationException($"Command {data[0]} not valid");
            }
            string username = data[0];
            string property = data[1].ToLower();
            string newValue = data[2];

            return this.userService.ModifyUser(username, property, newValue);
        }
    }
}
