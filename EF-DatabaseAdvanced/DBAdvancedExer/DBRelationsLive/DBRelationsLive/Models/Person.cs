namespace DBRelationsLive.Models
{
    using System.Collections.Generic;

    public class Person
    {
        public Person()
        {
            this.Comments=new HashSet<Comment>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public virtual Town PlaceOfBirth { get; set; }
               
        public virtual Town CurrentResidence { get; set; }

    }
}
