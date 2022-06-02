namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manuupdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Manufacturer", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Manufacturer", "Contact", c => c.Int(nullable: false));
        }
    }
}
