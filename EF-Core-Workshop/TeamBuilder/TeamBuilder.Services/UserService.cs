namespace TeamBuilder.Services
{
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
    }
}
