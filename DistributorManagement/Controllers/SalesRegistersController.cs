using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DistributorManagement.Models;
using DistributorManagement.ViewModels;
using DistributorManagement.Repositories;

namespace DistributorManagement.Controllers
{
    public class SalesRegistersController : Controller
    {
        private DistributorManagementContext db = new DistributorManagementContext();

        private CollectionDataRepository collectionRepo = new CollectionDataRepository();

        // GET: SalesRegisters
        public ActionResult Index(int? err)
        {
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name");
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            ViewBag.SrID = new SelectList(db.Srs, "ID", "Name");
            SaleRegisterFilterViewModel srvm = new SaleRegisterFilterViewModel();

            if (err == 1)
            {
                ViewBag.Message = "Delete Failed!!";
            }

            return View(srvm);
        }

        [HttpPost]
        public ActionResult Index(SaleRegisterFilterViewModel srvm)
        {
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name");
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            ViewBag.SrID = new SelectList(db.Srs, "ID", "Name");

            DateTime To = srvm.To.AddDays(1);

            var SalesRegister = db.SalesRegister.AsQueryable();
            SalesRegister = SalesRegister.Where(r => r.RegistrationDate >= srvm.From && r.RegistrationDate < To).OrderBy(r => r.RegistrationDate).AsQueryable();

            if (srvm.SaleRegisterID != null)
            {
                SalesRegister = SalesRegister.Where(r => r.ID == srvm.SaleRegisterID).AsQueryable();
            }
            if (srvm.AreaID != null)
            {
                SalesRegister = SalesRegister.Where(r => r.AreaID == srvm.AreaID).AsQueryable();
            }
            if (srvm.DsrID != null)
            {
                SalesRegister = SalesRegister.Where(r => r.DsrID == srvm.DsrID).AsQueryable();
            }
            if (srvm.ManufacturerID != null)
            {
                SalesRegister = SalesRegister.Where(r => r.ManufacturerID == srvm.ManufacturerID).AsQueryable();
            }
            if (srvm.SrID != null)
            {
                SalesRegister = SalesRegister.Where(r => r.SrID == srvm.SrID).AsQueryable();
            }
            srvm.SaleRegList = SalesRegister.ToList();

            return View(srvm);
        }

        // GET: SalesRegisters/Create
        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name");
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            ViewBag.SrID = new SelectList(db.Srs, "ID", "Name");
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name");

            ViewBag.Customers = CommonDataRepository.getCustomSelectList(db.Customers
                .Select(r => new CustomSelectItemViewModel()
                {
                    ID = r.ID,
                    Name = r.Name,
                    Code = r.Code
                }).ToList()
                );
            return View();
        }

