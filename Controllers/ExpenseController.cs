using Microsoft.AspNetCore.Mvc;
using ExpenseManager.Models;
using ExpenseManager.Interfaces;

namespace ExpenseManager.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService _expenseService)
        {
            expenseService = _expenseService;
        }
        public IActionResult Index(string searchString)
        {
            List<ExpenseReport> listExpense = new List<ExpenseReport>();
            listExpense = expenseService.GetAllExpenses().ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                listExpense = expenseService.GetSearchResult(searchString).ToList();
            }
            return View(listExpense);
        }

        public ActionResult AddEditExpenses(int itemId)
        {
            ExpenseReport model = new ExpenseReport();
            if (itemId > 0)
            {
                model = expenseService.GetExpenseData(itemId);
            }
            return PartialView("_expenseForm", model);
        }

        [HttpPost]
        public ActionResult Create(ExpenseReport newExpense)
        {
            if (ModelState.IsValid)
            {
                if (newExpense.ItemId > 0)
                {
                    expenseService.UpdateExpense(newExpense);
                }
                else
                {
                    expenseService.AddExpense(newExpense);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            expenseService.DeleteExpense(id);
            return RedirectToAction("Index");
        }

        public ActionResult ExpenseSummary()
        {
            return PartialView("_expenseReport");
        }

        public JsonResult GetMonthlyExpense()
        {
            Dictionary<string, decimal> monthlyExpense = expenseService.CalculateMonthlyExpense();
            return new JsonResult(monthlyExpense);
        }

        public JsonResult GetWeeklyExpense()
        {
            Dictionary<string, decimal> weeklyExpense = expenseService.CalculateWeeklyExpense();
            return new JsonResult(weeklyExpense);
        }
    }
}
