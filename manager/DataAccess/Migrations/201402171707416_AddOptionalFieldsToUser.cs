namespace DataAccess.Migrations.easygenerator.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOptionalFieldsToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Birthday", c => c.DateTime());
            AddColumn("dbo.Users", "City", c => c.String());
            AddColumn("dbo.Users", "AboutMySelf", c => c.String());
            AddColumn("dbo.Users", "Sex", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Sex");
            DropColumn("dbo.Users", "AboutMySelf");
            DropColumn("dbo.Users", "City");
            DropColumn("dbo.Users", "Birthday");
        }
    }
}
