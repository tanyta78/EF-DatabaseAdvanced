namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class AcceptFriendCommand:ICommand
    {
        private readonly IFriendshipService friendshipService;

        public AcceptFriendCommand(IFriendshipService friendshipService)
        {
            this.friendshipService = friendshipService;
        }


        // AcceptFriend <username1> <username2>
        //AcceptFriend username2 accept frienship after recieve request from username1. User2 add conection 12 in AddedAsFriend

        //friendship user 1 friend 2
        public string Execute(string command, params string[] data)
        {
            if (data.Length != 2)
            {
                throw new InvalidOperationException($"Command {data[0]} not valid");
            }

            var requesterUsername = data[0];
            var accepterUsername = data[1];

            return this.friendshipService.AcceptFriend(requesterUsername, accepterUsername);
        }
    }
}
