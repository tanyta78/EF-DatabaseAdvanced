namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class RegisterUserCommand :ICommand
    {
        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(string command,params string[] data)
        {
            if (data.Length != 4)
            {
                throw new InvalidOperationException($"Command {data[0]} not valid");
            }

            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            return this.userService.RegisterUser(username, password, repeatPassword, email);
        }
    }
}
