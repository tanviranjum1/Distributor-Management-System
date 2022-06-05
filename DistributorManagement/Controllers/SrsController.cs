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
    public class SrsController : Controller
    {
        private DistributorManagementContext db = new DistributorManagementContext();

        // GET: Srs
        public ActionResult Index(int? err)
        {
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            SrFilterViewModel svm = new SrFilterViewModel();
            if (err == 1)
            {
                ViewBag.Message = "Delete Failed!!";
            }
            return View(svm);
        }

        [HttpPost]
        public ActionResult Index(SrFilterViewModel svm)
        {
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");

            var Srs = db.Srs.AsQueryable();

            if (svm.Name != null)
            {
                Srs = Srs.Where(r => r.Name.Contains(svm.Name)).AsQueryable();
            }

            if (svm.Contact != null)
            {
                Srs = Srs.Where(r => r.Contact.Contains(svm.Contact)).AsQueryable();
            }

            if (svm.ManufacturerID != null)
            {
                Srs = Srs.Where(r => r.ManufacturerID == svm.ManufacturerID).AsQueryable();
            }
            svm.SrsList = Srs.ToList();
            return View(svm);
        }

    
        // GET: Srs/Create
        public ActionResult Create()
        {
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            return View();
        }

        // POST: Srs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,Contact,ManufacturerId")] Sr sr)
        {
            if (ModelState.IsValid)
            {
                db.Srs.Add(sr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Manufacturer manufacturer = new Manufacturer();
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name", manufacturer.ID);
            return View(sr);
        }

        // GET: Srs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sr sr = db.Srs.Find(id);
            if (sr == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name", sr.ManufacturerID);
            return View(sr);
        }

        // POST: Srs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,Contact,ManufacturerId")] Sr sr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name", sr.ManufacturerID);
            return View(sr);
        }

        // GET: Srs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sr sr = db.Srs.Find(id);
            if (sr == null)
            {
                return HttpNotFound();
            }
            try
            {
                db.Srs.Remove(sr);
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
