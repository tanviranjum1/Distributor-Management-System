using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class ManufacturerFilterViewModel
    {
        public ManufacturerFilterViewModel()
        {
            ManufacturersList = new List<Manufacturer>();
        }
        public string Name { get; set; }
        public string Contact { get; set; }
        public List<Manufacturer> ManufacturersList { get; set; }

    }
}