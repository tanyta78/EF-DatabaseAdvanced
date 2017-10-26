namespace ShoppingSpree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            try
            {
                List<Person> people = ReadPeopleInput();

                List<Product> products = ReadProductInput();

                while (true)
                {
                    var cmd = Console.ReadLine().Split(' ').ToArray();
                    if (cmd[0] == "END")
                    {
                        break;
                    }

                    var productToBuy = products.FirstOrDefault(pr => pr.Name == cmd[1]);

                    var result = people.FirstOrDefault(p => p.Name == cmd[0]).TryBuyProduct(productToBuy);

                    Console.WriteLine(result);

                }

                PrintBuyerInfo(people);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
               
            }
           



        }

        private static void PrintBuyerInfo(List<Person> people)
        {
            foreach (var person in people)
            {
                Console.Write($"{person.Name} - ");
                if (person.BagOfProducts.Count == 0)
                {
                    Console.WriteLine($"Nothing bought");
                }
                else
                {
                    Console.WriteLine($"{string.Join(", ", person.BagOfProducts.Select(pr => pr.Name).ToList())}");
                }
            }
        }

        private static List<Product> ReadProductInput()
        {
            var productsInfo = Console.ReadLine()
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            var products = new List<Product>();
            foreach (var info in productsInfo)
            {
                var productInfo = info.Split('=').ToArray();
                var product = new Product(productInfo[0], int.Parse(productInfo[1]));
                products.Add(product);
            }

            return products;
        }

        private static List<Person> ReadPeopleInput()
        {
            var peopleInfo = Console.ReadLine()
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            var people = new List<Person>();
            foreach (var info in peopleInfo)
            {
                var personInfo = info.Split('=').ToArray();
                var person = new Person(personInfo[0], int.Parse(personInfo[1]));
                people.Add(person);
            }

            return people;
        }
    }
}
