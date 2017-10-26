namespace OOPEncapsValid
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class Program
    {
       public static void Main(string[] args)
        {
            //check Box class
            Type boxType = typeof(Box);
            FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine(fields.Count());

            var length = double.Parse(Console.ReadLine());
            var width = double.Parse(Console.ReadLine());
            var height = double.Parse(Console.ReadLine());

            try
            {
                var box = new Box(length, width, height);
                // Console.WriteLine(box);
                box.SurfaceArea();
                box.LateralSurfaceArea();
                box.Volume();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
           
        }
    }
}
