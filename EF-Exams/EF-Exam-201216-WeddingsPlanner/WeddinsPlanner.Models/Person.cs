namespace WeddinsPlanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;

    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60,MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(1)]
        public string MiddleNameInitial { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{this.FirstName} {this.MiddleNameInitial} {this.LastName}";

        [Required]
        public Gender Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        [NotMapped]
        public int? Age {
            get
            {
                if (Birthdate == null)
                {
                    return null;
                }

                DateTime now = DateTime.Now;
                int age = now.Year - this.Birthdate.Value.Year;

                if (now.Month < Birthdate.Value.Month ||
                    (now.Month == Birthdate.Value.Month && now.Day < Birthdate.Value.Day))
                {
                    age--;
                }

                return age;
            } }

        public string Phone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+@[a-z]{1,}.[a-z]{1,}$")]
        public string Email { get; set; }


        [InverseProperty("Bride")]
        public List<Wedding> WeddingBrides { get; set; } = new List<Wedding>();

        [InverseProperty("Bridegroom")]
        public List<Wedding> WeddingBrooms { get; set; } = new List<Wedding>();

        public List<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
