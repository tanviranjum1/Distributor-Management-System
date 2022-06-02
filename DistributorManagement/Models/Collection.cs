using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.Models
{
    public class Collection
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public int? DsrID { get; set; }
        public virtual Dsr Dsr { get; set; }
    }
}