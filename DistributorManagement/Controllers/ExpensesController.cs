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
    public class ExpensesController : Controller
    {
        private DistributorManagementContext db = new DistributorManagementContext();

        // GET: Expenses
        public ActionResult Index(int? err)
        {
            //var expense = db.Expense.Include(e => e.ExpenseHead);
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name");
            ExpenseFilterViewModel efvm = new ExpenseFilterViewModel();

            DateTime nextDate = efvm.To.AddDays(1);

            efvm.ExpenseList = db.Expense.Where(r => r.Date >= efvm.From && r.Date < nextDate).ToList();

            if (err == 1)
            {
                ViewBag.Message = "Delete Failed!!";
            }
            return View(efvm);
        }

        [HttpPost]
        public ActionResult Index(ExpenseFilterViewModel efvm)
        {
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name");
            DateTime To = efvm.To.AddDays(1);

            var Expenses = db.Expense.AsQueryable();
            Expenses = Expenses.Where(f => f.Date >= efvm.From && f.Date < To).OrderBy(r => r.Date).AsQueryable();

            if (efvm.ExpenseHeadID != null)
            {
                Expenses = Expenses.Where(r => r.ID == efvm.ExpenseHeadID).AsQueryable();
            }
            efvm.ExpenseList = Expenses.ToList();

            return View(efvm);
        }


        // GET: Expenses/Create
        public ActionResult Create()
        {
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name");
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");
            return View();
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Date, Amount, ExpenseHeadID, ProductID, DsrID, ManufacturerID")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Expense.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name", expense.ExpenseHeadID);
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name", expense.ProductID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name", expense.ManufacturerID);
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name", expense.DsrID);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expense.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name", expense.ExpenseHeadID);
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name", expense.ProductID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name", expense.ManufacturerID);
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name", expense.DsrID);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpenseHeadID = new SelectList(db.ExpenseHead, "ID", "Name", expense.ExpenseHeadID);
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name", expense.ProductID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name", expense.ManufacturerID);
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name", expense.DsrID);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expense.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            try
            {
                db.Expense.Remove(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
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
