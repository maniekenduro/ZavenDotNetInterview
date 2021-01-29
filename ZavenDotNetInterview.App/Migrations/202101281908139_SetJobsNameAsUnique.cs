namespace ZavenDotNetInterview.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetJobsNameAsUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Jobs", "Name", unique: true, name: "JobName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Jobs", "JobName");
        }
    }
}
