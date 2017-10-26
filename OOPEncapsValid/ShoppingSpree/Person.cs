namespace ShoppingSpree
{
    using System;
    using System.Collections.Generic;

    public class Person
    {
        private string name;
        private decimal money;

        public Person(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;
            this.BagOfProducts=new List<Product>();
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (value.Equals(String.Empty))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                this.name = value;
            }
        }

        public decimal Money
        {
            get { return this.money; }
           private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                this.money = value;
            }
        }

        public List<Product> BagOfProducts { get; set; }

        public string TryBuyProduct(Product product)
        {
            if (product.Price <= this.Money)
            {
                this.BagOfProducts.Add(product);
                this.Money -= product.Price;
                return $"{this.Name} bought {product.Name}";
            }
            else
            {
                return $"{this.Name} can't afford {product.Name}";
            }
        }
    }
}
