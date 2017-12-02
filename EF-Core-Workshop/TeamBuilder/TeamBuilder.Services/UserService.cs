namespace TeamBuilder.Services
{
    using System.Collections.Specialized;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;

    public class UserService:IUserService
    {
        private readonly TeamBuilderContext db;

        public UserService(TeamBuilderContext db)
        {
            this.db = db;
        }
        
        public string RegisterUser(string username, string password,  string firstName, string lastName, int age,Gender gender)
        {
            User user = new User()
            {
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Gender = gender
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();

            return $"User {username} was registered successfully!";
        }

        public string Login(string username, string password)
        {
            Session.User = GetUserByCredentials(username, password);
            this.db.SaveChanges();
            return $"User {username} successfully logged in!";
        }

        public string Logout()
        {
            var username = Session.User.Username;
            Session.User = null;
            return $"User {username} successfully logged out!";
        }

        public User GetUserByCredentials(string username, string password)
        {
            var user = this.db.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            return user;
        }

        public string DeleteUser()
        {
            var user = Session.User;
            user.IsDeleted = true;
            this.db.SaveChanges();
            Session.User = null;
            return $"User {user.Username} was deleted successfully!";
        }
    }
}
