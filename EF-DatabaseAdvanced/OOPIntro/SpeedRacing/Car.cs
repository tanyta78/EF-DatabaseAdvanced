namespace SpeedRacing
{
    using System;

    public class Car
    {
        public Car(string model, double fuelAmount, double fuelConsumptionFor1km)
        {
           this.Model = model;
           this.FuelAmount = fuelAmount;
           this.FuelConsumptionFor1km = fuelConsumptionFor1km;
           this.DistanceTraveled = 0;
        }

        public string Model { get; set; }
        public double FuelAmount { get; set; }
        public double FuelConsumptionFor1km { get; set; }
        public double DistanceTraveled { get; set; }

        public void Drive(double amountOfKm)
        {
            var neededFuel = amountOfKm * this.FuelConsumptionFor1km;
            if (neededFuel > this.FuelAmount)
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
            else
            {
                this.FuelAmount -= neededFuel;
                this.DistanceTraveled += amountOfKm;
            }
        }

        public override string ToString()
        {
            var result = $"{this.Model} {this.FuelAmount:f2} {this.DistanceTraveled}";
            return result;
        }
    }
}
