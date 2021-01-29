namespace ZavenDotNetInterview.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnLastUpdatedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "LastUpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "LastUpdatedAt");
        }
    }
}
