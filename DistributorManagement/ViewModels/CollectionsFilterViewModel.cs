using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class CollectionsFilterViewModel
    {
        public CollectionsFilterViewModel()
        {
            CollectionsList = new List<Collection>();
            From = DateTime.Today;
            To = DateTime.Today;
        }

        public int? CollectionID { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int? DsrID { get; set; }

        public List<Collection> CollectionsList { get; set; }
    }
}