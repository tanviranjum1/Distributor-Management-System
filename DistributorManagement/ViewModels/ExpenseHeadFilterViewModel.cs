using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class ExpenseHeadFilterViewModel
    {
        public ExpenseHeadFilterViewModel()
        {
            ExpenseHeadList = new List<ExpenseHead>();
        }
        public int? ExpenseHeadID { get; set; }

        public List<ExpenseHead> ExpenseHeadList { get; set; }

    }
}