namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublicIdToTournament : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournaments", "PublicId", c => c.String(nullable: false));

            Sql("Insert into Numberings(Id, Type) values (NEWID(), 4);"+
                "Update Tournaments Set PublicId = '1' WHERE Id = 'E8F717E6-E9AC-42AD-8023-AC7BD309E66F';"+
                "Update Tournaments Set PublicId = '2' WHERE Id = 'A2DC2E03-E7D8-49A0-9D11-C308961D8264';"+
                "Update Tournaments Set PublicId = '3' WHERE Id = '5F099904-82B0-4473-AC17-F8344708A0B0'");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tournaments", "PublicId");
        }
    }
}
