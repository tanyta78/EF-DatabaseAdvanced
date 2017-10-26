namespace DBRelationsLive
{
    using System.Data.Entity;
    using Migrations;
    using Models;

    public class RelationLiveContext : DbContext
    {

        public RelationLiveContext()
            : base("name=RelationLiveContext")
        {
           // Database.SetInitializer(new DropCreateDatabaseAlways<RelationLiveContext>());
           //Database.SetInitializer(new MyInitializer());//когато използвам собствен сийд
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<RelationLiveContext,Configuration>());
        }


        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Town> Towns { get; set; }

    }


}