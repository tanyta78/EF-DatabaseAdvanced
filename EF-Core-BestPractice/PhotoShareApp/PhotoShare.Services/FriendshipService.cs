namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FriendshipService : IFriendshipService
    {
        private readonly PhotoShareContext db;

        public FriendshipService(PhotoShareContext db)
        {
            this.db = db;
        }


        public string AcceptFriend(string requesterUsername, string accepterUsername)
        {
            // AcceptFriend <username1> <username2>
            //AcceptFriend username2 accept frienship after recieve request from username1. User2 add conection 12 in AddedAsFriend

            CheckCredentials(accepterUsername);

            var requesterUser = db.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(af => af.Friend)
                    .FirstOrDefault(u => u.Username == requesterUsername);

            var accepterUser = db.Users
                .Include(u => u.AddedAsFriendBy)
                .ThenInclude(fa => fa.Friend)
                .FirstOrDefault(u => u.Username == accepterUsername);

            CheckUserExist(requesterUsername, requesterUser);
            CheckUserExist(accepterUsername, accepterUser);


            //check frienship 12 exist in username1 FriendsAdded and in username2 AddedAsFriendBy

            CheckFriendship(requesterUsername, accepterUsername, requesterUser, accepterUser);

            var frienship12 = new Friendship()
            {
                User = accepterUser,
                Friend = requesterUser
            };

            accepterUser.AddedAsFriendBy.Add(frienship12);

            db.SaveChanges();

            return $"{accepterUsername} accepted  {requesterUsername} as a friend";

        }

        private static void CheckUserExist(string username, User user)
        {
            if (user == null)
            {
                throw new ArgumentException($"{username} not found!");
            }

        }

        private static void CheckFriendship(string requesterUsername, string accepterUsername, User requesterUser, User accepterUser)
        {
            bool requesterAlreadySendRequest = requesterUser.FriendsAdded.Any(u => u.Friend == accepterUser);

            bool accepterAlreadyFriends = accepterUser.AddedAsFriendBy.Any(u => u.Friend == requesterUser);
            //check they are already friends

            if (requesterAlreadySendRequest && accepterAlreadyFriends)
            {
                throw new InvalidOperationException($"{accepterUsername} is already a friend to {requesterUsername}");
            }

            //check is friendship 12 exist in user1 FriendAdded -  was request sent

            if (!requesterAlreadySendRequest)
            {
                throw new InvalidOperationException($"{requesterUsername} has not added{accepterUsername} as a friend");
            }
        }

        private static void CheckCredentials(string username)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials! ");
            }

            if (username != Session.User.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }
        }

        public string AddFriend(string sendingRequestUsername, string receivedRequestUsername)
        {
            // AddFriend <username1> <username2>
            //AddFriend username1 add frienship after sending request то username2 in FriendsAdded collection
            CheckCredentials(sendingRequestUsername);

            var sendingRequestUser = db.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(af => af.Friend)
                    .FirstOrDefault(u => u.Username == sendingRequestUsername);

            var receivedRequestUser = db.Users
                .Include(u => u.AddedAsFriendBy)
                .ThenInclude(u => u.Friend)
                .FirstOrDefault(u => u.Username == receivedRequestUsername);


            CheckUserExist(sendingRequestUsername, sendingRequestUser);
            CheckUserExist(receivedRequestUsername, receivedRequestUser);

            bool senderAlreadySendRequest = sendingRequestUser.FriendsAdded.Any(u => u.Friend == receivedRequestUser);


            bool receiverAlreadyAcceptRequest = receivedRequestUser.AddedAsFriendBy.Any(u => u.Friend == sendingRequestUser);

            //check frienship 12 exist in username1 FriendsAdded and in username2 AddedAsFriendBy
            if (senderAlreadySendRequest && receiverAlreadyAcceptRequest)
            {
                throw new InvalidOperationException($"{receivedRequestUsername} is already a friend to {sendingRequestUsername}");
            }

            if (!receiverAlreadyAcceptRequest && senderAlreadySendRequest)
            {
                throw new InvalidOperationException($"{sendingRequestUsername} already send a request to {receivedRequestUsername}");
            }

            var frienship12 = new Friendship()
            {
                User = sendingRequestUser,
                Friend = receivedRequestUser
            };


            sendingRequestUser.FriendsAdded.Add(frienship12);

            db.SaveChanges();



            return $"Friend {receivedRequestUsername} added to {sendingRequestUsername}";
        }
    }
}
