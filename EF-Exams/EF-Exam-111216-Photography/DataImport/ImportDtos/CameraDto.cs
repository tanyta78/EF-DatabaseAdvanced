namespace Photography.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    public class CameraDto
    {
        [Required]
        public string Type { get; set; }
        
        [Required]
        public string Make { get; set; }
        
        [Required]
        public string Model { get; set; }

        public bool? IsFullFrame { get; set; }
        
        [Required]
        public int MinISO { get; set; }

        public int? MaxISO { get; set; }

        public int MaxShutterSpeed { get; set; }

        public string MaxVideoResolution { get; set; }

        public int MaxFrameRate { get; set; }


    }
}