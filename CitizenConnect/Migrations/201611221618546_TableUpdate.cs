namespace CitizenConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectViews", "InterestedUserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectViews", "InterestedUserID");
        }
    }
}
