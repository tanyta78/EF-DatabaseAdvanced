namespace Chirper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetTextSettings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Chirps", "ChirpContent", c => c.String(nullable: false, maxLength: 140));
            AlterColumn("dbo.Users", "Alias", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Alias", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Chirps", "ChirpContent", c => c.String(maxLength: 140));
        }
    }
}
