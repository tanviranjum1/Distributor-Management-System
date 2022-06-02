using DistributorManagement.Models;
using DistributorManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributorManagement.Repositories
{
    public class CustomerDataRepository
    {

        private DistributorManagementContext db = new DistributorManagementContext();

        public List<CustomersReportItemDetailViewModel> getSalesRegisterDetails(DateTime From, DateTime To, 
            int? saleRegisterID, int? customerID,
            int? dsrID, int? manufacturerID, int? productID, bool dueonly)
            {

            DateTime nextDate = To.AddDays(1);

            var SalesRegisterDetails = db.SalesRegisterDetails.Where(
                r => r.SalesRegister.RegistrationDate >= From && r.SalesRegister.RegistrationDate < nextDate).AsQueryable();

            if (saleRegisterID != null)
            {
                SalesRegisterDetails = SalesRegisterDetails.Where(r => r.SalesRegisterID == saleRegisterID).AsQueryable();
            }
            if (dsrID != null)
            {
                SalesRegisterDetails = SalesRegisterDetails.Where(r => r.SalesRegister.DsrID == dsrID).AsQueryable();
            }
            if (customerID != null)
            {
                SalesRegisterDetails = SalesRegisterDetails.Where(r => r.CustomerID == customerID).AsQueryable();
            }
            if (manufacturerID != null)
            {
                SalesRegisterDetails = SalesRegisterDetails.Where(r => r.SalesRegister.ManufacturerID == manufacturerID).AsQueryable();
            }
            if (productID != null)
            {
                SalesRegisterDetails = SalesRegisterDetails.Where(r => r.SalesRegister.ProductID == productID).AsQueryable();
            }

           var data = SalesRegisterDetails.Select(r =>
                 new CustomersReportItemDetailViewModel()
                 {
                     ID = r.ID,
                     CustomerID = r.CustomerID,
                     Customer = r.Customer,
                     InvoiceNumber = r.InvoiceNumber,
                     InvoiceAmount = r.InvoiceAmount,
                     GiftItem = r.GiftItem,
                     SalesRegisterID = r.SalesRegisterID,
                     SalesRegister = r.SalesRegister,
                     CollectionAmount = db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.ID) ?
                                          db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.ID).Sum(s => s.CollectionAmount) : 0,
                     ReturnAmount = db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.ID) ?
                                          db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.ID).Sum(s => s.ReturnAmount): 0,
                 }).ToList();
            if (dueonly)
            {
                return data.Where(r => r.InvoiceAmount - r.CollectionAmount - r.ReturnAmount != 0).ToList();
            }
            return data.ToList();
        }
    }

}