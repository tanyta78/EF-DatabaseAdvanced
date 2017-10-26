namespace Chirper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedFriends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFriends",
                c => new
                {
                    RequestedUserId = c.Int(nullable: false),
                    AcceptedUserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.RequestedUserId, t.AcceptedUserId })
                .ForeignKey("dbo.Users", t => t.RequestedUserId)
                .ForeignKey("dbo.Users", t => t.AcceptedUserId)
                .Index(t => t.RequestedUserId)
                .Index(t => t.AcceptedUserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.UserFriends", "AcceptedUserId", "dbo.Users");
            DropForeignKey("dbo.UserFriends", "RequestedUserId", "dbo.Users");
            DropIndex("dbo.UserFriends", new[] { "AcceptedUserId" });
            DropIndex("dbo.UserFriends", new[] { "RequestedUserId" });
            DropTable("dbo.UserFriends");
        }
    }
}
