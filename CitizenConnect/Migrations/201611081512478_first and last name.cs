namespace CitizenConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstandlastname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "userFirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "userLastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "userLastName");
            DropColumn("dbo.AspNetUsers", "userFirstName");
        }
    }
}
