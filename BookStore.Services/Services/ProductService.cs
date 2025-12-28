using BookStore.Services.IServices;
using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Services.Services
{
    internal class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public List<Product?> GetAllProducts()
        {
            return _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        }

        public Product? GetProductById(int? id)
        {
            if (id == null || id == 0) return null;
            return _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "ProductImages");
        }

        public IEnumerable<SelectListItem> GetCategoryList()
        {
            return _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
        }

        public void UpsertProduct(Product product, List<IFormFile>? files, string webRootPath)
        {
            if (product.Id == 0)
            {
                _unitOfWork.Product.Add(product);
            }
            else
            {
                _unitOfWork.Product.Update(product);
            }

            if (files != null && files.Count > 0)
            {
                string productPath = @"images\products\product-" + product.Id;
                string finalPath = Path.Combine(webRootPath, productPath);

                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);
                }

                foreach (IFormFile file in files)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(finalPath, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    ProductImage productImage = new()
                    {
                        ImageUrl = @"\" + productPath + @"\" + fileName,
                        ProductId = product.Id
                    };

                    if (product.ProductImages == null)
                        product.ProductImages = new List<ProductImage>();

                    _unitOfWork.ProductImages.Add(productImage);
                }
            }
        }

        public int DeleteImage(int imageId, string webRootPath)
        {
            var imageToBeDeleted = _unitOfWork.ProductImages.GetFirstOrDefault(u => u.Id == imageId);
            if (imageToBeDeleted == null) return 0;

            var productId = imageToBeDeleted.ProductId;

            if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
            {
                var oldImagePath = Path.Combine(webRootPath, imageToBeDeleted.ImageUrl.TrimStart('\\'));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            _unitOfWork.ProductImages.Delete(imageToBeDeleted);
            return productId;
        }

        public bool DeleteProduct(int? id, string webRootPath)
        {
            if (id == null || id == 0) return false;

            var productToBeDeleted = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (productToBeDeleted == null) return false;

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(webRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }

            _unitOfWork.Product.Delete(productToBeDeleted);
            return true;
        }
    }
}