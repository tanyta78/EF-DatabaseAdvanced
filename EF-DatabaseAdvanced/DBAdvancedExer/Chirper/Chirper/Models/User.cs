namespace Chirper.Models
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Chirps = new HashSet<Chirp>();
            this.FriendRequestsAccepted = new HashSet<User>();
            this.FriendRequestsMade=new HashSet<User>();
        }

        public int Id { get; set; }

        public string Alias { get; set; }

        public virtual ICollection<Chirp> Chirps { get; set; }

        public int CurrentSessionId { get; set; }

        public virtual ICollection<User> FriendRequestsMade { get; set; }

        public virtual ICollection<User> FriendRequestsAccepted { get; set; }
    }
}
