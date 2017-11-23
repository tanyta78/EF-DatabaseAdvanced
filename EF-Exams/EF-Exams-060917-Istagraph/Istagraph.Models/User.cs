namespace Istagraph.Models
{
    using System.Collections.Generic;
    using System.Reflection.Metadata;

    public class User
    {
        public User()
        {
            this.Followers=new HashSet<User>();
            this.Following=new HashSet<User>();
            this.Posts=new HashSet<Post>();
            this.Comments=new HashSet<Comment>();
        }
        
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Picture ProfilePicture { get; set; }

        public ICollection<User> Followers { get; set; }

        public ICollection<User> Following { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
