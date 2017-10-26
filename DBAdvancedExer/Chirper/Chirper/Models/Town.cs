namespace Chirper.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public Town()
        {
            this.Natives = new HashSet<Person>();
            this.Residents = new HashSet<Person>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Person> Natives { get; set; }

        public virtual ICollection<Person> Residents { get; set; }

    }
}
