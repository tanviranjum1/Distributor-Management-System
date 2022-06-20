using System;

namespace DistributorManagement.Models
{
    public class SalesRegister
    {

        public int ID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime RegistrationDate { get; set; }

        public decimal? TotalInvoiceAmount { get; set; }
        public decimal? TotalReturnAmount { get; set; }

        public decimal? CashCollection { get; set; }

        public int? ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int DsrID { get; set; }
        public virtual Dsr Dsr { get; set; }

        public int ManufacturerID { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public int SrID { get; set; }
        public virtual Sr Sr { get; set; }

        public int AreaID { get; set; }
        public virtual Area Area { get; set; }
    }
}