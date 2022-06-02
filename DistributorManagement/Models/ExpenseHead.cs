using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.Models
{
    public class ExpenseHead
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal OpeningBalance { get; set; }

        public decimal DefaultValue { get; set; }

    }
}