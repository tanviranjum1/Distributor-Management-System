using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.Models
{
    public class CollectionExpense
    {
        public int ID { get; set; }

        public decimal Amount { get; set; }
   
        public int ExpenseHeadID { get; set; }
        public virtual ExpenseHead ExpenseHead { get; set; }

        public int CollectionID { get; set; }
        public virtual Collection Collection { get; set; }

        public int? ManufacturerID { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public int? ProductID { get; set; }
        public virtual Product Product { get; set; }

    }
}