﻿namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.CategoryProducts=new HashSet<CategoryProduct>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}