using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DistributorManagement.ViewModels;
using DistributorManagement.Models;
using DistributorManagement.Repositories;

namespace DistributorManagement.Controllers
{
    public class CollectionsController : Controller
    {
        private DistributorManagementContext db = new DistributorManagementContext();
        private CollectionDataRepository collectionRepo = new CollectionDataRepository();


        //get salesRegisterDetails upon selection of dropdown value using Ajax.
        public JsonResult getSalesRegisterDetails(int? saleRegisterId, int? dsrId, bool dueOnly, int? customerId)
        {
            var srd = collectionRepo.getSalesRegisterDetails(saleRegisterId, dsrId, dueOnly, customerId);
            return Json(new { SalesRegisterDetail = srd }, JsonRequestBehavior.AllowGet);
        }

        //get products upon selection of dropdown value using Ajax.
        public JsonResult getProducts()
        {
            var product = db.Product.ToList();
            return Json(new { Product = product }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getAmount()
        {
            var expense = db.Expense.ToList();
            return Json(new { Expense = expense }, JsonRequestBehavior.AllowGet);
        }

        // GET: Collections
        public ActionResult Index(int? err)
        {
            // var collection = db.Collection.Include(c => c.Dsr);
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");
            CollectionsFilterViewModel cfvm = new CollectionsFilterViewModel();

            if (err == 1)
            {
                ViewBag.Message = "Delete Failed!!";
            }

            return View(cfvm);
        }

        [HttpPost]
        public ActionResult Index(CollectionsFilterViewModel cfvm)
        {
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");

            var Collections = db.Collection.AsQueryable();

            DateTime To = cfvm.To.AddDays(1);

            Collections = Collections.Where(r => r.Date >= cfvm.From && r.Date < To).OrderBy(r => r.Date).AsQueryable();

            if (cfvm.CollectionID != null)
            {
                Collections = Collections.Where(r => r.ID == cfvm.CollectionID).AsQueryable();
            }

            if (cfvm.DsrID != null)
            {
                Collections = Collections.Where(r => r.DsrID == cfvm.DsrID).AsQueryable();
            }

            cfvm.CollectionsList = Collections.ToList();
            return View(cfvm);
        }




        // GET: Collections/Details/5
        public ActionResult Details(int? id)
        {
            CollectionsViewModel cvm = new CollectionsViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cvm.Collection = db.Collection.Find(id);
            cvm.CollectionExpenses = db.CollectionExpense.Where(f => f.CollectionID == id).ToList();

            cvm.CollectionItemDetails = db.CollectionDetails.Where(r => r.CollectionID == id).
                   Select(r =>
                    new CollectionItemDetailViewModel()
                    {
                        ID = r.SalesRegisterDetailsID,
                        CustomerID = r.SalesRegisterDetails.CustomerID,
                        Customer = r.SalesRegisterDetails.Customer,
                        InvoiceNumber = r.SalesRegisterDetails.InvoiceNumber,
                        InvoiceAmount = r.SalesRegisterDetails.InvoiceAmount,
                        GiftItem = r.SalesRegisterDetails.GiftItem,
                        SalesRegisterID = r.SalesRegisterDetails.SalesRegisterID,
                        SalesRegister = r.SalesRegisterDetails.SalesRegister,
                        PreviousCollectionAmount = db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.SalesRegisterDetailsID) ?
                                  db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.SalesRegisterDetailsID).Sum(s => s.CollectionAmount)
                                             : 0,
                        PreviousReturnAmount = db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.SalesRegisterDetailsID) ?
                                  db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.SalesRegisterDetailsID).Sum(s => s.ReturnAmount)
                                             : 0,
                        CollectionAmount = r.CollectionAmount,
                        ReturnAmount = r.ReturnAmount
                    }).ToList();
            if (cvm.Collection == null)
            {
                return HttpNotFound();
            }
            return View(cvm);
        }

        // GET: Collections/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name");
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            CollectionsViewModel cvm = new CollectionsViewModel();
            return View(cvm);
        }

        // GET: Collections/insertCollections
        [HttpPost]
        public JsonResult insertCollections(CollectionsViewModel cvm)
        {
            Collection collection = new Collection()
            {
                Date = cvm.Collection.Date,
                DsrID = cvm.Collection.DsrID
            };
            db.Collection.Add(collection);
            db.SaveChanges();

            foreach (var item in cvm.CollectionDetails)
            {
                item.CollectionID = collection.ID;
                db.CollectionDetails.Add(item);
                db.SaveChanges();
            }

            foreach (var item in cvm.CollectionExpenses)
            {
                item.CollectionID = collection.ID;
                db.CollectionExpense.Add(item);
                db.SaveChanges();
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        // GET: Collections/Edit/5
        public ActionResult Edit(int? id)
        {
            CollectionsViewModel cvm = new CollectionsViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            cvm.Collection = db.Collection.Find(id);
            cvm.CollectionExpenses = db.CollectionExpense.Where(f => f.CollectionID == id).ToList();

            cvm.CollectionItemDetails = db.CollectionDetails.Where(r => r.CollectionID == id).
                     Select(r =>
                      new CollectionItemDetailViewModel()
                      {
                          ID = r.SalesRegisterDetailsID,
                          CustomerID = r.SalesRegisterDetails.CustomerID,
                          Customer = r.SalesRegisterDetails.Customer,
                          InvoiceNumber = r.SalesRegisterDetails.InvoiceNumber,
                          InvoiceAmount = r.SalesRegisterDetails.InvoiceAmount,
                          GiftItem = r.SalesRegisterDetails.GiftItem,
                          SalesRegisterID = r.SalesRegisterDetails.SalesRegisterID,
                          SalesRegister = r.SalesRegisterDetails.SalesRegister,
                          PreviousCollectionAmount = db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.SalesRegisterDetailsID) ?
                                    db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.SalesRegisterDetailsID).Sum(s => s.CollectionAmount)
                                               : 0,
                          PreviousReturnAmount = db.CollectionDetails.Any(s => s.SalesRegisterDetailsID == r.SalesRegisterDetailsID) ?
                                    db.CollectionDetails.Where(s => s.SalesRegisterDetailsID == r.SalesRegisterDetailsID).Sum(s => s.ReturnAmount)
                                               : 0,
                          CollectionAmount = r.CollectionAmount,
                          ReturnAmount = r.ReturnAmount
                      }).ToList();

            if (cvm.Collection == null)
            {
                return HttpNotFound();
            }
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name", cvm.Collection.DsrID);
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name");

            return View(cvm);
        }


        // GET: Collections/editCollections
        [HttpPost]
        public JsonResult editCollections(CollectionsViewModel cvm)
        {
            Collection collection = db.Collection.Find(cvm.Collection.ID);
            collection.Date = cvm.Collection.Date;
            collection.DsrID = cvm.Collection.DsrID;

            var cdl = db.CollectionDetails.Where(a => a.CollectionID == cvm.Collection.ID).ToList();
            foreach (var item in cdl)
            {
                db.CollectionDetails.Remove(item);
            }
            db.SaveChanges();

            var cdl2 = db.CollectionExpense.Where(a => a.CollectionID == cvm.Collection.ID).ToList();
            foreach (var item in cdl2)
            {
                db.CollectionExpense.Remove(item);
            }
            db.SaveChanges();

            foreach (var item in cvm.CollectionDetails)
            {
                item.CollectionID = collection.ID;
                db.CollectionDetails.Add(item);
                db.SaveChanges();
            }

            foreach (var item in cvm.CollectionExpenses)
            {
                item.CollectionID = collection.ID;
                db.CollectionExpense.Add(item);
                db.SaveChanges();
            }

            //collection edit
            db.Entry(collection).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        // GET: Collections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collection.Find(id);

            if (collection == null)
            {
                return HttpNotFound();
            }
            try
            {
                var srl = db.CollectionDetails.Where(a => a.CollectionID == id).ToList();
                foreach (var vp in srl)
                    db.CollectionDetails.Remove(vp);
                db.SaveChanges();

                var colexps = db.CollectionExpense.Where(a => a.CollectionID == id).ToList();
                foreach (var item in colexps)
                    db.CollectionExpense.Remove(item);
                db.SaveChanges();

                db.Collection.Remove(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", new { err = 1 });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


