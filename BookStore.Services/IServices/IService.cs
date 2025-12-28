namespace BookStore.Services.IServices;

public interface IService
{
    public ICategoryService Category { get; }
    public IProductService Product { get; }
    public ICompanyService Company { get; }
    public ICartService Cart { get; }
    public IHomeService Home { get; }
    public IOrderService Order { get; }
    public IReportService Report { get; }
    public IUserService User { get; }
}
