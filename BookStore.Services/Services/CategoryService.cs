using DataAccess.Repository.IRepository;
using BookStore.Services.IServices;
using DataAccess.Models;


namespace BookStore.Services.Services;

internal class CategoryService(IUnitOfWork unitOfWork): ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public bool CreateNewCategory(Category? category)
    {
        if (category == null || category.DisplayOrder.ToString() == category.Name) { return false; }

        var categoriesFromDb = _unitOfWork.Category.GetAll().ToList();

        if(categoriesFromDb.Any(c => c != null && c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        _unitOfWork.Category.Add(category);

        return true;
    }

    public List<Category?> GetAllCategories()
    {
        return _unitOfWork.Category.GetAll().ToList();
    } 

    public Category? GetCategoryById(int? id)
    {
        if(id == null || id == 0) { return null; }
        return _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
    }

    public bool UpdateCategory(Category? category)
    {
        if (category == null || category.DisplayOrder.ToString() == category.Name) { return false; }

        try
        {
            _unitOfWork.Category.Update(category);
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    public bool DeleteCategory(int? id)
    {
        if(id == 0 || id == null) { return false; }

        var categoryToBeDeleted = _unitOfWork.Category.GetFirstOrDefault(u  => u.Id == id);
        if(categoryToBeDeleted == null) { return false; }

        _unitOfWork.Category.Delete(categoryToBeDeleted);
        return true;
    }
}
