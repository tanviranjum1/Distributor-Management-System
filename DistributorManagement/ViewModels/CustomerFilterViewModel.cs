using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DistributorManagement.ViewModels
{
    public class CustomerFilterViewModel
    {
        public CustomerFilterViewModel()
        {
            CustomersList = new List<Customer>();
        }
        public string Name { get; set; }

        public string Code { get; set; }

        public string Contact { get; set; }

        public int? AreaID { get; set; }

        public List<Customer> CustomersList { get; set; }

    }
}