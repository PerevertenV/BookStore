using AutoMapper;
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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IMapper _mapper;

        public CompanyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Company> ObjectsFromDb = _mapper.Map<List<Company>>(_unitOfWork.Company.GetAll());
            return View(ObjectsFromDb);
        }

		public IActionResult Upsert(int? id) // = Update + Insert 
		{
			

			if(id == null || id == 0) 
			{
				//create
				return View(new Company());
			}
			else 
			{
                //update
                Company company = _mapper.Map<Company>(_unitOfWork.Company.GetFirstOrDefault(u => u.Id == id));
				return View(company);
			}
		}
		[HttpPost]
		public IActionResult Upsert(Company company)
		{
			if (ModelState.IsValid)
			{
				
				if(company.Id == 0) 
				{
					_unitOfWork.Company.Add(_mapper.Map<CompanyEntity>(company));
					TempData["success"] = "Product created successfully";
				}
				else 
				{
					_unitOfWork.Company.Update(_mapper.Map<CompanyEntity>(company));
					TempData["success"] = "Product updated successfully";
				}
				_unitOfWork.save();
				return RedirectToAction("Index");
			}
			else
			{
				return View(company);
			}

		}

		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
            List<Company> ObjectsFromDb = _mapper.Map<List<Company>>(_unitOfWork.Company.GetAll());
			return Json(new { data = ObjectsFromDb });
        }
		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var productToBeDeleted = _unitOfWork.Company.GetFirstOrDefault(u=>u.Id == id);
			if (productToBeDeleted == null) 
			{
				return Json(new { succes = false, message = "Error while deleting" });
			}

			_unitOfWork.Company.Delete(productToBeDeleted);
			_unitOfWork.save();

			return Json(new { succes = true, message = "Delete Successful"});

		}
		#endregion
	}
}