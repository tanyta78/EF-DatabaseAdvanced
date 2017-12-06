namespace Photography.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Camera
    {
        private int minIso;
        
        public Camera()
        {
            this.PrimaryCameraPhotographers=new List<Photographer>();
            this.SecondaryCameraPhotographers=new List<Photographer>();
        }
        
        public int Id { get; set; }
        
        [Required]
        public string Make { get; set; }
        
        [Required]
        public string Model { get; set; }

        public bool? IsFullFrame { get; set; }
        
        [Required]
        [Range(100,Int32.MaxValue)]
        public int MinISO { get; set; }
      
        public int? MaxISO { get; set; }

        public ICollection<Photographer> PrimaryCameraPhotographers { get; set; }

        public ICollection<Photographer> SecondaryCameraPhotographers { get; set; }
        
        
    }
}