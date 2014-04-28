namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublicIdForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PublicId", c => c.String(nullable: false, defaultValue: "000000"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PublicId");
        }
    }
}
