using DataAccess.Models;

namespace BookStore.Services.IServices
{
    public interface ICategoryService
    {
        bool CreateNewCategory(Category? category);
        List<Category?> GetAllCategories();
        Category? GetCategoryById(int? id);
        bool UpdateCategory(Category? category);
        bool DeleteCategory(int? id);
    }
}
