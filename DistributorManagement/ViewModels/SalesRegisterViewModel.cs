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
        }
        public SalesRegister SalesRegister { get; set; }
        public List<SalesRegisterDetails> SalesRegisterDetails { get; set; }
        public List<CollectionItemDetailViewModel> CollectionItemDetails { get; set; }
    }
}