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
    public class ManufacturersController : Controller
    {
        private DistributorManagementContext db = new DistributorManagementContext();

        // GET: Manufacturers
        public ActionResult Index(int? err)
        {
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name");
            ManufacturerFilterViewModel mvm = new ManufacturerFilterViewModel();

            if (err == 1)
            {
                ViewBag.Message = "Delete Failed!!";
            }

            return View(mvm);
        }

        [HttpPost]
        public ActionResult Index(ManufacturerFilterViewModel mvm)
        {
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name");
           
            var Manufacturers = db.Manufacturers.AsQueryable();

            if (mvm.Name != null)
            {
                Manufacturers = Manufacturers.Where(r => r.Name.Contains(mvm.Name)).AsQueryable();
            }

            if (mvm.Contact != null)
            {
                Manufacturers = Manufacturers.Where(r => r.Contact.Contains(mvm.Contact)).AsQueryable();
            }
            mvm.ManufacturersList = Manufacturers.ToList();

            return View(mvm);
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,Contact")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,Contact")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            try
            {
                db.Manufacturers.Remove(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception )
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
