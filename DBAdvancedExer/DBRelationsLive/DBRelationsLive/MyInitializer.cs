namespace DBRelationsLive
{
    using System.Data.Entity;

   public class MyInitializer:DropCreateDatabaseAlways<RelationLiveContext>
    {
        protected override void Seed(RelationLiveContext context)
        {
            //here i can include info

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
