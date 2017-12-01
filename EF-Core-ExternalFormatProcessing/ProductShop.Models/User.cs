﻿namespace ProductShop.Models
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.BoughtProducts=new HashSet<Product>();
            this.SoldProducts=new HashSet<Product>();
        }
        public int Id { get; set; }

        public int Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Product> BoughtProducts { get; set; }
        public ICollection<Product> SoldProducts { get; set; }

    }
}
