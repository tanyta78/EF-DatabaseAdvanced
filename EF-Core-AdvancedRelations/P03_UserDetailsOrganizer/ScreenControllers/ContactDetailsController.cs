namespace P03_UserDetailsOrganizer.ScreenControllers
{
    using System.Linq;
    using Enums;
    using Input;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.Models;
    using ScreenElements;
    using ScreenElements.Composite;

    public class ContactDetailsController : ScreenController
    {
        public ContactDetailsController(KeyboardInput parser, BillsPaymentSystemContext context, User user)
            : base(parser, context)
        {
            var menu = new Menu(12, 1);

            menu.AddItem(12, 0, 15, user.FirstName);
            menu.AddItem(12, 2, 15, user.LastName);
            menu.AddItem(12,4,15,string.Join("||",user.PaymentMethods.Select(pm=>pm.Id)));

            this.root = menu;
        }

        private void OpenEditBox(Label target)
        {
            var editBox = new EditBoxController(target, parser, context);
            editBox.BeginParse();
        }

        public void BeginParse()
        {
            while (true)
            {
                this.Print();
                var command = this.parser.Listen();
                switch (command)
                {
                    case Command.MoveUp:
                        ((Menu)this.root).MoveUp();
                        break;
                    case Command.MoveDown:
                        ((Menu)this.root).MoveDown();
                        break;
                    case Command.Execute:
                        this.OpenEditBox(((Menu)this.root).GetSelected());
                        break;
                    case Command.Back:
                        return;
                }
            }
        }
    }
}
