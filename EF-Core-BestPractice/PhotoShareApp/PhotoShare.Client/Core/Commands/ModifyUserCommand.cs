namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Data;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data)
        {
            string username = data[1];
            string property = data[2].ToLower();
            string newValue = data[3];

            using (var db = new PhotoShareContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                
                if (user==null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                string exceptionMessage = $"Value {newValue} not valid." + Environment.NewLine;
                string townError = $"Town {newValue} not found!";

                switch (property)
                {
                    case "password":
                        if (!newValue.Any(c=>Char.IsLower(c))
                            || !newValue.Any(c => Char.IsDigit(c)))
                        {
                            throw new ArgumentException(
                                exceptionMessage + "Invalid Password");
                        }

                        user.Password = newValue;
                        break;
                    case "borntown":
                        var bornTown = db.Towns.FirstOrDefault(t => t.Name == newValue);
                        
                        if (bornTown==null)
                        {
                            throw new ArgumentException(
                               exceptionMessage + townError);
                        }

                        user.BornTown = bornTown;
                        break;
                    case "currenttown":
                        var currentTown = db.Towns.FirstOrDefault(t => t.Name == newValue);

                        if (currentTown == null)
                        {
                            throw new ArgumentException(
                                exceptionMessage + townError);
                        }

                        user.CurrentTown = currentTown;
                        break;
                        default:
                            throw new ArgumentException($"Property {property} not supported!");

                }

                db.SaveChanges();
                return $"User {username} {property} is {newValue}.";
            }
        }
    }
}
