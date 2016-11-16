namespace CitizenConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectViewTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectViews",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        ProjectDescription = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectViews", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectViews", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ProjectViews");
        }
    }
}
