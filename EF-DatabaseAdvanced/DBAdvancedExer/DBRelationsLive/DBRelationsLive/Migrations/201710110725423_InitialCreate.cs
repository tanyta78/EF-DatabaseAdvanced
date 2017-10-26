namespace DBRelationsLive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Text = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Students",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Content = c.String(),
                    Post_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);

            CreateTable(
                "dbo.Posts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Content = c.String(),
                    Title = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.People",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    PlaceOfBirth_Id = c.Int(),
                    CurrentResidence_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Towns", t => t.PlaceOfBirth_Id)
                .ForeignKey("dbo.Towns", t => t.CurrentResidence_Id)
                .Index(t => t.PlaceOfBirth_Id)
                .Index(t => t.CurrentResidence_Id);

            CreateTable(
                "dbo.Towns",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "CurrentResidence_Id", "dbo.Towns");
            DropForeignKey("dbo.People", "PlaceOfBirth_Id", "dbo.Towns");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Addresses", "Id", "dbo.Students");
            DropIndex("dbo.People", new[] { "CurrentResidence_Id" });
            DropIndex("dbo.People", new[] { "PlaceOfBirth_Id" });
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropTable("dbo.Towns");
            DropTable("dbo.People");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
            DropTable("dbo.Students");
            DropTable("dbo.Addresses");
        }
    }
}
