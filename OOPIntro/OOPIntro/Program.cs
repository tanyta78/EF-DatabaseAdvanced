namespace OOPIntro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Program
    {
        public static void Main(string[] args)
        {
            //check person prop
            //Type personType = typeof(Person);
            //PropertyInfo[] properties = personType.GetProperties
            //    (BindingFlags.Public | BindingFlags.Instance);
            //Console.WriteLine(properties.Length);

            //check consructors
            //Type personType = typeof(Person);
            //ConstructorInfo emptyCtor = personType.GetConstructor(new Type[] { });
            //ConstructorInfo ageCtor = personType.GetConstructor(new[] { typeof(int) });
            //ConstructorInfo nameAgeCtor = personType.GetConstructor(new[] { typeof(string), typeof(int) });
            //bool swapped = false;
            //if (nameAgeCtor == null)
            //{
            //    nameAgeCtor = personType.GetConstructor(new[] { typeof(int), typeof(string) });
            //    swapped = true;
            //}

            //string name = Console.ReadLine();
            //int age = int.Parse(Console.ReadLine());
            //Person basePerson = (Person)emptyCtor.Invoke(new object[] { });
            //Person personWithAge = (Person)ageCtor.Invoke(new object[] { age });
            //Person personWithAgeAndName = swapped ? (Person)nameAgeCtor.Invoke(new object[] { age, name }) : (Person)nameAgeCtor.Invoke(new object[] { name, age });

            //Console.WriteLine("{0} {1}", basePerson.Name, basePerson.Age);
            //Console.WriteLine("{0} {1}", personWithAge.Name, personWithAge.Age);
            //Console.WriteLine("{0} {1}", personWithAgeAndName.Name, personWithAgeAndName.Age);

            var number = int.Parse(Console.ReadLine());
            var people = new List<Person>();

            for (int i = 0; i < number; i++)
            {
                var input = Console.ReadLine().Split(' ').ToArray();
                var person = new Person(input[0],int.Parse(input[1]));
                people.Add(person);
            }


            foreach (var person in people.Where(p=>p.Age>30).OrderBy(p=>p.Name))
            {
                Console.WriteLine($"{person.Name} - {person.Age}");
            }
           

        }
    }
}
