namespace DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
	IProductRepository Product { get; }
	ICompanyRepository Company { get; }
	ICategoryRepository Category { get; }
	IOrderDetailRepository OrderDetail { get; }
	IOrderHeaderRepository OrderHeader { get; }
	IShoppingCartRepository ShoppingCart { get; }
	IProductImagesRepository ProductImages { get; }
	IApplicationUserRepository ApplicationUser { get; }
}
