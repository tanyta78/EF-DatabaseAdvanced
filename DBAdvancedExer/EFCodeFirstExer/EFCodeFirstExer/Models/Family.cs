namespace EFCodeFirstExer.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Family
    {
        private List<Person> members;

        public List<Person> Members { get => members; set => members = value; }

        public Family()
        {
            this.Members = new List<Person>();
        }

        public void AddMember(Person member)
        {
            this.Members.Add(member);
        }

        public Person GetOldestMember()
        {

            var oldest = this.Members.OrderByDescending(m => m.Age).FirstOrDefault();

            return oldest;
        }
    }
}
