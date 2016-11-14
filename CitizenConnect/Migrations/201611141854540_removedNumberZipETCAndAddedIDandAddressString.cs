namespace CitizenConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedNumberZipETCAndAddedIDandAddressString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportViews", "PlaceID", c => c.String());
            AddColumn("dbo.ReportViews", "AddressString", c => c.String());
            DropColumn("dbo.ReportViews", "StreetNumber");
            DropColumn("dbo.ReportViews", "StreetName");
            DropColumn("dbo.ReportViews", "City");
            DropColumn("dbo.ReportViews", "State");
            DropColumn("dbo.ReportViews", "Zip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReportViews", "Zip", c => c.String());
            AddColumn("dbo.ReportViews", "State", c => c.String());
            AddColumn("dbo.ReportViews", "City", c => c.String());
            AddColumn("dbo.ReportViews", "StreetName", c => c.String());
            AddColumn("dbo.ReportViews", "StreetNumber", c => c.String());
            DropColumn("dbo.ReportViews", "AddressString");
            DropColumn("dbo.ReportViews", "PlaceID");
        }
    }
}
