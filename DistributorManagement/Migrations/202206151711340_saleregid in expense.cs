namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saleregidinexpense : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expense", "SalesRegisterID", c => c.Int());
            CreateIndex("dbo.Expense", "SalesRegisterID");
            AddForeignKey("dbo.Expense", "SalesRegisterID", "dbo.SalesRegister", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expense", "SalesRegisterID", "dbo.SalesRegister");
            DropIndex("dbo.Expense", new[] { "SalesRegisterID" });
            DropColumn("dbo.Expense", "SalesRegisterID");
        }
    }
}
