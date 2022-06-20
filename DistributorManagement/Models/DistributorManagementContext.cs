using DistributorManagement.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace DistributorManagement.Models
{
    public class DistributorManagementContext : DbContext
    {
        public DistributorManagementContext() : base("DistributorManagementContext")
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dsr> Dsrs { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Sr> Srs { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<SalesRegister> SalesRegister { get; set; }
        public DbSet<SalesRegisterDetails> SalesRegisterDetails { get; set; }
        public DbSet<Collection> Collection { get; set; }
        public DbSet<CollectionDetails> CollectionDetails { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<RetailerProduct> RetailerProduct { get; set; }
        public DbSet<DsrArea> DsrArea { get; set; }
        public DbSet<ExpenseHead> ExpenseHead { get; set; }
        public DbSet<CollectionExpense> CollectionExpense { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<SalesRegisterChequeInformation> SalesRegisterChequeInformation { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}