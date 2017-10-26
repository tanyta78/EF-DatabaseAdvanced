namespace Organazer.ScreenControllers
{
    using System.Linq;
    using Data;
    using Enumerations;
    using Input;
    using Models.Contacts;
    using ScreenElements.Composite;

    public class ContactsController : ScreenController
    {

        public ContactsController(OrganazerContext context, KeyboardInput parser) : base(context, parser)
        {
            var listBox = new ScrollList(1, 1, 11, 9);
            foreach (var person in context.People.ToList())
            {
                listBox.AddItem(person.FirstName + " " + person.LastName, person.Id);
            }

            this.root = new ContactList(0, 0, listBox);
        }

        private void OpenDetails(Person contact)
        {
            var details = new ContactDetailsController(context, parser, contact);
            details.BeginParse();
        }

        public void BeginParse()
        {
            while (true)
            {
                int refId = ((ContactList)this.root).GetSelected();
                var current = context.People.Find(refId);

                string[] details = new string[]
                {
                    current.FirstName,
                    current.LastName,
                    //$"Email:      {current.Emails.FirstOrDefault().Text}",
                };
                ((ContactList)this.root).SetDetails(details);

                this.Print();
                var command = this.parser.Listen();
                switch (command)
                {
                    case Command.MoveUp:
                        ((ContactList)root).MoveUp();
                        break;
                    case Command.MoveDown:
                        ((ContactList)root).MoveDown();
                        break;
                    case Command.Execute:
                        this.OpenDetails(current);
                        break;
                    case Command.Back:
                        return;
                }
            }
        }
    }
}
