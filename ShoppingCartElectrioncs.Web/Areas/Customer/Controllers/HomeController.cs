using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartElectrioncs.Entities.Models;
using ShoppingCartElectrioncs.Entities.Rebositories;
using ShoppingCartElectrioncs.Utilities;
using System.Dynamic;
using System.Security.Claims;
using X.PagedList.Extensions;

namespace ShoppingCartElectrioncs.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int ? page)
        {
            var PageNumber = page ?? 1;
            int PageSize = 8;

            var products=_unitOfWork.Product.GetAll().ToPagedList(PageNumber, PageSize);
            return View(products);
        }
        public IActionResult Details(int ProductId )
        {
            ShoppingCart obj = new ShoppingCart()
            {
                productId= ProductId,
                product = _unitOfWork.Product.GetFirstOrDefault(v => v.Id == ProductId, IncludeWord: "Category"),
                Count = 1
            };
             
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart )
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
             shoppingCart.applicationUserId = claim.Value;

            ShoppingCart CartObj = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                u => u.applicationUserId == claim.Value && u.productId == shoppingCart.productId);
                
            if (CartObj == null) 
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Comblete();
                HttpContext.Session.SetInt32(SD.SessionKey,
                    _unitOfWork.ShoppingCart.GetAll(x=>x.applicationUserId==claim.Value).ToList().Count());

            }
            else
            {
                _unitOfWork.ShoppingCart.IncraseCount(CartObj, shoppingCart.Count);
                _unitOfWork.Comblete();

            }

            return RedirectToAction("Index");
        }
    }
}
