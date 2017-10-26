namespace Chirper
{
    using System.Data.Entity;

    public class MyInitializer : DropCreateDatabaseAlways<ChirperContext>
    {
        protected override void Seed(ChirperContext context)
        {
            base.Seed(context);
        }
    }
}
