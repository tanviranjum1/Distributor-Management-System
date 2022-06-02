using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.Models
{
    public class SalesRegisterDetails
    {
        public int ID { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string GiftItem { get; set; }

        public int SalesRegisterID { get; set; }
        public virtual SalesRegister SalesRegister { get; set; }

    }
}