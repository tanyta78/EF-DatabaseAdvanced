namespace Photography.Models
{
    using System;
    using System.Collections.Generic;

    public abstract class Camera
    {
        private int minIso;
        
        public Camera()
        {
            this.PrimaryCameraPhotographers=new List<Photographer>();
            this.SecondaryCameraPhotographers=new List<Photographer>();
        }
        
        public int Id { get; set; }
        
        public string Make { get; set; }

        public string Model { get; set; }

        public bool IsFullFrame { get; set; }

        public int MinISO
        {
            get { return this.minIso; }
            set
            {
                if (value<100)
                {
                    throw new ArgumentOutOfRangeException();
                }

                this.minIso = value;
            }
        }

        public int MaxISO { get; set; }

        public ICollection<Photographer> PrimaryCameraPhotographers { get; set; }

        public ICollection<Photographer> SecondaryCameraPhotographers { get; set; }
        
        
    }
}