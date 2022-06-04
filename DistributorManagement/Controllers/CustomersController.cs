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

namespace DistributorManagement.Controllers
{
    public class CustomersController : Controller
    {
        private DistributorManagementContext db = new DistributorManagementContext();


        public ActionResult Settings(int? id)
        {
            ViewBag.ProductID = new MultiSelectList(db.Product, "ID", "Name");

            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetailerProductViewModel rpvm = new RetailerProductViewModel();
            rpvm.Customer = db.Customers.Find(id);
            rpvm.RetailerProduct = db.RetailerProduct.Where(f => f.CustomerID == id).OrderBy(r => r.Product.ManufacturerID).ToList();
            if (rpvm.Customer == null)
            {
                return HttpNotFound();
            }
            return View(rpvm);
        }

        //get product upon selection of dropdown value using Ajax.
        public JsonResult getProduct()
        {
            var product = db.Product.ToList();
            return Json(new { Product = product }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult insertRetailerProducts(RetailerProductViewModel rpvm)
        {
            var srl = db.RetailerProduct.Where(a => a.CustomerID == rpvm.Customer.ID).ToList();
            foreach (var vp in srl)
                db.RetailerProduct.Remove(vp);
            db.SaveChanges();

            foreach (var item in rpvm.RetailerProduct)
            {
                db.RetailerProduct.Add(item);
                db.SaveChanges();
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.AreaId = new SelectList(db.Areas, "ID", "Name");
            return View();
        }
        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaId = new SelectList(db.Areas, "ID", "Name", customer.AreaID);
            return View(customer);
        }

        public ActionResult Index(int? err)
        {
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name");
            CustomerFilterViewModel cvm = new CustomerFilterViewModel();

            if (err == 1)
            {
                ViewBag.Message = "Delete Failed!!";
            }

            return View(cvm);
        }

        [HttpPost]
        public ActionResult Index(CustomerFilterViewModel cvm)
        {
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name");
          
            var Customers = db.Customers.AsQueryable();

            if (cvm.Name != null)
            {
                Customers = Customers.Where(r => r.Name.Contains(cvm.Name)).AsQueryable();
            }

            if (cvm.Contact != null)
            {
                Customers = Customers.Where(r => r.Contact.Contains(cvm.Contact)).AsQueryable();
            }

            if (cvm.Code != null)
            {
                Customers = Customers.Where(r => r.Code.Contains(cvm.Code)).AsQueryable();
            }

            if (cvm.AreaID != null)
            {
                Customers = Customers.Where(r => r.AreaID == cvm.AreaID).AsQueryable();
            }

            cvm.CustomersList = Customers.ToList();
            return View(cvm);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name", customer.AreaID);
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name", customer.AreaID);
            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            try
            {
                var lists = db.RetailerProduct.Where(a => a.CustomerID == id).ToList();
                foreach (var item in lists)
                    db.RetailerProduct.Remove(item);
                db.SaveChanges();

                db.Customers.Remove(customer);
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
