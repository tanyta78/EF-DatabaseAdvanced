namespace Photography.DataProcessor.ImportDtos
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class LenDto
    {
        public string Make { get; set; }

        public int? FocalLength { get; set; }

        public float? MaxAperture { get; set; }

        //•	Compatible With – make of the camera that the lens is compatible with
        public string CompatibleWith { get; set; }

    }
}
