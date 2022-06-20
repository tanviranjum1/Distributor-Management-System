namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesRegisterChequeInformation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SalesRegisterID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        BankInformation = c.String(),
                        Details = c.String(),
                        ChequeInformation = c.String(),
                        HonorDate = c.DateTime(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .ForeignKey("dbo.SalesRegister", t => t.SalesRegisterID)
                .Index(t => t.SalesRegisterID)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesRegisterChequeInformation", "SalesRegisterID", "dbo.SalesRegister");
            DropForeignKey("dbo.SalesRegisterChequeInformation", "CustomerID", "dbo.Customer");
            DropIndex("dbo.SalesRegisterChequeInformation", new[] { "CustomerID" });
            DropIndex("dbo.SalesRegisterChequeInformation", new[] { "SalesRegisterID" });
            DropTable("dbo.SalesRegisterChequeInformation");
        }
    }
}
