namespace P03_UserDetailsOrganizer.ScreenControllers
{
    using Enums;
    using Input;
    using P01_BillsPaymentSystem.Data;
    using ScreenElements;

    public class EditBoxController : ScreenController
    {
        public EditBoxController(ScreenElement root, KeyboardInput parser, BillsPaymentSystemContext context)
            : base(root, parser, context)
        {
        }

        public string BeginParse()
        {
            bool active = true;
            while (active)
            {
                this.Print();
                var command = this.parser.Listen();
                switch (command)
                {
                    case Command.Back:
                        active = false;
                        break;
                }
            }

            return ((EditBox) this.root).Content;
        }
    }
}