namespace Cars.Data.Models
{
    using System.Collections.Generic;

    public class Engine
    {
        public int Id { get; set; }
        
        public double Capacity { get; set; }

        public int MakeId { get; set; }

        public Make Make { get; set; }

        public int Horsepower { get; set; }

        public FuelType FuelType { get; set; }
        
        public Transmission Transmission { get; set; }
        
        public ICollection<Car> Cars { get; set; }=new List<Car>();
        
    }
}