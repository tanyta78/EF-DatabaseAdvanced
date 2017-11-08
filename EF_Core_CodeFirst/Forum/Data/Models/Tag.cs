namespace Forum.Data.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        public Tag()
        {
            
        }
        
        public Tag(string name)
        {
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
        
        public ICollection<PostTag> PostTags { get; set; }

    }
}
