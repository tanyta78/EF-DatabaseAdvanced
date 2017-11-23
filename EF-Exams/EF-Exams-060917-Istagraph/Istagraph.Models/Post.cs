namespace Istagraph.Models
{
    using System.Collections.Generic;

    public class Post
    {
        public Post()
        {
            this.Comments=new HashSet<Comment>();
        }
        
        public int Id { get; set; }

        public string Caption { get; set; }

        public User User { get; set; }

        public Picture Picture { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
