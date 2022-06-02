using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class DsrFilterViewModel
    {
        public DsrFilterViewModel()
        {
            DsrsList = new List<Dsr>();
        }
        public string Name { get; set; }

        public string Contact { get; set; }

        public List<Dsr> DsrsList { get; set; }

    }
}