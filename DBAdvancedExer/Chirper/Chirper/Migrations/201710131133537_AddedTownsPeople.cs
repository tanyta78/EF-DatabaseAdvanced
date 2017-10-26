namespace Chirper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTownsPeople : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PlaceOfBirthId = c.Int(),
                        CurrentResidenceId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Towns", t => t.CurrentResidenceId)
                .ForeignKey("dbo.Towns", t => t.PlaceOfBirthId)
                .Index(t => t.PlaceOfBirthId)
                .Index(t => t.CurrentResidenceId);
            
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
            DropForeignKey("dbo.People", "PlaceOfBirthId", "dbo.Towns");
            DropForeignKey("dbo.People", "CurrentResidenceId", "dbo.Towns");
            DropIndex("dbo.People", new[] { "CurrentResidenceId" });
            DropIndex("dbo.People", new[] { "PlaceOfBirthId" });
            DropTable("dbo.Towns");
            DropTable("dbo.People");
        }
    }
}
