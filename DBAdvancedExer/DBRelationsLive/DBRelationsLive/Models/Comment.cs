namespace DBRelationsLive.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public Person Author { get; set; }

        public string Content { get; set; }

        public virtual Post Post { get; set; }

    }
}
