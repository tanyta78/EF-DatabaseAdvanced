namespace Organazer.ScreenControllers
{
    using System;
    using System.Data.Entity;
    using Data;
    using Input;
    using ScreenElements;

    public abstract class ScreenController
    {
        protected OrganazerContext context;
        protected  KeyboardInput parser;
        protected ScreenElement root;

        public ScreenController(OrganazerContext context, KeyboardInput parser, ScreenElement root)
        {
            this.context = context;
            this.parser = parser;
            this.root = root;
        }

        public ScreenController(OrganazerContext context, KeyboardInput parser)
        {
            this.context = context;
            this.parser = parser;
        }

        public void Print()
        {
            Console.SetCursorPosition(0,0);
            this.root.Print();
        }

       
    }
}
