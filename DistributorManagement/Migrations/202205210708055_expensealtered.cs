namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expensealtered : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Collection", new[] { "DsrID" });
            AddColumn("dbo.Expense", "ProductID", c => c.Int());
            AddColumn("dbo.Expense", "DsrID", c => c.Int());
            AddColumn("dbo.Expense", "ManufacturerID", c => c.Int());
            AlterColumn("dbo.Collection", "DsrID", c => c.Int());
            CreateIndex("dbo.Collection", "DsrID");
            CreateIndex("dbo.Expense", "ProductID");
            CreateIndex("dbo.Expense", "DsrID");
            CreateIndex("dbo.Expense", "ManufacturerID");
            AddForeignKey("dbo.Expense", "DsrID", "dbo.Dsr", "ID");
            AddForeignKey("dbo.Expense", "ManufacturerID", "dbo.Manufacturer", "ID");
            AddForeignKey("dbo.Expense", "ProductID", "dbo.Product", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expense", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Expense", "ManufacturerID", "dbo.Manufacturer");
            DropForeignKey("dbo.Expense", "DsrID", "dbo.Dsr");
            DropIndex("dbo.Expense", new[] { "ManufacturerID" });
            DropIndex("dbo.Expense", new[] { "DsrID" });
            DropIndex("dbo.Expense", new[] { "ProductID" });
            DropIndex("dbo.Collection", new[] { "DsrID" });
            AlterColumn("dbo.Collection", "DsrID", c => c.Int(nullable: false));
            DropColumn("dbo.Expense", "ManufacturerID");
            DropColumn("dbo.Expense", "DsrID");
            DropColumn("dbo.Expense", "ProductID");
            CreateIndex("dbo.Collection", "DsrID");
        }
    }
}
