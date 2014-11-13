namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterMatches : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "HomeGoal", c => c.Int(nullable: false));
            AddColumn("dbo.Matches", "GuestGoal", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "GuestGoal");
            DropColumn("dbo.Matches", "HomeGoal");
        }
    }
}
