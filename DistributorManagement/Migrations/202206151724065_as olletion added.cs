namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asolletionadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesRegister", "TotalReturnAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.SalesRegister", "CashCollection", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalesRegister", "CashCollection");
            DropColumn("dbo.SalesRegister", "TotalReturnAmount");
        }
    }
}
