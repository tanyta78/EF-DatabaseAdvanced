namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Models;
    using Services.Contracts;
    using Utilities;

    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }
        
        // RegisterUser <username> <password> <repeat-password> <firstName> <lastName> <age> <gender>
        public string Execute(string cmd,string[] inputArgs)
        {
            // Validate input length
            Check.CheckLength(7, inputArgs);

            // Validate username
            string username = inputArgs[0];
            if (username.Length < Constants.MinUsernameLength || username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }

            // Validate password
            string password = inputArgs[1];
            if (!password.Any(char.IsDigit) || !password.Any(char.IsUpper))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            // Validate repeated password
            string repeatedPassword = inputArgs[2];
            if (password != repeatedPassword)
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.PasswordDoesNotMatch));
            }

            // Validation for first and last name is optional
            string firstName = inputArgs[3];
            string lastName = inputArgs[4];
           

            // Validate age
            int age;
            bool isNumber = int.TryParse(inputArgs[5], out age);

            if (!isNumber || age <= 0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            // Validate gender
            Gender gender;
            bool isGenderValid = Enum.TryParse(inputArgs[6], out gender);

            if (!isGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            // Validate if user is already existing
            if (CommandHelper.IsUserExisting(username))
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }

            //to use user services
          return  this.userService.RegisterUser(username, password, firstName, lastName, age, gender);

            
        }
    }
}
