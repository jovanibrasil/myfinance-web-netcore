using myfinance_web_dotnet_domain.Entities;

namespace myfinance_web_dotnet_service.interfaces
{
    public interface IFinancialTransactionService
    {

        void upsert(FinancialTransaction entity);
        void delete(int id);
        List<FinancialTransaction> getFinancialTransactions();
        FinancialTransaction getFinancialTransaction(int id);
    }
}