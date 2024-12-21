using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using myfinance_web_dotnet_domain.Entities;

namespace myfinance_web_dotnet_infra;
public class MyFinanceDbContext : DbContext
{
  private readonly IConfiguration _configuration;

  public MyFinanceDbContext(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public DbSet<Category> Category { get; set; }
  public DbSet<FinancialTransaction> FinancialTransaction { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Database"));
  }

}
