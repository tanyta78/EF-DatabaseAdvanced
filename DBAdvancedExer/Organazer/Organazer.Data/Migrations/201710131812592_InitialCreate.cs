namespace Organazer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
           /* CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Alias = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            */
        }
        
        public override void Down()
        {
            /*
            DropForeignKey("dbo.Phones", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Emails", "PersonId", "dbo.People");
            DropIndex("dbo.Phones", new[] { "Person_Id" });
            DropIndex("dbo.Emails", new[] { "PersonId" });
            DropTable("dbo.Phones");
            DropTable("dbo.People");
            DropTable("dbo.Emails");*/
        }
    }
}
