using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.Models
{
    public class RetailerProduct
    {
        public int ID { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

       
    }
}