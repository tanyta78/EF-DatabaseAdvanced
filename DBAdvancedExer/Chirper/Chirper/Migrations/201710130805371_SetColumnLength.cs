namespace Chirper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetColumnLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Chirps", "ChirpContent", c => c.String(maxLength: 140));
            AlterColumn("dbo.Users", "Alias", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Alias", c => c.String());
            AlterColumn("dbo.Chirps", "ChirpContent", c => c.String());
        }
    }
}
