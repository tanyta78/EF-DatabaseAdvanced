namespace P03_UserDetailsOrganizer.ScreenControllers
{
    using Input;
    using P01_BillsPaymentSystem.Data;
    using ScreenElements;

    public abstract class ScreenController
    {
        protected BillsPaymentSystemContext context;
        protected ScreenElement root;
        protected KeyboardInput parser;

        public ScreenController(KeyboardInput parser, BillsPaymentSystemContext context)
        {
            this.context = context;
            this.parser = parser;
        }

        public ScreenController(ScreenElement root, KeyboardInput parser, BillsPaymentSystemContext context)
        {
            this.context = context;
            this.root = root;
            this.parser = parser;
        }

        public void Print()
        {
            System.Console.SetCursorPosition(0, 0);
            this.root.Print();
        }
    }
}
