namespace Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultAgain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Sales","Date",s=>s.String(defaultValue:"GETDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("Sales", "Date", s => s.String(defaultValue: "NULL"));
        }
    }
}
