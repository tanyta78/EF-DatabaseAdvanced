namespace OldestFamilyMember
{
    using System;
    using System.Reflection;

    public class Program
    {
       public static void Main(string[] args)
        {
            MethodInfo oldestMemberMethod = typeof(Family).GetMethod("GetOldestMember");
            MethodInfo addMemberMethod = typeof(Family).GetMethod("AddMember");
            if (oldestMemberMethod == null || addMemberMethod == null)
            {
                throw new Exception();
            }

            var numberOfMembers = int.Parse(Console.ReadLine());
            var family = new Family();
            for (int i = 0; i < numberOfMembers; i++)
            {
                var personInfo = Console.ReadLine().Split(' ');
                var member = new Person(personInfo[0],int.Parse(personInfo[1]));
                family.AddMember(member);
            }

            Console.WriteLine(family.GetOldestMember());

        }
    }
}
