namespace EFCodeFirstExer
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Models;

    public class StartUp
    {
       public static void Main(string[] args)
        {
            //1. DefineClassPerson();
            //2. OldestFamilyMember();
            //3. Students();
           //4. Console.WriteLine(Calculation.Calculate());
           //5. MathUtils
        }

        private static void Students()
        {
            string cmd;
            while ((cmd = Console.ReadLine()) != "End")
            {
                Student current = new Student(cmd);
            }

            Console.WriteLine(Student.NumberOfCreatedStudents);
        }

        private static void OldestFamilyMember()
        {
            var number = int.Parse(Console.ReadLine());
            var family = new Family();

            for (int i = 0; i < number; i++)
            {
                var param = Console.ReadLine().Split(' ').ToArray();
                var currentPerson = new Person(param[0], int.Parse(param[1]));
                family.AddMember(currentPerson);
            }

            var oldest = family.GetOldestMember();
            Console.WriteLine($"{oldest.Name} {oldest.Age}");
        }

        private static void DefineClassPerson()
        {
            var person1 = new Person()
            {
                Name = "Pesho",
                Age = 20
            };

            var person2 = new Person()
            {
                Name = "Gosho",
                Age = 18
            };

            var person3 = new Person()
            {
                Name = "Stamat",
                Age = 43
            };
        }
    }
}
