namespace Photography.Models
{
    using System;
    using System.Text.RegularExpressions;

    public class Len
    {
        private float maxAperture;

        public int Id { get; set; }
        
        public string Make { get; set; }

        public int FocalLength { get; set; }

        public float MaxAperture
        {
            get { return this.maxAperture; }
            set
            {
                Regex rgs = new Regex(@"(^\d*\.\d{1})");
                if (!rgs.IsMatch(value.ToString()))
                {
                    throw new ArgumentOutOfRangeException();
                }

                this.maxAperture = value;
            }
        }

        //•	Compatible With – make of the camera that the lens is compatible with
        public string CompatibleWith { get; set; }

        public int? OwnerId { get; set; }

        public Photographer Owner { get; set; }
    }
}
