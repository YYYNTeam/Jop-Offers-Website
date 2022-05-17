namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditApplyForJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyForJobs", "UserName", c => c.String(maxLength: 128));
            CreateIndex("dbo.ApplyForJobs", "UserName");
            AddForeignKey("dbo.ApplyForJobs", "UserName", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJobs", "UserName", "dbo.AspNetUsers");
            DropIndex("dbo.ApplyForJobs", new[] { "UserName" });
            DropColumn("dbo.ApplyForJobs", "UserName");
        }
    }
}
