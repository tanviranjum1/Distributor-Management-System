using DistributorManagement.Models;
using DistributorManagement.Repositories;
using DistributorManagement.ViewModels;
using System.Linq;
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



		public ActionResult CompanyWiseExpenseReport()
		{
			CompanyWisExpenseReportFilterViewModel cerfv = new CompanyWisExpenseReportFilterViewModel();
			ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
			ViewBag.ProductID = new SelectList(db.Product, "ID", "Name");

			return View(cerfv);
		}

		[HttpPost]
		public ActionResult CompanyWiseExpenseReport(CompanyWisExpenseReportFilterViewModel cerfv)
		{
			ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ID", "Name");
			ViewBag.ProductID = new SelectList(db.Product, "ID", "Name");

			string SqlQuery = @"select result.date as Date, result.ExpenseHead as ExpenseHead, sum(result.AmountEx+result.AmountCol) as Amount  from(
				   SELECT Expense.Date as date, (Select Name from ExpenseHead where ID= Expense.ExpenseHeadID)as ExpenseHead, 
				   SUM(Expense.Amount) as AmountEx,
				   0 as  Amountcol
				   FROM Expense
				   WHERE Expense.ManufacturerID ='" + cerfv.ManufacturerID + "'OR Expense.ProductID = '" + cerfv.ProductID + @"'
				   GROUP BY Expense.ExpenseHeadID, Expense.Date
				   UNION
				   SELECT  Collection.Date as date, (Select Name from ExpenseHead where ID= CollectionExpense.ExpenseHeadID)as ExpenseHead, 
				   0 as AmountEx,
				   SUM(CollectionExpense.Amount) as Amountcol
				   FROM CollectionExpense
				   INNER JOIN Collection ON CollectionExpense.CollectionID = Collection.ID       
				   WHERE CollectionExpense.ManufacturerID ='" + cerfv.ManufacturerID + "'OR CollectionExpense.ProductID = '" + cerfv.ProductID +
				   "'GROUP BY CollectionExpense.ExpenseHeadID, Collection.Date) as result group by result.date ,result.ExpenseHead";

			cerfv.CompanyWiseExpenseReportItemDetails = db.Database.SqlQuery<CompanyWiseExpenseReportItemDetailViewModel>(SqlQuery).ToList();
			return View(cerfv);
		}



		public ActionResult SaleRegisterWiseCollectionSummaryReport()
		{
			SalesRegisterCollectionSummaryReportFilterViewModel scsfvm = new SalesRegisterCollectionSummaryReportFilterViewModel();
			ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");
			return View(scsfvm);
		}

		[HttpPost]
		public ActionResult SaleRegisterWiseCollectionSummaryReport(SalesRegisterCollectionSummaryReportFilterViewModel scsfvm)
		{

			ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");

			string SqlQuery = @" SELECT result.SaleRegisterID as SalesRegisterID, result.TotalInvoice as TotalInvoice, (result.CashCollection + result.CollectionCashAmountSum) as CollectionCash
			,(result.CheckCollection) as CollectionCheque , (result.TotalReturnAmount + result.ReturnAmountSum) as ReturnAmount, 
			(result.InvoiceAm - result.CollectionCashAmountSum - result.ReturnAmountSum) as DueAmount, 
			result.expense as Expenses, result.DsrId as DsrID, result.Date as Date
			FROM( Select SalesRegister.ID as SaleRegisterID, SalesRegister.DsrID as DsrId, SalesRegister.RegistrationDate as Date, SalesRegister.TotalInvoiceAmount as TotalInvoice,
			SalesRegister.CashCollection as CashCollection, sum(SalesRegisterChequeInformation.Amount) as CheckCollection,
			SUM(CollectionDetails.CollectionAmount) as CollectionCashAmountSum,
			SUM(CollectionDetails.ReturnAmount) as ReturnAmountSum,
			SUM(SalesRegisterDetails.InvoiceAmount) as InvoiceAm,
			SUM(Expense.Amount) as expense,
			SalesRegister.TotalReturnAmount as TotalReturnAmount
			from SalesRegister left JOIN SalesRegisterDetails on SalesRegister.ID = SalesRegisterDetails.SalesRegisterID
			LEFT JOIN SalesRegisterChequeInformation on SalesRegister.ID = SalesRegisterChequeInformation.SalesRegisterID
			LEFT JOIN CollectionDetails on SalesRegisterDetails.ID = CollectionDetails.SalesRegisterDetailsID
			LEFT JOIN Expense on SalesRegister.ID = Expense.SalesRegisterID 
			WHERE SalesRegister.RegistrationDate = '" + scsfvm.Date + "'OR SalesRegister.DsrID = '" + scsfvm.DsrID + "'GROUP BY SalesRegister.ID, SalesRegister.TotalInvoiceAmount, SalesRegister.DsrID, SalesRegister.RegistrationDate, CashCollection, TotalReturnAmount ) as result ";

			scsfvm.SalesRegisterCollectionSummaryReportItemDetails = db.Database.SqlQuery<SalesRegisterCollectionSummaryReportItemDetailViewModel>(SqlQuery).ToList();

			string SqlQueryTwo = @"select* from(
			 select SalesRegister.DsrID as DsrId, SalesRegister.RegistrationDate as Date, Customer.Code as CustomerCode, Customer.Name as CustomerName, SalesRegisterDetails.InvoiceNumber
			 as InvoiceNumber, (SalesRegisterDetails.InvoiceAmount - CollectionDetails.CollectionAmount - CollectionDetails.ReturnAmount) as DueAmount
			 from SalesRegister 
			 left join SalesRegisterDetails on SalesRegister.ID = SalesRegisterDetails.SalesRegisterID
			 inner join Customer on SalesRegisterDetails.CustomerID = Customer.ID
			 inner join CollectionDetails on SalesRegisterDetails.ID = CollectionDetails.SalesRegisterDetailsID) as result
			 where result.DueAmount > 0 AND (result.Date = '" + scsfvm.Date + "'OR result.DsrID = '" + scsfvm.DsrID + "')";

			scsfvm.DueDetailsList = db.Database.SqlQuery<DueDetails>(SqlQueryTwo).ToList();

			string SqlQueryThree =
			@" SELECT(Select Name from ExpenseHead where ID = Expense.ExpenseHeadID) as HeadName, Expense.Amount as Amount, 
			Expense.Date as Date from Expense
			INNER JOIN SalesRegister ON Expense.SalesRegisterID = SalesRegister.ID
			WHERE SalesRegister.RegistrationDate = '" + scsfvm.Date + "'OR SalesRegister.DsrID = '" + scsfvm.DsrID + @"'
			UNION  
			SELECT(SELECT Name from ExpenseHead where ID = CollectionExpense.ExpenseHeadID)as Head, CollectionExpense.Amount, 
			Collection.Date from CollectionExpense inner join Collection
			on CollectionExpense.CollectionID = Collection.ID
			where[CollectionID] in(
			SELECT CollectionDetails.CollectionID from SalesRegister inner join SalesRegisterDetails
			on SalesRegister.ID = SalesRegisterDetails.SalesRegisterID inner join
			CollectionDetails on CollectionDetails.SalesRegisterDetailsID = SalesRegisterDetails.ID
			where CollectionDetails.CollectionID = Collection.ID AND (SalesRegister.RegistrationDate = '" + scsfvm.Date + "'OR SalesRegister.DsrID = '" + scsfvm.DsrID + "'))";

			scsfvm.ExpenditureDetailsList = db.Database.SqlQuery<ExpenditureDetails>(SqlQueryThree).ToList();

			return View(scsfvm);
		}
	}
}




