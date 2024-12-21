using myfinance_web_dotnet_domain.Entities;

namespace myfinance_web_dotnet_service.interfaces
{
    public interface ICategoryService
    {
        void upsert(Category entity);
        void delete(int id);
        List<Category> getCategories();
        Category getCategory(int id);
    }
}