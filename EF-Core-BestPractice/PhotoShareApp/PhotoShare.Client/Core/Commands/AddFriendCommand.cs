namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class AddFriendCommand:ICommand
    {
        private readonly IFriendshipService friendshipService;

        public AddFriendCommand(IFriendshipService friendshipService)
        {
            this.friendshipService = friendshipService;
        }
        
        
        // AddFriend <username1> <username2>
        //AddFriend username1 add frienship after sending request то username2 in FriendsAdded collection

       
        //friendship user 1 friend 2

        public string Execute(string command,params string[] data)
        {
            if (data.Length !=2)
            {
                throw new InvalidOperationException($"Command {data[0]} not valid");
            }

            var sendingRequestUsername = data[0];
            var receivedRequestUsername = data[1];

            return this.friendshipService.AddFriend(sendingRequestUsername, receivedRequestUsername);
        }
    }
}