//public ActionResult DueCollectionReport()
//{
//    DueCollectionReportFilterViewModel rfvm = new DueCollectionReportFilterViewModel();
//    ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");
//    return View(rfvm);
//}


//[HttpPost]
//public ActionResult DueCollectionReport(DueCollectionReportFilterViewModel dcfv)
//{
//    ViewBag.DsrID = new SelectList(db.Dsrs, "ID", "Name");

//    DateTime To = dcfv.To.AddDays(1);
//    string SqlQuery = @"
//                select [Collection].Date as Date, [Customer].Code as RetailerCode, [Customer].Name as RetailerName, 
//                SalesRegisterDetails.InvoiceNumber as InvoiceNumber, 
//               [SalesRegister].RegistrationDate as InvoiceDate,
//                SalesRegisterDetails.InvoiceAmount as DueAmount,
//               [CollectionDetails].CollectionAmount as PaidAmount,
//               (SalesRegisterDetails.InvoiceAmount-[CollectionDetails].CollectionAmount) as Balance
//                from CollectionDetails 
//               inner join SalesRegisterDetails on CollectionDetails.SalesRegisterDetailsID=SalesRegisterDetails.ID 
//               inner join Customer on Customer.ID= SalesRegisterDetails.CustomerID
//               inner join SalesRegister on SalesRegister.ID= SalesRegisterDetails.SalesRegisterID
//               inner join Collection on Collection.ID = CollectionDetails.CollectionID
//                Where (Collection.DsrID='" + dcfv.DsrID + "'AND Collection.Date BETWEEN'" + dcfv.From + "' AND  '" + To + @"')";

