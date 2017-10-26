namespace Organazer.Models.Contacts
{
    using System.Collections.Generic;

    public class Person
    {
        public Person()
        {
            this.Emails = new HashSet<Email>();
            this.Phones=new HashSet<Phone>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }

    }
}
