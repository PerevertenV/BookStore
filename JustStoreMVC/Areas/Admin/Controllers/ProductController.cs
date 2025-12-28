using BookStore.Services.IServices;
using DataAccess.Models;
using DataAccess.Models.ViewModels;
using DataAccess.Utlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustStoreMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var productList = _service.Product.GetAllProducts();
            return View(productList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _service.Product.GetCategoryList(),
                Product = _service.Product.GetProductById(id) ?? new Product()
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                _service.Product.UpsertProduct(productVM.Product, files, _webHostEnvironment.WebRootPath);
                TempData["success"] = "Product created/updated successfully";
                return RedirectToAction("Index");
            }

            productVM.CategoryList = _service.Product.GetCategoryList();
            return View(productVM);
        }

        public IActionResult DeleteImage(int imageId)
        {
           var deletingResult = _service.Product.DeleteImage(imageId, _webHostEnvironment.WebRootPath);

            if(deletingResult == 0)
            {
                TempData["error"] = "Error while deleting image";
                return RedirectToAction(nameof(DeleteImage) , new {imageId = imageId});
            }

            TempData["success"] = "Image deleted successfully";
            return RedirectToAction(nameof(Upsert), new { id = deletingResult });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _service.Product.GetAllProducts();
            return Json(new { data = data });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var result = _service.Product.DeleteProduct(id, _webHostEnvironment.WebRootPath);

            if (!result)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}