        // GET: Sales/insertSalesRegister
        [HttpPost]
        public JsonResult insertSalesRegister(SalesRegisterViewModel srvm)
        {
            SalesRegister salesRegister = new SalesRegister()
            {
                From = srvm.SalesRegister.From,
                To = srvm.SalesRegister.To,
                RegistrationDate = srvm.SalesRegister.RegistrationDate,
                DsrID = srvm.SalesRegister.DsrID,
                ManufacturerID = srvm.SalesRegister.ManufacturerID,
                AreaID = srvm.SalesRegister.AreaID,
                SrID = srvm.SalesRegister.SrID,
                ProductID = srvm.SalesRegister.ProductID,
                CashCollection = srvm.SalesRegister.CashCollection,
                TotalReturnAmount = srvm.SalesRegister.TotalReturnAmount,
                TotalInvoiceAmount = srvm.SalesRegister.TotalInvoiceAmount,
            };
            db.SalesRegister.Add(salesRegister);
            db.SaveChanges();

            foreach (var item in srvm.Expense)
            {
                item.SalesRegisterID = salesRegister.ID;
                db.Expense.Add(item);
                db.SaveChanges();
            }
            foreach (var item in srvm.SalesRegisterChequeInformation)
            {
                item.SalesRegisterID = salesRegister.ID;
                db.SalesRegisterChequeInformation.Add(item);
                db.SaveChanges();
            }
            foreach (var item in srvm.SalesRegisterDetails)
            {
                item.SalesRegisterID = salesRegister.ID;
                db.SalesRegisterDetails.Add(item);
                db.SaveChanges();
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        //get products upon selection of dropdown value using Ajax.
        public JsonResult getProducts()
        {
            var product = db.Product.ToList();
            return Json(new { Product = product }, JsonRequestBehavior.AllowGet);
        }

        //get areas upon selection of dropdown value using Ajax.
        public JsonResult getAreas()
        {
            var dsrareas = db.DsrArea.ToList();
            return Json(new { DsrArea = dsrareas }, JsonRequestBehavior.AllowGet);
        }

        //get areas upon selection of dropdown value using Ajax.
        public JsonResult getCustomers()
        {
            var customers = db.Customers.ToList();
            return Json(new { Customer = customers }, JsonRequestBehavior.AllowGet);
        }

        //get customerstable upon selection of dropdown value in product using Ajax.
        public JsonResult getRetailerProducts()
        {
            var retailerproducts = db.RetailerProduct.ToList();
            return Json(new { RetailerProduct = retailerproducts }, JsonRequestBehavior.AllowGet);
        }

        // GET: SalesRegisters/Edit/5
        public ActionResult Edit(int? id)
        {
            SalesRegisterViewModel svm = new SalesRegisterViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            svm.SalesRegister = db.SalesRegister.Find(id);
            svm.SalesRegisterDetails = db.SalesRegisterDetails.Where(f => f.SalesRegister.ID == id).ToList();
            if (svm.SalesRegister == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name", svm.SalesRegister.AreaID);
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name", svm.SalesRegister.DsrID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name", svm.SalesRegister.ManufacturerID);
            ViewBag.SrID = new SelectList(db.Srs, "ID", "Name", svm.SalesRegister.SrID);
            ViewBag.products = new SelectList(db.Product, "ID", "Name", svm.SalesRegister.ProductID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name");
            return View(svm);
        }

        // GET: Sales/editSalesRegister
        [HttpPost]
        public JsonResult editSalesRegister(SalesRegisterViewModel srvm)
        {
            SalesRegister salesRegister = db.SalesRegister.Find(srvm.SalesRegister.ID);
            salesRegister.From = srvm.SalesRegister.From;
            salesRegister.To = srvm.SalesRegister.To;
            salesRegister.RegistrationDate = srvm.SalesRegister.RegistrationDate;
            salesRegister.DsrID = srvm.SalesRegister.DsrID;
            salesRegister.ManufacturerID = srvm.SalesRegister.ManufacturerID;
            salesRegister.AreaID = srvm.SalesRegister.AreaID;
            salesRegister.SrID = srvm.SalesRegister.SrID;
            salesRegister.ProductID = srvm.SalesRegister.ProductID;
            salesRegister.TotalInvoiceAmount = srvm.SalesRegister.TotalInvoiceAmount;


            var srl = db.SalesRegisterDetails.Where(a => a.SalesRegisterID == srvm.SalesRegister.ID).ToList();
            foreach (var vp in srl)
            {
                db.SalesRegisterDetails.Remove(vp);
                db.SaveChanges();
            }

            foreach (var item in srvm.SalesRegisterDetails)
            {
                item.SalesRegisterID = salesRegister.ID;
                db.SalesRegisterDetails.Add(item);
                db.SaveChanges();
            }

            //salesRegister edit
            db.Entry(salesRegister).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        // GET: SalesRegisters/Details
        public ActionResult Details(int? id)
        {
            SalesRegisterViewModel svm = new SalesRegisterViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            svm.SalesRegister = db.SalesRegister.Find(id);
            svm.CollectionItemDetails = collectionRepo.getSalesRegisterDetails(id, null, false, null);
            if (svm.SalesRegister == null)
            {
                return HttpNotFound();
            }
            return View(svm);
        }

        // GET: SalesRegisters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesRegister salesRegister = db.SalesRegister.Find(id);
            if (salesRegister == null)
            {
                return HttpNotFound();
            }
            try
            {
                var srl = db.SalesRegisterDetails.Where(a => a.SalesRegisterID == id).ToList();
                foreach (var vp in srl)
                    db.SalesRegisterDetails.Remove(vp);
                db.SaveChanges();

                var li = db.Expense.Where(a => a.SalesRegisterID == id).ToList();
                foreach (var item in li)
                    db.Expense.Remove(item);
                db.SaveChanges();

                var litwo = db.SalesRegisterChequeInformation.Where(a => a.SalesRegisterID == id).ToList();
                foreach (var item in litwo)
                    db.SalesRegisterChequeInformation.Remove(item);
                db.SaveChanges();

                db.SalesRegister.Remove(salesRegister);
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


