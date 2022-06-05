using System;

namespace DistributorManagement.Models
{
    public enum PaymentMethod
    {
        Cash,
        Cheque
    }

    public class CollectionDetails
    {
        public int ID { get; set; }

        public int SalesRegisterDetailsID { get; set; }
        public virtual SalesRegisterDetails SalesRegisterDetails { get; set; }

        public int CollectionID { get; set; }
        public virtual Collection Collection { get; set; }

        public decimal CollectionAmount { get; set; }
        public decimal ReturnAmount { get; set; }


        public PaymentMethod? PaymentType { get; set; }
        public string BankInformation { get; set; }
        public string Details { get; set; }
        public string ChequeInformation { get; set; }
        public DateTime? HonorDate { get; set; }


    }
}