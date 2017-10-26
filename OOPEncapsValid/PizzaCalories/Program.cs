namespace PizzaCalories
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            string input;


            while ((input = Console.ReadLine()) != "END")
            {
                var args = input.Split();
                try
                {
                    switch (args[0])
                    {
                        case "Dough":
                            var dough = new Dough(args[1], args[2], double.Parse(args[3]));
                            Console.WriteLine($"{dough.GetCalories():f2}");
                            break;

                        case "Topping":
                            var topping = new Topping(args[1], double.Parse(args[2]));
                            Console.WriteLine($"{topping.GetCalories():f2}");
                            break;

                        case "Pizza":
                            MakePizza(args);
                            return;

                       }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

            }

        }

        private static void MakePizza(string[] args)
        {
            
            var pizza = new Pizza(args[1]);
            var doughInfo = Console.ReadLine().Split(' ');
            var dough = new Dough(doughInfo[1], doughInfo[2], double.Parse(doughInfo[3]));
            pizza.Dough = dough;

            while (true)
            {
                var topInfo = Console.ReadLine().Split(' ');
                if (topInfo[0]=="END")
                {
                    break;
                }
                var topping = new Topping(topInfo[1], double.Parse(topInfo[2]));
                if (pizza.NumberOfToppings==10)
                {
                    throw new ArgumentException("Number of toppings should be in range [0..10].");
                }
                pizza.AddTopping(topping);
            }

            Console.WriteLine($"{pizza.Name} - {pizza.GetCalories():f2} Calories.");
        }
    }
}
