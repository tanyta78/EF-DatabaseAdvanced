namespace Organazer.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Models.Contacts;

    internal sealed class Configuration : DbMigrationsConfiguration<Organazer.Data.OrganazerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Organazer.Data.OrganazerContext";
        }

        protected override void Seed(Organazer.Data.OrganazerContext context)
        {
            context.People.AddOrUpdate(p => new { p.FirstName, p.LastName },
                 new Person()
                 {
                     FirstName = "Ivan",
                     LastName = "Ivanov"
                 },
                 new Person()
                 {
                     FirstName = "Pesho",
                     LastName = "Petrov"
                 },
                 new Person()
                 {
                     FirstName = "Georgi",
                     LastName = "Georgiev"
                 },
                 new Person()
                 {
                     FirstName = "Grigor",
                     LastName = "Grigorov"
                 },
                 new Person()
                 {
                     FirstName = "Petkan",
                     LastName = "Petkanov"
                 },
                 new Person()
                 {
                     FirstName = "Katya",
                     LastName = "Kadieva"
                 },
                 new Person()
                 {
                     FirstName = "Asen",
                     LastName = "Asenov"
                 },
                 new Person()
                 {
                     FirstName = "Kosta",
                     LastName = "Kostov"
                 },
                 new Person()
                 {
                     FirstName = "Anton",
                     LastName = "Antonov"
                 },
                 new Person()
                 {
                     FirstName = "Iliya",
                     LastName = "Iliev"
                 },
                 new Person()
                 {
                     FirstName = "Maria",
                     LastName = "Ivanova"
                 },
                 new Person()
                 {
                     FirstName = "Stanka",
                     LastName = "Krasimirova"
                 },
                 new Person()
                 {
                     FirstName = "Ognyqn",
                     LastName = "Ognyanov"
                 });
            context.SaveChanges();

        }
    }
}
