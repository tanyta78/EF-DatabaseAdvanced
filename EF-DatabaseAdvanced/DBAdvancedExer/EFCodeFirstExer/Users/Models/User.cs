namespace Users.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class User
    {
        private string password;
        private string email;

        [Key]
        [Range(1, 2147483647)]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                //These checks have to be commented in order to work Problem 11 and 12;
                char[] specialSymbols = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '<', '>', '?' };
                if (!value.Any(c=>char.IsLower(c))
                   || !value.Any(c=>char.IsUpper(c))
                   || !value.Any(c => char.IsDigit(c))
                   || !value.Any(c=>specialSymbols.Contains(c)))
                {
                    throw new ArgumentException("The password must contain at least one lower letter, one capital letter, one digit and one special symbol!");
                }

                this.password = value;
            }
        }

        [Required]
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                string regularExpressionString = @"^([a-zA-Z0-9]+(-|\.|_)?)*[a-zA-Z0-9]+@[a-zA-Z0-9]+(-|\.|_)?[a-zA-Z0-9]+((\.)?[a-zA-Z0-9]+)((\.)?[a-zA-Z0-9]+)?$";
                Regex regex = new Regex(regularExpressionString);
                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException("The email is not in the correct format!");
                }

                this.email = value;
            }
        }

        [MaxLength(1024 * 1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }
    }
}
