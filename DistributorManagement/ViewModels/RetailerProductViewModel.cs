using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class RetailerProductViewModel
    {
        public RetailerProductViewModel()
        {
            RetailerProduct = new List<RetailerProduct>();
        }
        public Customer Customer { get; set; }
        public List<RetailerProduct> RetailerProduct { get; set; }
    }
}
