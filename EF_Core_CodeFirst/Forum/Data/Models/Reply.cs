namespace Forum.Data.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class Reply
    {
        public Reply()
        {
            
        }

        public Reply(string content,  Post post, User author)
        {
            this.Content = content;
            this.Author = author;
            this.Post = post;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

    }
}
