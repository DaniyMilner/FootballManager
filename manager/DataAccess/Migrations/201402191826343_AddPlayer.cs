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

            Sql("Insert into Positions values ('C7C41812-D6BA-42FE-88B5-D93965D40DD8','GK','Голкипер');" +
                "Insert into Positions values ('51126600-164A-49BD-8C23-BB81A1D9D922','DEF','Защитник');" +
                "Insert into Positions values ('11683223-3908-4739-BE94-EC969C6A35BB','MID','Полузащитник');" +
                "Insert into Positions values ('B114C1DB-9663-4CF1-9290-1739BA6476BF','FW','Форвард');" +
                "Insert into Countries values ('47A4EC4D-D649-4374-B43F-3056BED72DD3','UA','Ukraine');" +
                "Insert into Countries values ('04AB5363-0806-41D0-A7EF-876BE34196B1','RU','Russia');" +
                "Insert into Countries values ('8D302B08-E550-4F3C-9158-85D82A96FE99','BY','Belarus');" +
                "Insert into Countries values ('F7079996-24E5-41A2-9E9B-EBC2DC680DD4','PL','Poland');" +
                "Insert into Countries values ('BF2061D0-4230-49F0-830A-92F2C016BE38','EN','England');" +
                "Insert into Countries values ('99CA397E-5E32-4C90-87BD-35AB2D843BAD','FR','France');" +
                "Insert into Countries values ('23773B84-68DE-4B96-8954-66499E5F521C','DE','Germany');" +
                "Insert into Countries values ('9E734C1D-8F25-4137-9F06-7FEBA439EA00','IT','Italy');" +
                "Insert into Countries values ('FA00FF8D-89F8-4E1B-AB55-A1A83F1F8F8B','ES','Spain');" +
                "Insert into Countries values ('12F3F161-F78D-4182-9C0D-4ED458D2539F','PT','Portugal');" +
                "Insert into Illnesses values ('08E94207-3A21-4CF2-9BF1-A3795442CA68','healthy', 0)");

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
