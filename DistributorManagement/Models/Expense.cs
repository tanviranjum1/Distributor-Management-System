using System;

namespace DistributorManagement.Models
{
    public class Expense
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public int? SalesRegisterID { get; set; }
        public virtual SalesRegister SalesRegister { get; set; }


        public int ExpenseHeadID { get; set; }
        public virtual ExpenseHead ExpenseHead { get; set; }

        public int? ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int? DsrID { get; set; }
        public virtual Dsr Dsr { get; set; }

        public int? ManufacturerID { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }


    }
}