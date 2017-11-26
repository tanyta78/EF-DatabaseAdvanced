namespace PhotoShare.Services.Contracts
{
    using Models;

    public interface IUserService
    {
        User FindUserByUsername(string username);

        string RegisterUser(string username, string password, string repeatedPassword, string email);

        string PrintFriendsList(string username);

        string DeleteUser(string username);

        string ModifyUser(string username, string property, string newValue);

        string Login(string username, string password);

        string Logout();
    }
}
