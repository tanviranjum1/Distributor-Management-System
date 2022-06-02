namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manufacaddedtoretailerprod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RetailerProduct", "ManufacturerID", c => c.Int());
            CreateIndex("dbo.RetailerProduct", "ManufacturerID");
            AddForeignKey("dbo.RetailerProduct", "ManufacturerID", "dbo.Manufacturer", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RetailerProduct", "ManufacturerID", "dbo.Manufacturer");
            DropIndex("dbo.RetailerProduct", new[] { "ManufacturerID" });
            DropColumn("dbo.RetailerProduct", "ManufacturerID");
        }
    }
}
