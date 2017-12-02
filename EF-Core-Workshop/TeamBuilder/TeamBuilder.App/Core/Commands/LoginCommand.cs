namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        //•	Login <username> <password>
        public string Execute(string cmd, params string[] args)
        {
            // Validate input length
            Check.CheckLength(2, args);

            var username = args[0];
            var password = args[1];

            //check is there currently logged in user
            if (Session.User != null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            //check is user exist in db or user.IsDeleted ==true
            //check user with given credentials
            if (!CommandHelper.IsUserExisting(username) || this.userService.GetUserByCredentials(username, password) == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }


            return this.userService.Login(username, password);

        }
    }
}
