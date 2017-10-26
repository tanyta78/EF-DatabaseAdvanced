namespace Organazer.Models.Contacts
{

    public class Email
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
