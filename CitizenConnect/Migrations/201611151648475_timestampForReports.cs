namespace CitizenConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timestampForReports : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportViews", "TimeStamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReportViews", "TimeStamp");
        }
    }
}
