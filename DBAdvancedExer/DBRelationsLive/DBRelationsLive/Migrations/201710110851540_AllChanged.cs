namespace DBRelationsLive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Town_Id", c => c.Int());
            AddColumn("dbo.Comments", "Author_Id", c => c.Int());
            CreateIndex("dbo.Addresses", "Town_Id");
            CreateIndex("dbo.Comments", "Author_Id");
            AddForeignKey("dbo.Addresses", "Town_Id", "dbo.Towns", "Id");
            AddForeignKey("dbo.Comments", "Author_Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.People");
            DropForeignKey("dbo.Addresses", "Town_Id", "dbo.Towns");
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Addresses", new[] { "Town_Id" });
            DropColumn("dbo.Comments", "Author_Id");
            DropColumn("dbo.Addresses", "Town_Id");
        }
    }
}
