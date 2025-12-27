using BookStore.Services.IServices;
using DataAccess.Models;
using DataAccess.Utlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustStoreMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IService _service;

        public CategoryController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.Category.GetAllCategories());
        }

        public IActionResult CreateNewCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewCategory(Category obj)
        {
            var addingResult = _service.Category.CreateNewCategory(obj);

            if (!addingResult)
            {
                TempData["error"] = "Incorrect data or this category already exists";
                return View();
            }

            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult EditCategory(int? id)
        {
            var categoryFromDb = _service.Category.GetCategoryById(id);

            if (categoryFromDb == null)
            {
                TempData["error"] = "Category doesn't exist";
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult EditCategory(Category obj)
        {
            var updatingResult = _service.Category.UpdateCategory(obj);

            if (!updatingResult)
            {
                TempData["error"] = "Incorrect data";
                return View();
            }

            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int? id)
        {
            var categoryFromDb = _service.Category.GetCategoryById(id);

            if (categoryFromDb == null)
            {
                TempData["error"] = "Category doesn't exist";
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("DeleteCategory")]
        public IActionResult DeleteCategoryPost(int? id)
        {
            var deletingResult = _service.Category.DeleteCategory(id);

            if (!deletingResult)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
