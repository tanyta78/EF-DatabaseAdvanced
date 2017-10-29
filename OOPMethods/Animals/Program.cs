namespace Animals
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
       public static void Main()
        {
            var animals = new List<Animal>();
            var command = string.Empty;
            while ((command = Console.ReadLine()) != "Beast!")
            {
                var animalInfo = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    switch (command.ToLower().Trim())
                    {
                        case "cat":
                            animals.Add(new Cat(animalInfo[0], int.Parse(animalInfo[1]), animalInfo[2]));
                            break;

                        case "dog":
                            animals.Add(new Dog(animalInfo[0], int.Parse(animalInfo[1]), animalInfo[2]));
                            break;

                        case "frog":
                            animals.Add(new Frog(animalInfo[0], int.Parse(animalInfo[1]), animalInfo[2]));
                            break;

                        case "kitten":
                            animals.Add(new Kitten(animalInfo[0], int.Parse(animalInfo[1]), "Female"));
                            break;

                        case "tomcat":
                            animals.Add(new Tomcat(animalInfo[0], int.Parse(animalInfo[1]), "Male"));
                            break;

                        default:
                            Console.WriteLine("Invalid input!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            animals.ForEach(a => Console.WriteLine(a));
        }
    }
}
