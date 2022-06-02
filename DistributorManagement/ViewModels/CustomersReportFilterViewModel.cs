using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class CustomersReportFilterViewModel
    {
        public CustomersReportFilterViewModel()
        {
            From = DateTime.Today;
            To = DateTime.Today;
            CustomersReportItemDetails = new List<CustomersReportItemDetailViewModel>();
        }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int? SaleRegisterID { get; set; }
        public int? CustomerID { get; set; }
        public int? DsrID { get; set; }
        public int? ManufacturerID { get; set; }
        public int? ProductID { get; set; }
        public bool? CheckID { get; set; } 
        public List<CustomersReportItemDetailViewModel> CustomersReportItemDetails { get; set; }
    }

    public class CustomersReportItemDetailViewModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string GiftItem { get; set; }
        public int SalesRegisterID { get; set; }
        public virtual SalesRegister SalesRegister { get; set; }
        public decimal PreviousCollectionAmount { get; set; }
        public decimal PreviousReturnAmount { get; set; }
        public decimal CollectionAmount { get; set; }
        public decimal ReturnAmount { get; set; }
        public decimal DueAmount { get; set; }
    }
}