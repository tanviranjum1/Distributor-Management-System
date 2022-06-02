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
    public class ExpenseHeadsController : Controller
    {
        private DistributorManagementContext db = new DistributorManagementContext();

        // GET: ExpenseHeads
        public ActionResult Index(int? err)
        {
            if (err == 1)
            {
                ViewBag.Message = "Delete Failed!!";
            }
            ExpenseHeadFilterViewModel ehfvm = new ExpenseHeadFilterViewModel();
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name");
            return View(ehfvm);
        }

        [HttpPost]
        public ActionResult Index(ExpenseHeadFilterViewModel ehfvm)
        {
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name");

            if (ehfvm.ExpenseHeadID != null)
            {
                ehfvm.ExpenseHeadList = db.ExpenseHead.Where(r => r.ID == ehfvm.ExpenseHeadID).ToList();
            }
            return View(ehfvm);
        }


        // GET: ExpenseHeads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseHeads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,OpeningBalance,DefaultValue")] ExpenseHead expenseHead)
        {
            if (ModelState.IsValid)
            {
                db.ExpenseHead.Add(expenseHead);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseHead);
        }

        // GET: ExpenseHeads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseHead expenseHead = db.ExpenseHead.Find(id);
            if (expenseHead == null)
            {
                return HttpNotFound();
            }
            return View(expenseHead);
        }

        // POST: ExpenseHeads/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,OpeningBalance,DefaultValue")] ExpenseHead expenseHead)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenseHead).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseHead);
        }

        // GET: ExpenseHeads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseHead expenseHead = db.ExpenseHead.Find(id);

            if (expenseHead == null)
            {
                return HttpNotFound();
            }
            try
            {
                db.ExpenseHead.Remove(expenseHead);
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
