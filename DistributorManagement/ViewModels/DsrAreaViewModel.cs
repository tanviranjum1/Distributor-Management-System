using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class DsrAreaViewModel
    {
        public DsrAreaViewModel()
        {
            AreaID = new List<int>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public string NationalID { get; set; }

        public string Contact { get; set; }
        public string Address { get; set; }

        public List<int> AreaID { get; set; }

    }
}