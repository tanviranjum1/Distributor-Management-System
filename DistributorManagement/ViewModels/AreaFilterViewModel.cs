using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class AreaFilterViewModel
    {
        public AreaFilterViewModel()
        {
            AreasList = new List<Area>();
        }

        public string Name { get; set; }

        public List<Area> AreasList { get; set; }

    }
}