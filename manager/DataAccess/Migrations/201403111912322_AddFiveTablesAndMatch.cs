namespace DataAccess.Migrations.easygenerator.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFiveTablesAndMatch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arrangements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Scheme = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventLines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LineId = c.Guid(nullable: false),
                        Minute = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Player_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .Index(t => t.Player_Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HomeTeamId = c.Guid(nullable: false),
                        GuestTeamId = c.Guid(nullable: false),
                        EventLineId = c.Guid(nullable: false),
                        FansCount = c.Int(),
                        TicketPrice = c.Int(),
                        DateStart = c.DateTime(nullable: false),
                        Result = c.String(),
                        Weather_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Weathers", t => t.Weather_Id, cascadeDelete: true)
                .Index(t => t.Weather_Id);
            
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayerSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Index = c.Int(),
                        Settings = c.String(),
                        isCaptain = c.Boolean(nullable: false),
                        isWritable = c.Boolean(nullable: false),
                        Match_Id = c.Guid(nullable: false),
                        Player_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matches", t => t.Match_Id, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .Index(t => t.Match_Id)
                .Index(t => t.Player_Id);
            
            CreateTable(
                "dbo.TeamSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Settings = c.String(),
                        LineUp = c.String(),
                        Arrangement_Id = c.Guid(nullable: false),
                        Match_Id = c.Guid(nullable: false),
                        PlayerSend_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Arrangements", t => t.Arrangement_Id, cascadeDelete: true)
                .ForeignKey("dbo.Matches", t => t.Match_Id, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerSend_Id, cascadeDelete: true)
                .Index(t => t.Arrangement_Id)
                .Index(t => t.Match_Id)
                .Index(t => t.PlayerSend_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamSettings", "PlayerSend_Id", "dbo.Players");
            DropForeignKey("dbo.TeamSettings", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.TeamSettings", "Arrangement_Id", "dbo.Arrangements");
            DropForeignKey("dbo.PlayerSettings", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.PlayerSettings", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.Matches", "Weather_Id", "dbo.Weathers");
            DropForeignKey("dbo.EventLines", "Player_Id", "dbo.Players");
            DropIndex("dbo.TeamSettings", new[] { "PlayerSend_Id" });
            DropIndex("dbo.TeamSettings", new[] { "Match_Id" });
            DropIndex("dbo.TeamSettings", new[] { "Arrangement_Id" });
            DropIndex("dbo.PlayerSettings", new[] { "Player_Id" });
            DropIndex("dbo.PlayerSettings", new[] { "Match_Id" });
            DropIndex("dbo.Matches", new[] { "Weather_Id" });
            DropIndex("dbo.EventLines", new[] { "Player_Id" });
            DropTable("dbo.TeamSettings");
            DropTable("dbo.PlayerSettings");
            DropTable("dbo.Weathers");
            DropTable("dbo.Matches");
            DropTable("dbo.EventLines");
            DropTable("dbo.Arrangements");
        }
    }
}
