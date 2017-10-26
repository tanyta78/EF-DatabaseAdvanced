namespace Organazer.ScreenControllers
{
    using Data;
    using Enumerations;
    using Input;
    using Models.Contacts;
    using ScreenElements;
    using ScreenElements.Composite;

    public class ContactDetailsController : ScreenController
    {

        public ContactDetailsController(OrganazerContext context, KeyboardInput parser,Person contact) : base(context, parser)
        {

            var menu = new Menu(12, 1);

            menu.AddItem(12, 0, 15, contact.FirstName);
            menu.AddItem(12, 2, 15, contact.LastName);

            this.root = menu;
        }

        private void OpenEditBox(Label target)
        {
            var editBox = new EditBoxConroller(context,parser,target);
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
