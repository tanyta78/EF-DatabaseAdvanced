namespace Organazer.Data
{
    using System.Data.Entity;
    using Migrations;
    using Models.Contacts;

    public class OrganazerContext : DbContext
    {

        public OrganazerContext()
            : base("name=OrganazerContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OrganazerContext,Configuration>());
        }


        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
       
    }


}