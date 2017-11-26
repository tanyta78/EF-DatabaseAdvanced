namespace PhotoShare.Services.Contracts
{
   public interface IFriendshipService
    {
        string AcceptFriend(string requesterUsername, string accepterUsername);

        string AddFriend(string sendingRequestUsername, string receivedRequestUsername);
    }
}

