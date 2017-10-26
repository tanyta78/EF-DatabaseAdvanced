namespace EFAdvancedQuering.Models
{
    using System.Collections.Generic;

    public class Client
    {
        public Client()
        {
            this.Orders=new HashSet<Order>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }
    }
}
