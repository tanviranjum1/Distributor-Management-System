using System;
using System.Collections.Generic;

namespace DistributorManagement.ViewModels
{
    public class SalesRegisterCollectionSummaryReportFilterViewModel
    {
        public SalesRegisterCollectionSummaryReportFilterViewModel()
        {
            Date = DateTime.Today;
            SalesRegisterCollectionSummaryReportItemDetails = new List<SalesRegisterCollectionSummaryReportItemDetailViewModel>();
            DueDetailsList = new List<DueDetails>();
            ExpenditureDetailsList = new List<ExpenditureDetails>();
        }
        public DateTime? Date { get; set; }  /*invoice date*/
        public int? DsrID { get; set; }
        public List<SalesRegisterCollectionSummaryReportItemDetailViewModel> SalesRegisterCollectionSummaryReportItemDetails { get; set; }
        public List<DueDetails> DueDetailsList { get; set; }
        public List<ExpenditureDetails> ExpenditureDetailsList { get; set; }

    }

    public class SalesRegisterCollectionSummaryReportItemDetailViewModel
    {
        public DateTime Date { get; set; }
        public int SalesRegisterID { get; set; }
        public decimal TotalInvoice { get; set; }
        public decimal? CollectionCash { get; set; }
        public decimal? CollectionCheque { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? Expenses { get; set; }
        public decimal? ReturnAmount { get; set; }
    }

    public class DueDetails
    {
        public DateTime Date { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal DueAmount { get; set; }
    }

    public class ExpenditureDetails
    {
        public DateTime Date { get; set; }
        public string HeadName { get; set; }
        public decimal Amount { get; set; }
    }

}




