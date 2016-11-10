namespace CitizenConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportTypes",
                c => new
                    {
                        ReportTypeID = c.Int(nullable: false, identity: true),
                        ReportTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ReportTypeID);
            
            CreateTable(
                "dbo.ReportViews",
                c => new
                    {
                        ReportID = c.Int(nullable: false, identity: true),
                        Longitude = c.Single(nullable: false),
                        Latitude = c.Single(nullable: false),
                        ReportTypeID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReportID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportViews", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ReportViews", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ReportViews");
            DropTable("dbo.ReportTypes");
        }
    }
}
