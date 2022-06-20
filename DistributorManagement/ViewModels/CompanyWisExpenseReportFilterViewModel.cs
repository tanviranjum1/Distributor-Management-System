using System;
using System.Collections.Generic;

namespace DistributorManagement.ViewModels
{
    public class CompanyWisExpenseReportFilterViewModel
    {
        public CompanyWisExpenseReportFilterViewModel()
        {
            CompanyWiseExpenseReportItemDetails = new List<CompanyWiseExpenseReportItemDetailViewModel>();
        }
        public int? ManufacturerID { get; set; }
        public int? ProductID { get; set; }
        public List<CompanyWiseExpenseReportItemDetailViewModel> CompanyWiseExpenseReportItemDetails { get; set; }
    }

    public class CompanyWiseExpenseReportItemDetailViewModel
    {
        //public int ID { get; set; }
        public DateTime Date { get; set; }
        public string ExpenseHead { get; set; }
        public decimal Amount { get; set; }
    }
}
