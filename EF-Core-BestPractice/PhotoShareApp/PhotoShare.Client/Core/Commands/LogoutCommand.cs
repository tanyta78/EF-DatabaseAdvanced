namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services;
    using Services.Contracts;

    public class LogoutCommand:ICommand
    {
        private readonly IUserService userService;

        public LogoutCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // Logout 
        public string Execute(string command,params string[] data)
        {
            if (data.Length != 0)
            {
                throw new InvalidOperationException($"Invalid credentials!");
            }

            return this.userService.Logout();
        }
    }
}
