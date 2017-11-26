namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;

    public class UserService : IUserService
    {
        private readonly PhotoShareContext db;

        public UserService(PhotoShareContext db)
        {
            this.db = db;
        }

        public User FindUserByUsername(string username)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            return user;
        }

        public string RegisterUser(string username, string password, string repeatedPassword, string email)
        {
            if (Session.User != null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }
            
            var checkUser = FindUserByUsername(username);

            if (checkUser != null)
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            if (password != repeatedPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }


            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };


            db.Users.Add(user);
            db.SaveChanges();


            return "User " + username + " was registered successfully!";
        }

        public string PrintFriendsList(string username)
        {
            var user = this.FindUserByUsername(username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var friends = user.FriendsAdded.Select(f => "-" + f.Friend.Username).ToList();


            if (friends.Count == 0)
            {
                return "No friends for this user. :(";
            }

            var result = "Friends:" + Environment.NewLine + string.Join(Environment.NewLine, friends);

            return result;
        }

        public string DeleteUser(string username)
        {
            var user = this.FindUserByUsername(username);

            this.CheckCredentialsByUsername(username);

            user.IsDeleted = true;

            db.SaveChanges();

            return $"User {username} was deleted successfully!";

        }

        private void CheckCredentialsByUsername(string username)
        {
            var user = this.FindUserByUsername(username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (user.IsDeleted != null && user.IsDeleted.Value)
            {
                throw new InvalidOperationException($"User {username} is already deleted!");
            }

            if (Session.User.Username != user.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }
        }

        public string ModifyUser(string username, string property, string newValue)
        {

            var user = FindUserByUsername(username);

            CheckCredentialsByUsername(username);

            ModifyUserProperty(property, newValue, user);

            db.SaveChanges();

            return $"User {username} {property} is {newValue}.";

        }

        private void ModifyUserProperty(string property, string newValue, User user)
        {
            switch (property)
            {
                case "password":
                    if (!newValue.Any(c => Char.IsLower(c))
                        || !newValue.Any(c => Char.IsDigit(c)))
                    {
                        throw new ArgumentException(
                            $"Value {newValue} not valid." + Environment.NewLine + "Invalid Password");
                    }

                    user.Password = newValue;
                    break;
                case "borntown":
                    var bornTown = db.Towns.FirstOrDefault(t => t.Name == newValue);

                    if (bornTown == null)
                    {
                        throw new ArgumentException($"Value {newValue} not valid." + Environment.NewLine + $"Town {newValue} not found!");
                    }

                    user.BornTown = bornTown;
                    break;
                case "currenttown":
                    var currentTown = db.Towns.FirstOrDefault(t => t.Name == newValue);

                    if (currentTown == null)
                    {
                        throw new ArgumentException($"Value {newValue} not valid." + Environment.NewLine + $"Town {newValue} not found!");
                    }

                    user.CurrentTown = currentTown;
                    break;
                default:
                    throw new ArgumentException($"Property {property} not supported!");

            }
        }

        public string Login(string username, string password)
        {
            var user = this.FindUserByUsername(username);

            CheckCredentialsByPasswordAndUser(password, user);

            if (Session.User == user)
            {
                throw new ArgumentException("You should logout first!");
            }

            Session.User = user;

            db.SaveChanges();


            return "User " + username + " successfully logged in!";
        }

        private static void CheckCredentialsByPasswordAndUser(string password, User user)
        {
            if (user == null)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            if (user.Password != password)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            if (user.IsDeleted != null && (bool)user.IsDeleted)
            {
                throw new ArgumentException("Invalid username or password!");
            }


        }

        public string Logout()
        {
            var user = Session.User;

            if (user == null)
            {
                throw new ArgumentException("You should log in first in order to logout.");
            }

            var username = user.Username;
            Session.User = null;

            db.SaveChanges();


            return "User " + username + " successfully logged out!";
        }


    }
}
