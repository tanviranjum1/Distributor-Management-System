using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.Models
{
    public class DsrArea
    {
        public int ID { get; set; }

        public int AreaID { get; set; }
        public virtual Area Area { get; set; }

        public int DsrID { get; set; }
        public virtual Dsr Dsr { get; set; }
    }
}