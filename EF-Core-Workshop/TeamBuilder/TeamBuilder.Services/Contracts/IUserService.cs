namespace TeamBuilder.Services.Contracts
{
    using Models;

    public interface IUserService
   {
       string RegisterUser(string username,string password,string firstName,string lastName,int age, Gender gender);

       string Login(string username, string password);

       string Logout();

       User GetUserByCredentials(string username, string password);

       string DeleteUser();
   }
}
