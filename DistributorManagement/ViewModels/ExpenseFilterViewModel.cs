using DistributorManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.ViewModels
{
    public class ExpenseFilterViewModel
    {
        public ExpenseFilterViewModel()
        {
            ExpenseList = new List<Expense>();
            From = DateTime.Today;
            To = DateTime.Today;
        }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int? ExpenseHeadID { get; set; }
       
        public List<Expense> ExpenseList { get; set; }


    }
}