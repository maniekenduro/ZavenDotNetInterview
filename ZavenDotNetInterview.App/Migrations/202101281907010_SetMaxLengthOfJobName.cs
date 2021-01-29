namespace ZavenDotNetInterview.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetMaxLengthOfJobName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "Name", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "Name", c => c.String(nullable: false));
        }
    }
}
