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
            this.Lens=new List<Len>();
            this.Accessories=new List<Accessory>();
            this.WorkshopsParticipated = new List<PhotographersWorkshop>();
            this.Trainings =new List<Workshop>();
        }
        
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (value.Length>50 || value.Length<2)
                {
                    throw new ArgumentOutOfRangeException();
                }
                this.lastName = value;
            }
        }

        [RegularExpression(@"\+\d{1,3}\/\d{8,10}")]
        public string Phone
        {
            get { return this.phone; }
            set
            {
                Regex rgs = new Regex(@"\+\d{1,3}\/\d{8,10}");
                if (!rgs.IsMatch(value))
                {
                    throw new ArgumentOutOfRangeException();
                }
                this.phone = value;
            }
        }

        public int PrimaryCameraId { get; set; }

        public Camera PrimaryCamera { get; set; }
        
        public int SecondaryCameraId { get; set; }

        public Camera SecondaryCamera { get; set; }

        public ICollection<Len> Lens { get; set; }

        public ICollection<Accessory> Accessories { get; set; }

        public List<Workshop> Trainings { get; set; }

        public ICollection<PhotographersWorkshop> WorkshopsParticipated{ get; set; }
    }
}