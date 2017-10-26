namespace DBRelationsLive
{
   public class StartUp
    {
       public static void Main()
        {
            RelationLiveContext context=new RelationLiveContext();

            context.Database.Initialize(true);
        }
    }
}
