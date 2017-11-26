namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class LoginCommand :ICommand
    {
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // Login <username> <password> 
        public string Execute(string command,params string[] data)
        {
            if (data.Length != 2)
            {
                throw new InvalidOperationException($"Invalid credentials!");
            }

            string username = data[0];
            string password = data[1];


            return this.userService.Login(username, password);
        }
    }
}

