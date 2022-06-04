namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Collection", new[] { "DsrID" });
            AddColumn("dbo.SalesRegister", "TotalInvoiceAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.SalesRegister", "ProductID", c => c.Int());
            AddColumn("dbo.Expense", "ProductID", c => c.Int());
            AddColumn("dbo.Expense", "DsrID", c => c.Int());
            AddColumn("dbo.Expense", "ManufacturerID", c => c.Int());
            AlterColumn("dbo.Collection", "DsrID", c => c.Int());
            AlterColumn("dbo.Manufacturer", "Contact", c => c.String());
            AlterColumn("dbo.Sr", "Contact", c => c.String());
            CreateIndex("dbo.Collection", "DsrID");
            CreateIndex("dbo.SalesRegister", "ProductID");
            CreateIndex("dbo.Expense", "ProductID");
            CreateIndex("dbo.Expense", "DsrID");
            CreateIndex("dbo.Expense", "ManufacturerID");
            AddForeignKey("dbo.SalesRegister", "ProductID", "dbo.Product", "ID");
            AddForeignKey("dbo.Expense", "DsrID", "dbo.Dsr", "ID");
            AddForeignKey("dbo.Expense", "ManufacturerID", "dbo.Manufacturer", "ID");
            AddForeignKey("dbo.Expense", "ProductID", "dbo.Product", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expense", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Expense", "ManufacturerID", "dbo.Manufacturer");
            DropForeignKey("dbo.Expense", "DsrID", "dbo.Dsr");
            DropForeignKey("dbo.SalesRegister", "ProductID", "dbo.Product");
            DropIndex("dbo.Expense", new[] { "ManufacturerID" });
            DropIndex("dbo.Expense", new[] { "DsrID" });
            DropIndex("dbo.Expense", new[] { "ProductID" });
            DropIndex("dbo.SalesRegister", new[] { "ProductID" });
            DropIndex("dbo.Collection", new[] { "DsrID" });
            AlterColumn("dbo.Sr", "Contact", c => c.Int(nullable: false));
            AlterColumn("dbo.Manufacturer", "Contact", c => c.Int(nullable: false));
            AlterColumn("dbo.Collection", "DsrID", c => c.Int(nullable: false));
            DropColumn("dbo.Expense", "ManufacturerID");
            DropColumn("dbo.Expense", "DsrID");
            DropColumn("dbo.Expense", "ProductID");
            DropColumn("dbo.SalesRegister", "ProductID");
            DropColumn("dbo.SalesRegister", "TotalInvoiceAmount");
            CreateIndex("dbo.Collection", "DsrID");
        }
    }
}
