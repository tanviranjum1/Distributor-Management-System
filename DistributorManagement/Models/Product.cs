using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int ManufacturerID { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }


    }
}