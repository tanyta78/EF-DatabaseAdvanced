namespace Photography.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class Photographer
    {
        private string lastName;
        private string phone;

        public Photographer()
        {
            this.Lens = new List<Len>();
            this.Accessories = new List<Accessory>();
            this.WorkshopsParticipated = new List<PhotographersWorkshop>();
            this.Trainings = new List<Workshop>();
        }

        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }


        [RegularExpression(@"\+\d{1,3}\/\d{8,10}")]
        public string Phone { get; set; }


        public int PrimaryCameraId { get; set; }
        [Required]
        public Camera PrimaryCamera { get; set; }

        public int SecondaryCameraId { get; set; }
        [Required]
        public Camera SecondaryCamera { get; set; }

        public ICollection<Len> Lens { get; set; }

        public ICollection<Accessory> Accessories { get; set; }

        public List<Workshop> Trainings { get; set; }

        public ICollection<PhotographersWorkshop> WorkshopsParticipated { get; set; }
    }
}