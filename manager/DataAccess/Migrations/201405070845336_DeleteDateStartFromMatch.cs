namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDateStartFromMatch : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Matches", "DateStart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Matches", "DateStart", c => c.DateTime(nullable: false));
        }
    }
}
