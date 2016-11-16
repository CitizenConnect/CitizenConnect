namespace CitizenConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class debugging : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterestedVolunteers",
                c => new
                    {
                        InterestedUserID = c.Int(nullable: false, identity: true),
                        IfInterested = c.Boolean(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InterestedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.ProjectViews", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InterestedVolunteers", "ProjectID", "dbo.ProjectViews");
            DropForeignKey("dbo.InterestedVolunteers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.InterestedVolunteers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.InterestedVolunteers", new[] { "ProjectID" });
            DropTable("dbo.InterestedVolunteers");
        }
    }
}
