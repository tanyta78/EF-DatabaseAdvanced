namespace Chirper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumnName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Chirps", name: "Content", newName: "ChirpContent");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Chirps", name: "ChirpContent", newName: "Content");
        }
    }
}
