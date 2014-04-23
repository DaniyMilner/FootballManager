namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublicIdToMatch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "PublicId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "PublicId");
        }
    }
}
