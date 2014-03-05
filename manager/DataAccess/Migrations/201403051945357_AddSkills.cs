namespace DataAccess.Migrations.easygenerator.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Ordering = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SkillsPlayers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.Double(nullable: false),
                        Player_Id = c.Guid(nullable: false),
                        Skill_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .Index(t => t.Player_Id)
                .Index(t => t.Skill_Id);

            Sql("Insert into Skills values (NEWID(),'Speed',1);" +
                "Insert into Skills values (NEWID(),'GameByHead',2);" +
                "Insert into Skills values (NEWID(),'ImpactForce',3);" +
                "Insert into Skills values (NEWID(),'Pas',4);" +
                "Insert into Skills values (NEWID(),'Accuracy',5);" +
                "Insert into Skills values (NEWID(),'Selection',6);" +
                "Insert into Skills values (NEWID(),'Dribbling',7)" +
                "Insert into Skills values (NEWID(),'Endurance',13);" +
                "Insert into Skills values (NEWID(),'Reaction',9);" +
                "Insert into Skills values (NEWID(),'Possession',8);" +
                "Insert into Skills values (NEWID(),'PlayingInTheAir',10);" +
                "Insert into Skills values (NEWID(),'Jump',11);" +
                "Insert into Skills values (NEWID(),'Positioning',12);" +
                "Insert into Skills values (NEWID(),'Leadership',14);");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillsPlayers", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.SkillsPlayers", "Player_Id", "dbo.Players");
            DropIndex("dbo.SkillsPlayers", new[] { "Skill_Id" });
            DropIndex("dbo.SkillsPlayers", new[] { "Player_Id" });
            DropTable("dbo.SkillsPlayers");
            DropTable("dbo.Skills");
        }
    }
}
