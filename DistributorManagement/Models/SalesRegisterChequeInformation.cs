using System;

namespace DistributorManagement.Models
{
    public class SalesRegisterChequeInformation
    {
        public int ID { get; set; }

        public int SalesRegisterID { get; set; }
        public virtual SalesRegister SalesRegister { get; set; }


        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public string BankInformation { get; set; }
        public string Details { get; set; }
        public string ChequeInformation { get; set; }
        public DateTime? HonorDate { get; set; }

        public decimal Amount { get; set; }


    }
}