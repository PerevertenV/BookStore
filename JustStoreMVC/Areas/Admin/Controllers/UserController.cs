using AutoMapper;
using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using JustStore.Models;
using JustStore.Models.ViewModels;
using JustStore.Utlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JustStoreMVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class UserController : Controller
	{

		private readonly UserManager<IdentityUser> _um;
		private readonly RoleManager<IdentityRole> _rm;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> um, IMapper mapper,

            RoleManager<IdentityRole> rm)
		{
			_unitOfWork = unitOfWork;
			_um = um;
			_rm = rm;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult RoleManagment(string userId)
		{

            RoleManagmentVM RoleVM = new RoleManagmentVM()
			{
				ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId,
                    includeProperties: "Company"),

                RoleList = _rm.Roles.Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Name
				}),

				CompanyList = _unitOfWork.Company.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				})
			};

			RoleVM.ApplicationUser.Role = _um.GetRolesAsync(_unitOfWork.ApplicationUser
				.GetFirstOrDefault(u => u.Id == userId)).GetAwaiter().GetResult().FirstOrDefault();
			return View(RoleVM);
		}

		[HttpPost]
		public IActionResult RoleManagment(RoleManagmentVM rmvm) 
		{
			string oldRole = _um.GetRolesAsync(_unitOfWork.ApplicationUser
				.GetFirstOrDefault(u => u.Id == rmvm.ApplicationUser.Id))
				.GetAwaiter().GetResult().FirstOrDefault();

            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(_unitOfWork.ApplicationUser
				.GetFirstOrDefault(u => u.Id == rmvm.ApplicationUser.Id));

            if (!(rmvm.ApplicationUser.Role == oldRole)) 
			{

				if (rmvm.ApplicationUser.Role == SD.Role_Company) 
				{
					applicationUser.CompanyId = rmvm.ApplicationUser.CompanyId;
				}
				if(oldRole == SD.Role_Company) 
				{
					applicationUser.CompanyId = null;
				}

				_unitOfWork.ApplicationUser.Update(applicationUser);
				_unitOfWork.save();

				_um.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
				_um.AddToRoleAsync(applicationUser, rmvm.ApplicationUser.Role).GetAwaiter().GetResult();

			}
			else 
			{
				if(oldRole == SD.Role_Company && 
					applicationUser.CompanyId != rmvm.ApplicationUser.CompanyId) 
				{
					applicationUser.CompanyId = rmvm.ApplicationUser.CompanyId;
					_unitOfWork.ApplicationUser.Update(applicationUser);
					_unitOfWork.save();	
				}
			}

			return RedirectToAction(nameof(Index));
		}


		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
            List<ApplicationUser> ObjectsFromDb = _mapper.Map<List<ApplicationUser>>(_unitOfWork.ApplicationUser
				.GetAll(includeProperties:"Company")).ToList();
			 
			foreach (var p in ObjectsFromDb) 
			{
		
				p.Role = _um.GetRolesAsync(p).GetAwaiter().GetResult().FirstOrDefault();

				if(p.Company == null) { p.Company = new() {Name = "" }; }
			}
			return Json(new { data = ObjectsFromDb });
        }

		[HttpPost]
		public IActionResult LockUnlock([FromBody]string id)
		{
			var objFromDb = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
			if (objFromDb == null) 
			{
                return Json(new { succes = false, message = "Error while Locking/Unloking" });
            }

			if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now) 
			{
				objFromDb.LockoutEnd = DateTime.Now;
			}
			else 
			{
				objFromDb.LockoutEnd = DateTime.Now.AddDays(30);
            }
			_unitOfWork.ApplicationUser.Update(objFromDb);
			_unitOfWork.save(); 
            return Json(new { success = true, message = "Operation successful"});

		}
		#endregion
	}
}