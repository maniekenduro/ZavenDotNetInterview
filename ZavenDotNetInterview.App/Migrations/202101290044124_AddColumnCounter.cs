namespace ZavenDotNetInterview.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCounter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Counter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Counter");
        }
    }
}
