namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTournamentTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        CountItems = c.Int(nullable: false),
                        Season_Id = c.Guid(nullable: false),
                        Country_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Season_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TournamentItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ItemNumber = c.Int(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        Tournament_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id, cascadeDelete: true)
                .Index(t => t.Tournament_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tournaments", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.TournamentItems", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Tournaments", "Season_Id", "dbo.Seasons");
            DropIndex("dbo.Tournaments", new[] { "Country_Id" });
            DropIndex("dbo.TournamentItems", new[] { "Tournament_Id" });
            DropIndex("dbo.Tournaments", new[] { "Season_Id" });
            DropTable("dbo.TournamentItems");
            DropTable("dbo.Seasons");
            DropTable("dbo.Tournaments");
        }
    }
}
