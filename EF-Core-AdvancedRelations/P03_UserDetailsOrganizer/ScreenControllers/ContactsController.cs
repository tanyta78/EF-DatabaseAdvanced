namespace P03_UserDetailsOrganizer.ScreenControllers
{
    using System.Linq;
    using Enums;
    using Input;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.DBInitializer.Generators;
    using P01_BillsPaymentSystem.Data.Models;
    using ScreenElements.Composite;

    public class ContactsController : ScreenController
    {
        public ContactsController(KeyboardInput parser, BillsPaymentSystemContext context)
            : base(parser, context)
        {
            var listBox = new ScrollList(1, 1, 11, 9);
            foreach (var user in context.Users.ToList())
            {
                listBox.AddItem(user.FirstName + " " + user.LastName, user.UserId);
            }

            this.root = new ContactsList(0, 0, listBox);
        }

        private void OpenDetails(User user)
        {
            var details = new ContactDetailsController(this.parser, this.context, user);
            details.BeginParse();
        }

        public void BeginParse()
        {
            while (true)
            {
                int refId = ((ContactsList)this.root).GetSelected();
                var current = this.context.Users.Find(refId);

                string[] details = new string[]
                {
                    current.FirstName,
                    current.LastName,
                   $"PaymentMethods:{string.Join("|||",current.PaymentMethods.Where(pm=>pm.Type==PaymentMethodType.BankAccount))}"
                    //$"Email:      {current.Emails.FirstOrDefault().Text}",
                };
                ((ContactsList)this.root).SetDetails(details);

                this.Print();
                var command = this.parser.Listen();
                switch (command)
                {
                    case Command.MoveUp:
                        ((ContactsList)root).MoveUp();
                        break;
                    case Command.MoveDown:
                        ((ContactsList)root).MoveDown();
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
