namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Collection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DsrID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dsr", t => t.DsrID)
                .Index(t => t.DsrID);
            
            CreateTable(
                "dbo.Dsr",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NationalID = c.String(),
                        Contact = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CollectionDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SalesRegisterDetailsID = c.Int(nullable: false),
                        CollectionID = c.Int(nullable: false),
                        CollectionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReturnAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Collection", t => t.CollectionID)
                .ForeignKey("dbo.SalesRegisterDetails", t => t.SalesRegisterDetailsID)
                .Index(t => t.SalesRegisterDetailsID)
                .Index(t => t.CollectionID);
            
            CreateTable(
                "dbo.SalesRegisterDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        InvoiceNumber = c.String(),
                        InvoiceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GiftItem = c.String(),
                        SalesRegisterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.SalesRegister", t => t.SalesRegisterID)
                .Index(t => t.CustomerID)
                .Index(t => t.SalesRegisterID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Contact = c.String(),
                        Address = c.String(),
                        AreaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Area", t => t.AreaID)
                .Index(t => t.AreaID);
            
            CreateTable(
                "dbo.SalesRegister",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        DsrID = c.Int(nullable: false),
                        ManufacturerID = c.Int(nullable: false),
                        SrID = c.Int(nullable: false),
                        AreaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Area", t => t.AreaID)
                .ForeignKey("dbo.Dsr", t => t.DsrID)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerID)
                .ForeignKey("dbo.Sr", t => t.SrID)
                .Index(t => t.DsrID)
                .Index(t => t.ManufacturerID)
                .Index(t => t.SrID)
                .Index(t => t.AreaID);
            
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Contact = c.Int(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sr",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Contact = c.Int(nullable: false),
                        Address = c.String(),
                        ManufacturerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerID)
                .Index(t => t.ManufacturerID);
            
            CreateTable(
                "dbo.CollectionExpense",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpenseHeadID = c.Int(nullable: false),
                        CollectionID = c.Int(nullable: false),
                        ManufacturerID = c.Int(),
                        ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Collection", t => t.CollectionID)
                .ForeignKey("dbo.ExpenseHead", t => t.ExpenseHeadID)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerID)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .Index(t => t.ExpenseHeadID)
                .Index(t => t.CollectionID)
                .Index(t => t.ManufacturerID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ExpenseHead",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OpeningBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DefaultValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ManufacturerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerID)
                .Index(t => t.ManufacturerID);
            
            CreateTable(
                "dbo.DsrArea",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AreaID = c.Int(nullable: false),
                        DsrID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Area", t => t.AreaID)
                .ForeignKey("dbo.Dsr", t => t.DsrID)
                .Index(t => t.AreaID)
                .Index(t => t.DsrID);
            
            CreateTable(
                "dbo.Expense",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpenseHeadID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExpenseHead", t => t.ExpenseHeadID)
                .Index(t => t.ExpenseHeadID);
            
            CreateTable(
                "dbo.RetailerProduct",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RetailerProduct", "ProductID", "dbo.Product");
            DropForeignKey("dbo.RetailerProduct", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Expense", "ExpenseHeadID", "dbo.ExpenseHead");
            DropForeignKey("dbo.DsrArea", "DsrID", "dbo.Dsr");
            DropForeignKey("dbo.DsrArea", "AreaID", "dbo.Area");
            DropForeignKey("dbo.CollectionExpense", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "ManufacturerID", "dbo.Manufacturer");
            DropForeignKey("dbo.CollectionExpense", "ManufacturerID", "dbo.Manufacturer");
            DropForeignKey("dbo.CollectionExpense", "ExpenseHeadID", "dbo.ExpenseHead");
            DropForeignKey("dbo.CollectionExpense", "CollectionID", "dbo.Collection");
            DropForeignKey("dbo.CollectionDetails", "SalesRegisterDetailsID", "dbo.SalesRegisterDetails");
            DropForeignKey("dbo.SalesRegisterDetails", "SalesRegisterID", "dbo.SalesRegister");
            DropForeignKey("dbo.SalesRegister", "SrID", "dbo.Sr");
            DropForeignKey("dbo.Sr", "ManufacturerID", "dbo.Manufacturer");
            DropForeignKey("dbo.SalesRegister", "ManufacturerID", "dbo.Manufacturer");
            DropForeignKey("dbo.SalesRegister", "DsrID", "dbo.Dsr");
            DropForeignKey("dbo.SalesRegister", "AreaID", "dbo.Area");
            DropForeignKey("dbo.SalesRegisterDetails", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Customer", "AreaID", "dbo.Area");
            DropForeignKey("dbo.CollectionDetails", "CollectionID", "dbo.Collection");
            DropForeignKey("dbo.Collection", "DsrID", "dbo.Dsr");
            DropIndex("dbo.RetailerProduct", new[] { "ProductID" });
            DropIndex("dbo.RetailerProduct", new[] { "CustomerID" });
            DropIndex("dbo.Expense", new[] { "ExpenseHeadID" });
            DropIndex("dbo.DsrArea", new[] { "DsrID" });
            DropIndex("dbo.DsrArea", new[] { "AreaID" });
            DropIndex("dbo.Product", new[] { "ManufacturerID" });
            DropIndex("dbo.CollectionExpense", new[] { "ProductID" });
            DropIndex("dbo.CollectionExpense", new[] { "ManufacturerID" });
            DropIndex("dbo.CollectionExpense", new[] { "CollectionID" });
            DropIndex("dbo.CollectionExpense", new[] { "ExpenseHeadID" });
            DropIndex("dbo.Sr", new[] { "ManufacturerID" });
            DropIndex("dbo.SalesRegister", new[] { "AreaID" });
            DropIndex("dbo.SalesRegister", new[] { "SrID" });
            DropIndex("dbo.SalesRegister", new[] { "ManufacturerID" });
            DropIndex("dbo.SalesRegister", new[] { "DsrID" });
            DropIndex("dbo.Customer", new[] { "AreaID" });
            DropIndex("dbo.SalesRegisterDetails", new[] { "SalesRegisterID" });
            DropIndex("dbo.SalesRegisterDetails", new[] { "CustomerID" });
            DropIndex("dbo.CollectionDetails", new[] { "CollectionID" });
            DropIndex("dbo.CollectionDetails", new[] { "SalesRegisterDetailsID" });
            DropIndex("dbo.Collection", new[] { "DsrID" });
            DropTable("dbo.RetailerProduct");
            DropTable("dbo.Expense");
            DropTable("dbo.DsrArea");
            DropTable("dbo.Product");
            DropTable("dbo.ExpenseHead");
            DropTable("dbo.CollectionExpense");
            DropTable("dbo.Sr");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.SalesRegister");
            DropTable("dbo.Customer");
            DropTable("dbo.SalesRegisterDetails");
            DropTable("dbo.CollectionDetails");
            DropTable("dbo.Dsr");
            DropTable("dbo.Collection");
            DropTable("dbo.Area");
        }
    }
}
