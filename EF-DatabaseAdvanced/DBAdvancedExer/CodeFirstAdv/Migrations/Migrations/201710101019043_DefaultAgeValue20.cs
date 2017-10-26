namespace Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class DefaultAgeValue20 : DbMigration
    {
        public override void Up()
        {

            AlterColumn("Customers", "Age", a => a.Int(defaultValueSql: "20"));
        }

        public override void Down()
        {

            AlterColumn("Customers", "Age", a => a.Int(defaultValue: 0));
        }


    }
}
