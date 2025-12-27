using AutoMapper;
using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository;
public class UnitOFWork : IUnitOfWork
{
	public IProductRepository Product { get; private set; }
	public ICompanyRepository Company { get; private set; }
	public ICategoryRepository Category { get; private set; }
	public IOrderHeaderRepository OrderHeader { get; private set; }
	public IOrderDetailRepository OrderDetail { get; private set; }
	public IShoppingCartRepository ShoppingCart { get; private set; }
	public IProductImagesRepository ProductImages { get; private set; }
	public IApplicationUserRepository ApplicationUser { get; private set; }

	public UnitOFWork(AplicationDBContextcs db, IMapper mapper)
	{
		Product = new ProductRepository(db, mapper);
		Company = new CompanyRepository(db, mapper);
		Category = new CategoryRepository(db , mapper);
		OrderHeader = new OrderHeaderRepository(db, mapper);
		OrderDetail = new OrderDetailRepository(db, mapper);
		ShoppingCart = new ShoppingCartRepository(db, mapper);
		ProductImages = new ProductImagesRepository(db, mapper);
		ApplicationUser = new ApplicationUserRepository(db, mapper);
	}
}
