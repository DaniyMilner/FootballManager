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

            Sql("Insert into Skills values ('4A0313D3-7BA0-47B8-B262-09DF4623D4C6','Speed',1);" +
                "Insert into Skills values ('8F23769D-7839-4AEE-880C-0C5D4CE596FD','GameByHead',2);" +
                "Insert into Skills values ('C5CBD0B4-968B-401D-B419-E5352FA1ACE1','ImpactForce',3);" +
                "Insert into Skills values ('93F0C018-BE74-4F31-B697-659DF316F3ED','Pas',4);" +
                "Insert into Skills values ('BDA97D67-7854-49B6-8D68-44DEA6F2038C','Accuracy',5);" +
                "Insert into Skills values ('9093FAC1-FE5D-42BB-B29B-134D03DD89E7','Selection',6);" +
                "Insert into Skills values ('ACB1BBB4-A301-492D-B61B-242455C27C0B','Dribbling',7)" +
                "Insert into Skills values ('15BDBE73-5D95-4BF1-9605-CAC9E3F9EC41','Endurance',13);" +
                "Insert into Skills values ('EFBCEAEC-C9F8-4F77-BAEA-A1256A48758B','Reaction',9);" +
                "Insert into Skills values ('A7722C27-1326-4DE9-991D-FEB068F4F7A1','Possession',8);" +
                "Insert into Skills values ('2E73F75C-22AD-49C2-8B34-2E07D71F502A','PlayingInTheAir',10);" +
                "Insert into Skills values ('BFB6782A-2B34-4C40-B1B5-E7BA4FEF0CA4','Jump',11);" +
                "Insert into Skills values ('53418FFB-C1B6-4B32-A39F-87AA43D46C53','Positioning',12);" +
                "Insert into Skills values ('9C3D8FBD-46B4-410C-B158-2115FE399657','Leadership',14);");
            
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
