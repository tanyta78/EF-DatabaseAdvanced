namespace RowData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Channels;

    public class Program
    {
       public static void Main(string[] args)
       {
           var numberOfCars = int.Parse(Console.ReadLine());
           var cars = new List<Car>();

           for (int i = 0; i < numberOfCars; i++)
           {
               var carInfo = Console.ReadLine().Split(' ');
               var model = carInfo[0];

               var engineSpeed = int.Parse(carInfo[1]);
               var enginePower = int.Parse(carInfo[2]);
                var engine = new Engine(engineSpeed,enginePower);

               var cargoWeight = int.Parse(carInfo[3]);
               var cargoType = carInfo[4];
               var cargo = new Cargo(cargoWeight,cargoType);

                var tires = new List<Tire>();
               for (int j = 5; j < 12; j+=2)
               {
                   var TirePressure = double.Parse(carInfo[j]);
                   var TireAge = int.Parse(carInfo[j+1]);
                    var tire = new Tire(TirePressure,TireAge);
                    tires.Add(tire);
                }

               var car = new Car(model,engine,cargo,tires);
              cars.Add(car);

            }

           var command = Console.ReadLine();
            var selected = new List<Car>();
           switch (command)
           {
               case "fragile":
                   selected = cars.Where(c =>
                   
                       c.Cargo.Type == "fragile" &&
                       c.Tires.Any(t => t.Pressure < 1)

                   ).ToList();
                    
                    break;
               case "flammable":
                   selected = cars.Where(c =>

                       c.Cargo.Type == "flammable" &&
                       c.Engine.Power>250

                   ).ToList();
                    break;

            }

           foreach (var car in selected)
           {
               Console.WriteLine(car.Model);
           }
        }
    }
}
