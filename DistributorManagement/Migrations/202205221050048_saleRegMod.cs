namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saleRegMod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesRegister", "ProductID", c => c.Int());
            CreateIndex("dbo.SalesRegister", "ProductID");
            AddForeignKey("dbo.SalesRegister", "ProductID", "dbo.Product", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesRegister", "ProductID", "dbo.Product");
            DropIndex("dbo.SalesRegister", new[] { "ProductID" });
            DropColumn("dbo.SalesRegister", "ProductID");
        }
    }
}
