using DistributorManagement.Models;
using DistributorManagement.Repositories;
using DistributorManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DistributorManagement.Controllers
{
    public class ReportController : Controller
    {
        private DistributorManagementContext db = new DistributorManagementContext();
        private CustomerDataRepository customerRepo = new CustomerDataRepository();

        [HttpGet]
        // GET: RetailerWiseReport
        public ActionResult CustomerDetailsReport()
        {
            CustomersReportFilterViewModel rfvm = new CustomersReportFilterViewModel();

            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name");

            return View(rfvm);
        }

        [HttpPost]
        public ActionResult CustomerDetailsReport(CustomersReportFilterViewModel rfvm)
        {
            ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
            ViewBag.ProductID = new SelectList(db.Product, "ID", "Name");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name");

            if (rfvm.CheckID == true)
            {
                rfvm.CustomersReportItemDetails = customerRepo.getSalesRegisterDetails(rfvm.From, rfvm.To,
                 rfvm.SaleRegisterID, rfvm.CustomerID, rfvm.DsrID, rfvm.ManufacturerID, rfvm.ProductID, true);
            }
            else
            {
                rfvm.CustomersReportItemDetails = customerRepo.getSalesRegisterDetails(rfvm.From, rfvm.To,
                   rfvm.SaleRegisterID, rfvm.CustomerID, rfvm.DsrID, rfvm.ManufacturerID, rfvm.ProductID, false);
            }
            return View(rfvm);
        }
    }
}