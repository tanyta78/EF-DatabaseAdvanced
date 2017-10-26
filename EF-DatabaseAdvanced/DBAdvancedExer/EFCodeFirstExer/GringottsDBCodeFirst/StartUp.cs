namespace GringottsDBCodeFirst
{
   
   public class StartUp
    {
       public static void Main()
        {
            var ctx = new GringottsContext();
            ctx.Database.Initialize(true);
        }
    }
}
