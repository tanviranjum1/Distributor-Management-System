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
    public class ProductsController : Controller
    {
        private DistributorManagementContext db = new DistributorManagementContext();

        // GET: Products
        public ActionResult Index(int? err)
        {
           // var product = db.Product.Include(p => p.Manufacturer);
            ProductFilterViewModel pvm = new ProductFilterViewModel();
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            if (err == 1)
            {
                ViewBag.Message = "Delete Failed!!";
            }
            return View(pvm);
        }

        [HttpPost]
        public ActionResult Index(ProductFilterViewModel pvm)
        {
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            var Products = db.Product.OrderBy(r => r.ManufacturerID).AsQueryable();
            if (pvm.Name != null)
            {
                Products = Products.Where(r => r.Name.Contains(pvm.Name)).AsQueryable();
            }
            if (pvm.ManufacturerID != null)
            {
                Products = Products.Where(r => r.ManufacturerID == pvm.ManufacturerID).AsQueryable();
            }
            pvm.ProductList = Products.ToList();
            return View(pvm);
        }


        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,ManufacturerID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name", product.ManufacturerID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name", product.ManufacturerID);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,ManufacturerID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name", product.ManufacturerID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            try
            {
                db.Product.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
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
