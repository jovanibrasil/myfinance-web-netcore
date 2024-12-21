namespace myfinance_web_dotnet_domain.Entities;
public class FinancialTransaction
{

    public int? id { get; set; }

    public string historic { get; set; }

    public DateTime transactiondate { get; set; }
    public decimal amount { get; set; }

    public int categoryid { get; set; }

    public Category category { get; set; }

}
