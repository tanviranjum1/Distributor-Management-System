using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class SrFilterViewModel
    {
        public SrFilterViewModel()
        {
            SrsList = new List<Sr>();
        }
        public string Name { get; set; }
        public string Contact { get; set; }
        public int? ManufacturerID { get; set; }
        public List<Sr> SrsList { get; set; }
    }
}