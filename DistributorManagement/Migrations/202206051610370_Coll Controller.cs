namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CollController : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CollectionDetails", "PaymentType", c => c.Int());
            AddColumn("dbo.CollectionDetails", "BankInformation", c => c.String());
            AddColumn("dbo.CollectionDetails", "Details", c => c.String());
            AddColumn("dbo.CollectionDetails", "ChequeInformation", c => c.String());
            AddColumn("dbo.CollectionDetails", "HonorDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CollectionDetails", "HonorDate");
            DropColumn("dbo.CollectionDetails", "ChequeInformation");
            DropColumn("dbo.CollectionDetails", "Details");
            DropColumn("dbo.CollectionDetails", "BankInformation");
            DropColumn("dbo.CollectionDetails", "PaymentType");
        }
    }
}
