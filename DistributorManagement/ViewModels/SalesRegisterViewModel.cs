using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DistributorManagement.Models;

namespace DistributorManagement.ViewModels
{
    public class SalesRegisterViewModel
    {
        public SalesRegisterViewModel()
        {
            SalesRegisterDetails = new List<SalesRegisterDetails>();
            CollectionItemDetails = new List<CollectionItemDetailViewModel>();
            Expense = new List<Expense>();
            SalesRegisterChequeInformation = new List<SalesRegisterChequeInformation>();


        }
        public SalesRegister SalesRegister { get; set; }
        public List<SalesRegisterDetails> SalesRegisterDetails { get; set; }
        public List<CollectionItemDetailViewModel> CollectionItemDetails { get; set; }
        public List<Expense> Expense { get; set; }

        public List<SalesRegisterChequeInformation> SalesRegisterChequeInformation { get; set; }

    }
}