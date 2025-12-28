using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Services.IServices
{
    public interface IProductService
    {
        List<Product?> GetAllProducts();
        Product? GetProductById(int? id);
        void UpsertProduct(Product product, List<IFormFile>? files, string webRootPath);
        bool DeleteProduct(int? id, string webRootPath);
        int DeleteImage(int imageId, string webRootPath);
        IEnumerable<SelectListItem> GetCategoryList();
    }
}