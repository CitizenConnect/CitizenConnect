namespace CitizenConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportTypes : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ReportViews", "ReportTypeID");
            AddForeignKey("dbo.ReportViews", "ReportTypeID", "dbo.ReportTypes", "ReportTypeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportViews", "ReportTypeID", "dbo.ReportTypes");
            DropIndex("dbo.ReportViews", new[] { "ReportTypeID" });
        }
    }
}
