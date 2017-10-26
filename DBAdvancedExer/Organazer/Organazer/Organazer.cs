namespace Organazer
{
    using Data;
    using Input;
    using ScreenControllers;

    public class Organazer
    {
        private OrganazerContext context;
        private KeyboardInput kbInput;
        private HomeScreenController homeScreen;

        public Organazer(OrganazerContext context)
        {
            this.context = context;
            this.kbInput = new KeyboardInput();
            this.homeScreen = new HomeScreenController(context, this.kbInput);
        }

        public void Start()
        {
            this.homeScreen.BeginParse();

            // Clean up, if necessary
        }
    }
}
