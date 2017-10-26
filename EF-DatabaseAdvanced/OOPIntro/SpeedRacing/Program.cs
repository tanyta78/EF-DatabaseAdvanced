namespace SpeedRacing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
      public static void Main()
      {
          var numberOfCars = int.Parse(Console.ReadLine());
          var cars = new List<Car>();

          for (int i = 0; i < numberOfCars; i++)
          {
              var input = Console.ReadLine().Split(' ').ToArray();
              var car =new Car(input[0],double.Parse(input[1]),double.Parse(input[2]));
                cars.Add(car);
          }

          while (true)
          {
              var commandLine = Console.ReadLine().Split(' ').ToArray();
              if (commandLine[0]=="End")
              {
                  break;
              }

              var carModel = commandLine[1];
              var amountOfKm = double.Parse(commandLine[2]);
                cars.FirstOrDefault(c => c.Model==carModel)?.Drive(amountOfKm);
          }

          foreach (var car in cars)
          {
              Console.WriteLine(car);
          }
      }
    }
}
