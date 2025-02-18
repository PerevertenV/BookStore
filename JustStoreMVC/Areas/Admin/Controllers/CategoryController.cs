﻿using AutoMapper;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using JustStore.Models;
using JustStore.Utlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustStoreMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            List<Category> objectFromDB = _mapper.Map<List<Category>>(_unitOfWork.Category.GetAll().ToList());
            return View(objectFromDB);
        }

        public IActionResult CreateNewCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewCategory(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("name", "The Display Order can`t exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(_mapper.Map<CategoryEntity>(obj));
                _unitOfWork.save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult EditCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? CategoryFromDBbyID = _mapper.Map<Category>(_unitOfWork.Category
                .GetFirstOrDefault(u => u.ID == id));

            if (CategoryFromDBbyID == null)
            {
                TempData["error"] = "Category wasn`t updated";
                return NotFound();
            }
            return View(CategoryFromDBbyID);
        }
        [HttpPost]
        public IActionResult EditCategory(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order can`t exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(_mapper.Map<CategoryEntity>(obj));
                _unitOfWork.save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? CategoryFromDBbyID = _mapper.Map<Category>(_unitOfWork.Category
                .GetFirstOrDefault(u => u.ID == id));

            if (CategoryFromDBbyID == null)
            {
                return NotFound();
            }
            return View(CategoryFromDBbyID);
        }
        [HttpPost, ActionName("DeleteCategory")]
        public IActionResult DeleteCategoryPOST(int? id)
        {
            Category? CategoryFromDBbyID = _mapper.Map<Category>(_unitOfWork.Category
                .GetFirstOrDefault(u => u.ID == id));

            if (CategoryFromDBbyID == null)
            {
                TempData["error"] = "Category wasn`t deleted";
                return NotFound();
            }
            _unitOfWork.Category.Delete(_mapper.Map<CategoryEntity>(CategoryFromDBbyID));
            _unitOfWork.save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
