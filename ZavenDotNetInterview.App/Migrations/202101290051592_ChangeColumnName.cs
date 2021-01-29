namespace ZavenDotNetInterview.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "FailureCounter", c => c.Int(nullable: false));
            DropColumn("dbo.Jobs", "Counter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Counter", c => c.Int(nullable: false));
            DropColumn("dbo.Jobs", "FailureCounter");
        }
    }
}
