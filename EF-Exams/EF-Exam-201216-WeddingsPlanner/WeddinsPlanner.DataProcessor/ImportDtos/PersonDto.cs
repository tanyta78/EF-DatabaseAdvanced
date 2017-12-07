namespace WeddinsPlanner.DataProcessor.ImportDtos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Models.Enums;

    public class PersonDto
    {
        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(1)]
        public string MiddleInitial { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        public Gender Gender { get; set; } = Gender.NotSpecified;

        public DateTime? Birthdate { get; set; }

        public string Phone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+@[a-z]{1,}.[a-z]{1,}$")]
        public string Email { get; set; }


       
    }
}