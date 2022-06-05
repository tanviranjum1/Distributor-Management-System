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
    public class DsrsController : Controller
    {
        private DistributorManagementContext db = new DistributorManagementContext();

        // GET: Dsrs
        public ActionResult Index(int? err)
        {
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name");
            DsrFilterViewModel dvm = new DsrFilterViewModel();
            if (err == 1)
            {
                ViewBag.Message = "Delete Failed!!";
            }
            return View(dvm);
        }

        [HttpPost]
        public ActionResult Index(DsrFilterViewModel dvm)
        {
            ViewBag.AreaID = new SelectList(db.Areas, "ID", "Name");

            var Dsrs = db.Dsrs.AsQueryable();

            if (dvm.Name != null)
            {
                Dsrs = Dsrs.Where(r => r.Name.Contains(dvm.Name)).AsQueryable();
            }

            if (dvm.Contact != null)
            {
                Dsrs = Dsrs.Where(r => r.Contact.Contains(dvm.Contact)).AsQueryable();
            }
            dvm.DsrsList = Dsrs.ToList();

            return View(dvm);
        }

        // GET: Dsrs/Create
        public ActionResult Create()
        {
            ViewBag.AreaId = new MultiSelectList(db.Areas, "ID", "Name");
            return View();
        }

        // POST: Dsrs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DsrAreaViewModel da)
        {
            if (ModelState.IsValid)
            {
                Dsr dsr = new Dsr()
                {
                    Name = da.Name,
                    Address = da.Address,
                    Contact = da.Contact,
                    NationalID = da.NationalID
                };
                db.Dsrs.Add(dsr);
                db.SaveChanges();

                foreach (var item in da.AreaID)
                {
                    DsrArea DsrArea = new DsrArea()
                    {
                        DsrID = dsr.ID,
                        AreaID = item
                    };
                    db.DsrArea.Add(DsrArea);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaId = new SelectList(db.Areas, "ID", "Name");
            return View(da);
        }

        // GET: Dsrs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Dsr = db.Dsrs.Find(id);
            if (Dsr == null)
            {
                return HttpNotFound();
            }
            DsrAreaViewModel DsrAreaViewModel = new DsrAreaViewModel()
            {
                Name = Dsr.Name,
                Address = Dsr.Address,
                Contact = Dsr.Contact,
                NationalID = Dsr.NationalID
            };
            DsrAreaViewModel.AreaID = db.DsrArea.Where(f => f.Dsr.ID == id).Select(r => r.AreaID).ToList();

            ViewBag.AreaID = new MultiSelectList(db.Areas, "ID", "Name" );
            return View(DsrAreaViewModel);
        }

        [HttpPost]
        public ActionResult Edit(DsrAreaViewModel davm)
        {
            Dsr dsr = db.Dsrs.Find(davm.ID);
            dsr.Name = davm.Name;
            dsr.Address = davm.Address;
            dsr.Contact = davm.Contact;
            dsr.NationalID = davm.NationalID;

            // dsr list delete
            var srl = db.DsrArea.Where(a => a.DsrID == davm.ID).ToList();
            foreach (var vp in srl)
            {
                db.DsrArea.Remove(vp);
                db.SaveChanges();
            }
            foreach (var item in davm.AreaID)
            {
                DsrArea DsrArea = new DsrArea()
                {
                    DsrID = dsr.ID,
                    AreaID = item
                };
                db.DsrArea.Add(DsrArea);
            }
            //Dsr edit
            db.Entry(dsr).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }


       // GET: Dsrs/Delete/5
       public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dsr dsr = db.Dsrs.Find(id);
            if (dsr == null)
            {
                return HttpNotFound();
            }
            try
            {
                var srl = db.DsrArea.Where(a => a.DsrID == id).ToList();
                foreach (var vp in srl)
                    db.DsrArea.Remove(vp);
                db.SaveChanges();

                db.Dsrs.Remove(dsr);
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
