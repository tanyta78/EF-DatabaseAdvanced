namespace Organazer.Models.Contacts
{
    public class Phone
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual Person Person { get; set; }
    }
}
