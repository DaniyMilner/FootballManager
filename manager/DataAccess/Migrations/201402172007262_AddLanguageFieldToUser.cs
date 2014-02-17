namespace DataAccess.Migrations.easygenerator.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLanguageFieldToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Language", c => c.String(nullable: false, maxLength: 10, defaultValue: "ru"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Language");
        }
    }
}
