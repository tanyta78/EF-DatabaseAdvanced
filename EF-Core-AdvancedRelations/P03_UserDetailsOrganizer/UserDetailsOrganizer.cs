namespace P03_UserDetailsOrganizer
{
    using Input;
    using P01_BillsPaymentSystem.Data;
    using ScreenControllers;

    public class UserDetailsOrganizer
    {
        private BillsPaymentSystemContext context;
        private KeyboardInput kbInput;
        private HomeScreenController homeScreen;

        public UserDetailsOrganizer(BillsPaymentSystemContext context)
        {
            this.context = context;
            this.kbInput = new KeyboardInput();
            this.homeScreen = new HomeScreenController(this.kbInput, context);
        }

        public void Start()
        {
            this.homeScreen.BeginParse();

            // Clean up, if necessary
        }
    }
}