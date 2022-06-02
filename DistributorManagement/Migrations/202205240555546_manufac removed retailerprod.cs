namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manufacremovedretailerprod : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RetailerProduct", "ManufacturerID", "dbo.Manufacturer");
            DropIndex("dbo.RetailerProduct", new[] { "ManufacturerID" });
            DropColumn("dbo.RetailerProduct", "ManufacturerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RetailerProduct", "ManufacturerID", c => c.Int());
            CreateIndex("dbo.RetailerProduct", "ManufacturerID");
            AddForeignKey("dbo.RetailerProduct", "ManufacturerID", "dbo.Manufacturer", "ID");
        }
    }
}
