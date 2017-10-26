namespace Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductDefaulDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Products","Description",p=>p.String(defaultValue:"No Description"));
        }
        
        public override void Down()
        {
            AlterColumn("Products", "Description", p => p.String(defaultValue: "Null"));
        }
    }
}
