using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet_domain.Entities;

namespace myfinance_web_dotnet_infra;
public class MyFinanceDbContext : DbContext
{
  public DbSet<Category> Category { get; set; }
  public DbSet<FinancialTransaction> FinancialTransaction { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(@"Server=localhost;Database=myFinance;Trusted_Connection=True;");
  }

}
