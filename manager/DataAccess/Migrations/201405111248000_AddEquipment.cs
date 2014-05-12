namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEquipment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        CountOfMatch = c.Int(nullable: false),
                        AmountOfSkills = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayerEquipment",
                c => new
                    {
                        Equipment_Id = c.Guid(nullable: false),
                        Player_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Equipment_Id, t.Player_Id })
                .ForeignKey("dbo.Equipments", t => t.Equipment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .Index(t => t.Equipment_Id)
                .Index(t => t.Player_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerEquipment", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.PlayerEquipment", "Equipment_Id", "dbo.Equipments");
            DropIndex("dbo.PlayerEquipment", new[] { "Player_Id" });
            DropIndex("dbo.PlayerEquipment", new[] { "Equipment_Id" });
            DropTable("dbo.PlayerEquipment");
            DropTable("dbo.Equipments");
        }
    }
}
