using AutoMapper;
using DataAccess.Entity;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using JustStore.Models;
using JustStore.Utlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Diagnostics;
using System.Security.Claims;

namespace JustStoreMVC.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var ProductsFromDb = _unitOfWork.Product
                .GetAll(includeProperties: "Category,ProductImages");
            IEnumerable<JustStore.Models.Product> productList = _mapper.Map<IEnumerable<JustStore.Models.Product>>(ProductsFromDb);
            return View(productList);
        }
        
        public IActionResult Details(int id)
        {
            var productEntity = _unitOfWork.Product
                .GetFirstOrDefault(u => u.ID == id, includeProperties: "Category,ProductImages");

            ShoppingCart cart = new()
            {
                Product = _mapper.Map<JustStore.Models.Product>(productEntity),
                Count = 1,
                ProductId = id
            };
           
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userID;
            shoppingCart.Id = 0;

            ShoppingCartEntity shoppingCartFromDb = _unitOfWork.ShoppingCart
                .GetFirstOrDefault(u => u.ApplicationUserId == userID 
                && u.ProductId == shoppingCart.ProductId);

            if (shoppingCartFromDb != null)
            {
                shoppingCartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(shoppingCartFromDb);
                _unitOfWork.save();
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(_mapper.Map<ShoppingCartEntity>(shoppingCart));
                _unitOfWork.save();
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart
				    .GetAll(u => u.ApplicationUserId == userID).Count());
            }

            TempData["success"] = "Cart updated successfully";
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
