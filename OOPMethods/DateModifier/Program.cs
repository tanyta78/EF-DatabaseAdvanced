namespace DateModifierPr
{
    using System;

    public class Program
    {
       public static void Main()
       {
           var firstDate = Console.ReadLine();
           var secondDate = Console.ReadLine();
            var diff = new DateModifier(firstDate,secondDate);
            Console.WriteLine(diff.Difference);

        }
    }
}
