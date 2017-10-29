namespace OldestFamilyMember
{
    using System.Collections.Generic;
    using System.Linq;

    public class Family
    {
        public Family()
        {
           this.Members = new List<Person>();
        }

        public ICollection<Person> Members { get; set; }

        public void AddMember(Person member)
        {
            this.Members.Add(member);
        }

        public Person GetOldestMember()
        {
            var oldest = this.Members.OrderByDescending(p => p.Age).FirstOrDefault();
            return oldest;
        }

    }
}
