namespace DataAccess.Migrations.easygenerator.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlayer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PublicId = c.String(nullable: false, maxLength: 254),
                        Name = c.String(nullable: false, maxLength: 254),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Illnesses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IllnessName = c.String(nullable: false, maxLength: 254),
                        TimeForRecovery = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PublicId = c.String(nullable: false, maxLength: 254),
                        Name = c.String(nullable: false, maxLength: 254),
                    })
                .PrimaryKey(t => t.Id);

            Sql("Insert into Positions values (NEWID(),'GK','Голкипер');" +
                "Insert into Positions values (NEWID(),'DEF','Защитник');" +
                "Insert into Positions values (NEWID(),'MID','Полузащитник');" +
                "Insert into Positions values (NEWID(),'FW','Форвард');" +
                "Insert into Countries values (NEWID(),'UA','Ukraine');" +
                "Insert into Countries values (NEWID(),'RU','Russia');" +
                "Insert into Countries values (NEWID(),'BY','Belarus');" +
                "Insert into Countries values (NEWID(),'PL','Poland');" +
                "Insert into Countries values (NEWID(),'EN','England');" +
                "Insert into Countries values (NEWID(),'FR','France');" +
                "Insert into Countries values (NEWID(),'DE','Germany');" +
                "Insert into Countries values (NEWID(),'IT','Italy');" +
                "Insert into Countries values (NEWID(),'ES','Spain');" +
                "Insert into Countries values (NEWID(),'PT','Portugal');" +
                "Insert into Illnesses values (NEWID(),'healthy', 0)");

            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Surname = c.String(nullable: false, maxLength: 30),
                        Age = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Growth = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Salary = c.Int(nullable: false),
                        Money = c.Int(nullable: false),
                        Humor = c.Int(nullable: false),
                        Condition = c.Int(nullable: false),
                        PublicId = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Country_Id = c.Guid(nullable: false),
                        Illness_Id = c.Guid(nullable: false),
                        Position_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .ForeignKey("dbo.Illnesses", t => t.Illness_Id, cascadeDelete: true)
                .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Country_Id)
                .Index(t => t.Illness_Id)
                .Index(t => t.Position_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Players", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.Players", "Illness_Id", "dbo.Illnesses");
            DropForeignKey("dbo.Players", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Players", new[] { "User_Id" });
            DropIndex("dbo.Players", new[] { "Position_Id" });
            DropIndex("dbo.Players", new[] { "Illness_Id" });
            DropIndex("dbo.Players", new[] { "Country_Id" });
            DropTable("dbo.Players");
            DropTable("dbo.Positions");
            DropTable("dbo.Illnesses");
            DropTable("dbo.Countries");
        }
    }
}
