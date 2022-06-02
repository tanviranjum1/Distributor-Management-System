using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DistributorManagement.Models;

namespace DistributorManagement.ViewModels
{
    public class SaleRegisterFilterViewModel
    {
        public SaleRegisterFilterViewModel()
        {
            SaleRegList = new List<SalesRegister>();
        }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int? SaleRegisterID { get; set; }
        public int? DsrID { get; set; }
        public int? ManufacturerID { get; set; }
        public int? SrID { get; set; }
        public int? AreaID { get; set; }

        public List<SalesRegister> SaleRegList { get; set; }
    }
}