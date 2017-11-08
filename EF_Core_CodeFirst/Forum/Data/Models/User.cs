namespace Forum.Data.Models
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            
        }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }   

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public IEnumerable<Post>Posts { get; set; }=new HashSet<Post>();

        public IEnumerable<Reply> Replies { get; set; }=new HashSet<Reply>();


    }
}
