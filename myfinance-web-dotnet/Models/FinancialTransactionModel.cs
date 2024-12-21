using Microsoft.AspNetCore.Mvc.Rendering;

namespace myfinance_web_dotnet.Models
{
    public class FinancialTransactionModel
    {
        public int? id { get; set; }

        public string historic { get; set; }

        public DateTime transactiondate { get; set; }
        public decimal amount { get; set; }

        public int categoryid { get; set; }

        public SelectList financialCategories { get; set; }
    }
}