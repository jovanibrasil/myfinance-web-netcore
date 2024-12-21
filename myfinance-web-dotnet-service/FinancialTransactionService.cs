using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_infra;

namespace myfinance_web_dotnet_service.interfaces
{
  public class FinancialTransactionService : IFinancialTransactionService
  {
    private readonly MyFinanceDbContext _dbContext;

    public FinancialTransactionService(MyFinanceDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void delete(int id)
    {
      var financialTransaction = new FinancialTransaction() { id = id };
      _dbContext.Attach(financialTransaction);
      _dbContext.Remove(financialTransaction);
      _dbContext.SaveChanges();
    }

    public List<FinancialTransaction> getFinancialTransactions()
    {
      var dbSet = _dbContext.FinancialTransaction;
      return dbSet.ToList();
    }

    public FinancialTransaction getFinancialTransaction(int id)
    {
      var dbSet = _dbContext.FinancialTransaction;
      return dbSet.Where(x => x.id == id).First();
    }

    public void upsert(FinancialTransaction entity)
    {
      var dbSet = _dbContext.FinancialTransaction;


      if (entity.id == null)
      {
        dbSet.Add(entity);
      }
      else
      {
        dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
      }

      _dbContext.SaveChanges();
    }
  }
}