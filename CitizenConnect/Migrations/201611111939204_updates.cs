namespace CitizenConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReportViews", "Longitude", c => c.String());
            AlterColumn("dbo.ReportViews", "Latitude", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReportViews", "Latitude", c => c.Single(nullable: false));
            AlterColumn("dbo.ReportViews", "Longitude", c => c.Single(nullable: false));
        }
    }
}
