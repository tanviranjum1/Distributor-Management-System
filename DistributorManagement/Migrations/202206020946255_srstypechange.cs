namespace DistributorManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class srstypechange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sr", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sr", "Contact", c => c.Int(nullable: false));
        }
    }
}
