namespace CodeFirstAdv
{
    
    public class Product
    {
        public Product()
        {
            
        }
        public Product(string name, string distributor, string description, decimal price)
        {
            this.Name = name;
            this.Distributor = distributor;
            this.Description = description;
            this.Price = price;
        }

   
        public int Id { get; set; }

        public string Name { get; set; }

        public string Distributor { get; set; }

        public string Description { get; set; }

        public double Weigth { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
