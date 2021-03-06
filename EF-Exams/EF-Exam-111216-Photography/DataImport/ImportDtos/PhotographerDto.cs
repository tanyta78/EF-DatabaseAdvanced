﻿namespace Photography.DataProcessor.ImportDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PhotographerDto
    {
     
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [RegularExpression(@"\+\d{1,3}\/\d{8,10}")]
        public string Phone { get; set; }

        public ICollection<int> Lenses { get; set; }
    }
}
