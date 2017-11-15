﻿namespace Cars.Data.Models
{
    using System.Collections.Generic;

    public class Engine
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public double Capacity { get; set; }

        public int Cyllinders { get; set; }

        public FuelType FuelType { get; set; }

        public int Horsepower { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();

    }
}