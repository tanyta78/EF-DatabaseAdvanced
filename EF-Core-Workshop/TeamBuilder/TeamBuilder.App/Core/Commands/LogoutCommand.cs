namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class LogoutCommand:ICommand
    {
        private readonly IUserService userService;

        public LogoutCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(string cmd, params string[] args)
        {
            // Validate input length
            Check.CheckLength(0, args);

            //check is logged in user

            if (Session.User==null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
            
            return this.userService.Logout();
        }
    }
}
