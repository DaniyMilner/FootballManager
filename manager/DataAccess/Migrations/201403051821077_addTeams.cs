namespace DataAccess.Migrations.easygenerator.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTeams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        ShortName = c.String(nullable: false, maxLength: 20),
                        Logo = c.String(nullable: false),
                        CoachId = c.Guid(),
                        AssistantId = c.Guid(),
                        Stadium = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        Country_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Country_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Teams", new[] { "Country_Id" });
            DropTable("dbo.Teams");
        }
    }
}
