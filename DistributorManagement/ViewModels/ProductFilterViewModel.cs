using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class ProductFilterViewModel
    {
        public ProductFilterViewModel()
        {
            ProductList = new List<Product>();
        }

        public string Name { get; set; }

        public int? ManufacturerID { get; set; }

        public List<Product> ProductList { get; set; }

    }
}