//    dcfv.DueCollectionReportItemDetails = db.Database.SqlQuery<DueCollectionReportItemDetailViewModel>(SqlQuery).ToList();
//    return View(dcfv);
//}

















//WHERE SalesRegister.RegistrationDate = '" + scsfvm.Date + "'AND SalesRegister.DsrID = '" + scsfvm.DsrID + 

//string SqlQuery = @"
//      SELECT SalesRegister.ID as SalesRegisterID, SalesRegister.TotalInvoiceAmount as TotalInvoice, SalesRegister.TotalReturnAmount as ReturnAmount, SalesRegister.CashCollection as CollectionCash,SUM(SalesRegisterChequeInformation.Amount) as CollectionCheque, 
//      SUM(SalesRegisterDetails.InvoiceAmount) as DueAmount, SUM(Expense.Amount) as Expenses
//     FROM SalesRegister
//     INNER JOIN  SalesRegisterChequeInformation ON SalesRegister.ID = SalesRegisterChequeInformation.SalesRegisterID
//     INNER JOIN  SalesRegisterDetails ON SalesRegister.ID = SalesRegisterDetails.SalesRegisterID
//     INNER JOIN  Expense ON SalesRegister.ID = Expense.SalesRegisterID
//       WHERE SalesRegister.RegistrationDate='" + scsfvm.Date + "'AND SalesRegister.DsrID ='" + scsfvm.DsrID + "' GROUP BY SalesRegister.ID, SalesRegister.TotalInvoiceAmount,  SalesRegister.CashCollection, SalesRegister.TotalReturnAmount ";

//scsfvm.SalesRegisterCollectionSummaryReportItemDetails = db.Database.SqlQuery<SalesRegisterCollectionSummaryReportItemDetailViewModel>(SqlQuery).ToList();
//return View(scsfvm);


//sql1 =" 

//    "


//cerfv.SalesRegisterCollectionSummaryReportItemDetailViewModel = db.Database.SqlQuery<SalesRegisterCollectionSummaryReportItemDetailViewModel>(sql1).ToList();


//sql2 =" 

//    "


//cerfv.DueDetails = db.Database.SqlQuery<DueDetails>(sql1).ToList();


//     select(Select Name from ExpenseHead where ID = Expense.ExpenseHeadID)as Head, Expense.Amount as Amount, Expense.Date as Date from Expense

// where SalesRegisterID =  1

// union

// select(Select Name from ExpenseHead where ID = CollectionExpense.ExpenseHeadID)as Head, CollectionExpense.Amount, Collection.Date from CollectionExpense inner join Collection
// on CollectionExpense.CollectionID= Collection.ID
// where [CollectionID] in(
//     select CollectionDetails.CollectionID from SalesRegister inner join SalesRegisterDetails
//     on SalesRegister.ID = SalesRegisterDetails.SalesRegisterID inner join
//     CollectionDetails on CollectionDetails.SalesRegisterDetailsID = SalesRegisterDetails.ID
//     where CollectionDetails.CollectionID = Collection.ID and SalesRegisterID = 1
//)



//SELECT  Expense.Date, (Select Name from ExpenseHead where ID= Expense.ExpenseHeadID)as ExpenseHead, Expense.Amount
//FROM Expense
//LEFT JOIN ExpenseHead ON   Expense.ExpenseHeadID = ExpenseHead.ID
//WHERE Expense.ManufacturerID ='" + cerfv.ManufacturerID + "'AND Expense.ProductID = '" + cerfv.ProductID + @"'
//   Union
//   SELECT  Collection.Date, (Select Name from ExpenseHead where ID= CollectionExpense.ExpenseHeadID)as ExpenseHead, CollectionExpense.Amount
//   FROM CollectionExpense
//   LEFT JOIN Collection ON CollectionExpense.CollectionID = Collection.ID
//   WHERE CollectionExpense.ManufacturerID ='" + cerfv.ManufacturerID + "'AND CollectionExpense.ProductID = '" + cerfv.ProductID + "'";


