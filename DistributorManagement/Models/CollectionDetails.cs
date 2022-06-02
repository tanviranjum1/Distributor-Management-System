using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.Models
{
    public class CollectionDetails
    {
        public int ID { get; set; }

        public int SalesRegisterDetailsID { get; set; }
        public virtual SalesRegisterDetails SalesRegisterDetails { get; set; }

        public int CollectionID { get; set; }
        public virtual Collection Collection { get; set; }

        public decimal CollectionAmount { get; set; }
        public decimal ReturnAmount { get; set; }

    }
}