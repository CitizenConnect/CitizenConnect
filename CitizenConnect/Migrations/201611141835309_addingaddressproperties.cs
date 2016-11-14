namespace CitizenConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingaddressproperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportViews", "StreetNumber", c => c.String());
            AddColumn("dbo.ReportViews", "StreetName", c => c.String());
            AddColumn("dbo.ReportViews", "City", c => c.String());
            AddColumn("dbo.ReportViews", "State", c => c.String());
            AddColumn("dbo.ReportViews", "Zip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReportViews", "Zip");
            DropColumn("dbo.ReportViews", "State");
            DropColumn("dbo.ReportViews", "City");
            DropColumn("dbo.ReportViews", "StreetName");
            DropColumn("dbo.ReportViews", "StreetNumber");
        }
    }
}
