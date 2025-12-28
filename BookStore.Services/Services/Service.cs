using BookStore.Services.IServices;
using DataAccess.Repository.IRepository;

namespace BookStore.Services.Services;

public class Service: IService
{

    public ICategoryService Category { get; private set; }
    public IProductService Product { get; private set; }

    public Service(IUnitOfWork unitOfWork)
    {
        Category = new CategoryService(unitOfWork);
        Product = new ProductService(unitOfWork);
    }
}
