using DistributorManagement.Models;
using System;
using System.Collections.Generic;

namespace DistributorManagement.ViewModels
{
    public class CollectionsViewModel
    {
        public CollectionsViewModel()
        {
            CollectionDetails = new List<CollectionDetails>();
            CollectionExpenses = new List<CollectionExpense>();
            CollectionItemDetails = new List<CollectionItemDetailViewModel>();
        }
        public Collection Collection { get; set; }
        public List<CollectionDetails> CollectionDetails { get; set; }
        public List<CollectionExpense> CollectionExpenses { get; set; }
        public List<CollectionItemDetailViewModel> CollectionItemDetails { get; set; }
    }

    public class CollectionItemDetailViewModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public string RegistrationDateFormatted
        {
            get
            {
                return SalesRegister.RegistrationDate.ToString("dd-MMM-yyyy");
            }
        }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string GiftItem { get; set; }
        public int SalesRegisterID { get; set; }
        public virtual SalesRegister SalesRegister { get; set; }
        public decimal PreviousCollectionAmount { get; set; }
        public decimal PreviousReturnAmount { get; set; }
        public decimal CollectionAmount { get; set; }
        public decimal ReturnAmount { get; set; }
        public PaymentMethod? PaymentType { get; set; }
        public string BankInformation { get; set; }
        public string ChequeInformation { get; set; }
        public string Details { get; set; }
        public DateTime? HonorDate { get; set; }
    }

}