﻿using AutoMapper;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using JustStore.Models;
using JustStore.Models.ViewModels;
using JustStore.Utlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JustStoreMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
			_mapper = mapper;
        }

        public IActionResult Index()
        {
			var productEntity = _unitOfWork.Product
				.GetAll(includeProperties: "Category").ToList();
            List<Product> ObjectsFromDb = _mapper.Map<List<Product>>(productEntity);

            return View(ObjectsFromDb);
        }

		public IActionResult Upsert(int? id) // = Update + Insert 
		{
			ProductVM productVM = new()
			{
				 CategoryList = _unitOfWork.Category.GetAll()
				.Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.ID.ToString()
				}),
				Product = new Product()
			};
			if(id == null || id == 0) 
			{
				//create
				return View(productVM);
			}
			else 
			{
				var productEntityWithImage = _unitOfWork.Product.GetFirstOrDefault(u => u.ID == id,
                    includeProperties: "ProductImages");
				//update
				productVM.Product = _mapper.Map<Product>(productEntityWithImage);
				return View(productVM);
			}
		}
		[HttpPost]
		public IActionResult Upsert(ProductVM productVM, List<IFormFile> files)
		{
			if (ModelState.IsValid)
			{
				ProductEntity ProductToDb = (_mapper.Map<ProductEntity>(productVM.Product));

                if (productVM.Product.ID == 0)
				{
					_unitOfWork.Product.Add(ProductToDb);
					TempData["success"] = "Product created successfully";
				}
				else
				{
					_unitOfWork.Product.Update(ProductToDb);
					TempData["success"] = "Product updated successfully";
				}
				_unitOfWork.save();

                productVM.Product.ID = ProductToDb.ID;

                string wwwRootPath = _webHostEnvironment.WebRootPath;

				if(files != null) 
				{
					foreach (IFormFile file in files) 
					{
						string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

						string productPath = @"images\products\product-"+productVM.Product.ID;

						string finalPath = Path.Combine(wwwRootPath, productPath);		

						if (!Directory.Exists(finalPath)) { Directory.CreateDirectory(finalPath); }

						using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
						{
							file.CopyTo(fileStream);
						}

						ProductImage productImage = new() 
						{
							ImageUrl = @"\" + productPath + @"\" + fileName,
							ProductId = productVM.Product.ID	
						};

						if(productVM.Product.ProductImages == null)
							productVM.Product.ProductImages = new List<ProductImage>();

						productVM.Product.ProductImages.Add(productImage);
					}

					_unitOfWork.Product.Update(_mapper.Map<ProductEntity>(productVM.Product));
					_unitOfWork.save();

				}
				
				return RedirectToAction("Index");
			}
			else
			{

				productVM.CategoryList = _unitOfWork.Category.GetAll()
				.Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.ID.ToString()
				});
				return View(productVM);
			}

		}

		public IActionResult DeleteImage(int imageId) 
		{
			var imageToBeDeleted = _unitOfWork.ProductImages.GetFirstOrDefault(u => u.ID == imageId);
			int prodeuctId = imageToBeDeleted.ProductId;
			if(imageToBeDeleted != null) 
			{
				if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl)) 
				{

					var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
									imageToBeDeleted.ImageUrl.TrimStart('\\'));

					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}

				}

				_unitOfWork.ProductImages.Delete(imageToBeDeleted);
				_unitOfWork.save();

				TempData["success"] = "Deleted successfully";
			}

			return RedirectToAction(nameof(Upsert), new {id= prodeuctId});
		}

		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			var productsFromDb = _unitOfWork.Product
                           .GetAll(includeProperties: "Category").ToList();

            List<Product> ObjectsFromDbMapped = _mapper.Map<List<Product>>(productsFromDb);
			return Json(new { data = ObjectsFromDbMapped });
        }
		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var productToBeDeleted = _unitOfWork.Product.GetFirstOrDefault(u=>u.ID == id);
			if (productToBeDeleted == null) 
			{
				return Json(new { succes = false, message = "Error while deleting" });
			}

			string productPath = @"images\products\product-" + id;

			string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

			if (Directory.Exists(finalPath)) 
			{
				string[] filePaths = Directory.GetFiles(finalPath);

				foreach (string filePath in filePaths) 
				{
					System.IO.File.Delete(filePath);
				}

				Directory.Delete(finalPath);
			}

			_unitOfWork.Product.Delete(productToBeDeleted);
			_unitOfWork.save();

			return Json(new { succes = true, message = "Delete Successful"});

		}
		#endregion
	}
}