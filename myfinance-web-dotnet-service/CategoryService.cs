using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_infra;

namespace myfinance_web_dotnet_service.interfaces
{
  public class CategoryService : ICategoryService
  {
    private readonly MyFinanceDbContext _dbContext;

    public CategoryService(MyFinanceDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void delete(int id)
    {
      var category = new Category() { id = id };
      _dbContext.Attach(category);
      _dbContext.Remove(category);
      _dbContext.SaveChanges();
    }

    public List<Category> getCategories()
    {
      var dbSet = _dbContext.Category;
      return dbSet.ToList();
    }

    public Category getCategory(int id)
    {
      var dbSet = _dbContext.Category;
      return dbSet.Where(x => x.id == id).First();
    }

    public void upsert(Category entity)
    {
      var dbSet = _dbContext.Category;

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