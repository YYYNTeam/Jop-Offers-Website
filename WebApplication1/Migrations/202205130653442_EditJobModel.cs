namespace Jop_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditJobModel : DbMigration
    {
        public override void Up()
        {

            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobContent = c.String(),
                        JobImg = c.String(),
                        CategoryId = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.CategoryId)
                .Index(t => t.UserID);
            

            
        }
        
        public override void Down()
        {
   
            DropForeignKey("dbo.Jobs", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Jobs", "CategoryId", "dbo.Categories");

            DropIndex("dbo.Jobs", new[] { "UserID" });
            DropIndex("dbo.Jobs", new[] { "CategoryId" });
 
            DropTable("dbo.Jobs");
        }
    }
}
