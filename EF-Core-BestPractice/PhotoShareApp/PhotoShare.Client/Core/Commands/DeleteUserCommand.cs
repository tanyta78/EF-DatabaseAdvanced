namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class DeleteUserCommand:ICommand
    {
        private readonly IUserService userService;

        public DeleteUserCommand(IUserService userService)
        {
            this.userService = userService;
        }
        
        // DeleteUser <username>
        public string Execute(string command,params string[] data)
        {
            if (data.Length != 1)
            {
                throw new InvalidOperationException($"Command {data[0]} not valid");
            }

            string username = data[0];

            return this.userService.DeleteUser(username);
        }
    }
}
