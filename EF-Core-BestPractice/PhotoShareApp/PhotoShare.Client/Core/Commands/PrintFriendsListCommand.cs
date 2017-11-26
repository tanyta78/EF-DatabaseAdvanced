namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class PrintFriendsListCommand :ICommand
    {
        private readonly IUserService userService;

        public PrintFriendsListCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // PrintFriendsList <username>
        public string Execute(string command, params string[] data)
        {
            if (data.Length !=1)
            {
                throw new InvalidOperationException($"Command {data[0]} not valid");
            }

            var username = data[0];
            return this.userService.PrintFriendsList(username);
        }
    }
}
