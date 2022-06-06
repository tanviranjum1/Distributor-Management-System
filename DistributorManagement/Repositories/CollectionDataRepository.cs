using DistributorManagement.Models;
using DistributorManagement.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace DistributorManagement.Repositories
{
    public class CollectionDataRepository
    {
        private DistributorManagementContext db = new DistributorManagementContext();

        public List<CollectionItemDetailViewModel> getSalesRegisterDetails(int? saleRegisterId, int? dsrId, bool dueOnly, int? customerId)
        {
            var SalesRegisterDetails = db.SalesRegisterDetails.AsQueryable();

            if (saleRegisterId != null)
            {
                SalesRegisterDetails = SalesRegisterDetails.Where(r => r.SalesRegisterID == saleRegisterId).AsQueryable();
            }
            if (dsrId != null)
            {
                SalesRegisterDetails = SalesRegisterDetails.Where(r => r.SalesRegister.DsrID == dsrId).AsQueryable();
            }
            if (customerId != null)
            {
                SalesRegisterDetails = SalesRegisterDetails.Where(r => r.CustomerID == customerId).AsQueryable();
            }


            if (dueOnly)
            {
                return SalesRegisterDetails.
                  Where(r =>
                  r.InvoiceAmount -
                  (db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.ID) ?
                   db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.ID).Sum(s => s.CollectionAmount) : 0) -
                  (db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.ID) ?
                   db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.ID).Sum(s => s.ReturnAmount) : 0)
                  != 0)
                  .Select(r =>
                  new CollectionItemDetailViewModel()
                  {
                      ID = r.ID,
                      CustomerID = r.CustomerID,
                      Customer = r.Customer,
                      InvoiceNumber = r.InvoiceNumber,
                      InvoiceAmount = r.InvoiceAmount,
                      GiftItem = r.GiftItem != null ? r.GiftItem : "",
                      SalesRegisterID = r.SalesRegisterID,
                      SalesRegister = r.SalesRegister,
                      PreviousCollectionAmount = db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.ID) ?
                                           db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.ID).Sum(s => s.CollectionAmount)
                                           : 0,
                      PreviousReturnAmount = db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.ID) ?
                                           db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.ID).Sum(s => s.ReturnAmount)
                                           : 0,
                      CollectionAmount = 0,
                      ReturnAmount = 0,

                  }).ToList();
            }
            else
            {
                return SalesRegisterDetails.Select(r =>
                  new CollectionItemDetailViewModel()
                  {
                      ID = r.ID,
                      CustomerID = r.CustomerID,
                      Customer = r.Customer,
                      InvoiceNumber = r.InvoiceNumber,
                      InvoiceAmount = r.InvoiceAmount,
                      GiftItem = r.GiftItem != null ? r.GiftItem : "",
                      SalesRegisterID = r.SalesRegisterID,
                      SalesRegister = r.SalesRegister,
                      PreviousCollectionAmount = db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.ID) ?
                                           db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.ID).Sum(s => s.CollectionAmount)
                                           : 0,
                      PreviousReturnAmount = db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.ID) ?
                                           db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.ID).Sum(s => s.ReturnAmount)
                                           : 0,
                      CollectionAmount = 0,
                      ReturnAmount = 0
                  }).ToList();
            }
        }
    }
}




