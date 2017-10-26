namespace Chirper
{
   public class StartUp
    {
       public static void Main()
        {
            var context = new ChirperContext();
            context.Database.Initialize(true);
        }
    }
}
