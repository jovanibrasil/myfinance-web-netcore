using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myfinance_web_dotnet.Models;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_service.interfaces;

namespace myfinance_web_dotnet.Controllers
{
    [Route("[controller]")]
    public class FinancialTransactionController : Controller
    {
        private readonly ILogger<FinancialTransactionController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IFinancialTransactionService _financialTransactionService;

        public FinancialTransactionController(ILogger<FinancialTransactionController> logger, ICategoryService categoryService, IFinancialTransactionService financialCategoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _financialTransactionService = financialCategoryService;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            var listFinancialTransactions = _financialTransactionService.getFinancialTransactions();
            List<FinancialTransactionModel> listFinancialTransactionsModel = new List<FinancialTransactionModel>();

            foreach (var listCategoryItem in listFinancialTransactions)
            {
                var itemFinancialTransactionModel = new FinancialTransactionModel()
                {
                    id = listCategoryItem.id,
                    historic = listCategoryItem.historic,
                    transactiondate = listCategoryItem.transactiondate,
                    amount = listCategoryItem.amount
                };
                listFinancialTransactionsModel.Add(itemFinancialTransactionModel);
            }

            ViewBag.ListTransactions = listFinancialTransactionsModel;

            return View();
        }

        [HttpGet]
        [Route("Upsert")]
        [Route("Upsert/{id}")]
        public IActionResult Upsert(int? id)
        {
            var financialCategories = new SelectList(_categoryService.getCategories(), "id", "description");

            var financialTransactionModel = new FinancialTransactionModel()
            {
                financialCategories = financialCategories,
            };


            if (id != null)
            {
                var financialTransaction = _financialTransactionService.getFinancialTransaction((int)id);

                financialTransactionModel.id = financialTransaction.id;
                financialTransactionModel.amount = financialTransaction.amount;
                financialTransactionModel.historic = financialTransaction.historic;
                financialTransactionModel.transactiondate = financialTransaction.transactiondate;
                financialTransactionModel.categoryid = financialTransaction.categoryid;
            }

            return View(financialTransactionModel);
        }

        [HttpPost]
        [Route("Upsert")]
        [Route("Upsert/{id}")]
        public IActionResult Upsert(FinancialTransactionModel financialTransactionModel)
        {
            var financialTransaction = new FinancialTransaction()
            {
                id = financialTransactionModel.id,
                historic = financialTransactionModel.historic,
                transactiondate = financialTransactionModel.transactiondate,
                amount = financialTransactionModel.amount,
                categoryid = financialTransactionModel.categoryid,
            };

            _financialTransactionService.upsert(financialTransaction);

            return RedirectToAction("index");
        }

        // [HttpGet]
        // [Route("Remove/{id}")]
        // public IActionResult Remove(int? id)
        // {
        //     _categoryService.delete((int)id);

        //     return RedirectToAction("index");
        // }


        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }
    }
}