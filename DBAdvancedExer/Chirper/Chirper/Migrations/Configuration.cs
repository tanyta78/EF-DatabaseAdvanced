namespace Chirper.Migrations
{
    using System.Data.Entity.Migrations;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ChirperContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Chirper.ChirperContext";
        }

        protected override void Seed(ChirperContext context)
        {
            var cmt = new User()
            {
                Alias = "Pesho"
            };

            context.Users.AddOrUpdate(u=>u.Alias,cmt);
            context.SaveChanges();
        }
    }
}
