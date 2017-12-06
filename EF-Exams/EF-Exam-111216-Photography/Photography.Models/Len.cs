namespace Photography.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Len
    {
       public int Id { get; set; }
        
        public string Make { get; set; }

        public int? FocalLength { get; set; }

        [RegularExpression(@"(^\d*\.\d{1})")]
        public float? MaxAperture { get; set; }

        //•	Compatible With – make of the camera that the lens is compatible with
        public string CompatibleWith { get; set; }

        public int? OwnerId { get; set; }

        public Photographer Owner { get; set; }
    }
}
