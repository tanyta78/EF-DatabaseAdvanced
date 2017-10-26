namespace Organazer.ScreenControllers
{
    using Data;
    using Enumerations;
    using Input;
    using ScreenElements;

    public class EditBoxConroller:ScreenController
    {
        public EditBoxConroller(OrganazerContext context, KeyboardInput parser, ScreenElement root) : base(context, parser, root)
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

            return ((EditBox)this.root).Content;
        }
    }
}
