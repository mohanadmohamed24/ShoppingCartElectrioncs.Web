using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartElectrioncs.Entities.Models;
using ShoppingCartElectrioncs.Entities.Rebositories;
using ShoppingCartElectrioncs.Entities.ViewModels;
using ShoppingCartElectrioncs.Utilities;
using Stripe;

namespace ShoppingCartElectrioncs.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetData()
        {
            IEnumerable<OrderHeader> orderHeaders;
            orderHeaders = _unitOfWork.OrderHeader.GetAll(IncludeWord: "ApplicationUser");
            return Json(new { data = orderHeaders });
        }
        public IActionResult Details(int orderid)
        {
            OrderVM orderVM = new OrderVM()
            {
                OrderHeader =_unitOfWork.OrderHeader.GetFirstOrDefault(u=>u.Id== orderid,IncludeWord: "ApplicationUser"),
                OrderDetails=_unitOfWork.OrderDetail.GetAll(x=>x.OrderHeaderId==orderid,IncludeWord:"Product")
            };
            return View(orderVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult StartProccess()
        {
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.Proccessing, null);
            _unitOfWork.Comblete();


            TempData["Update"] = "Order Stauts has Updated Succefuly";
            return RedirectToAction("Details", "Order", new { orderid = OrderVM.OrderHeader.Id });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult StartShip()
        {
            var orderfromdb = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            orderfromdb.TrakcingNumber = OrderVM.OrderHeader.TrakcingNumber;
            orderfromdb.Carrier = OrderVM.OrderHeader.Carrier;
            orderfromdb.OrderStatus = SD.Shipped;
            orderfromdb.ShippingDate = DateTime.Now;

            _unitOfWork.OrderHeader.Update(orderfromdb);
            _unitOfWork.Comblete();


            TempData["Update"] = "Order has Shiped Succefuly";
            return RedirectToAction("Details", "Order", new { orderid = OrderVM.OrderHeader.Id });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CancelOrder()
        {
            var orderfromdb = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            if(orderfromdb.PaymentStatus==SD.Approve)
            {
                var option = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent=orderfromdb.PaymentIntentId
                };
                var service = new RefundService();
                Refund refund =service.Create(option);
                _unitOfWork.OrderHeader.UpdateStatus(orderfromdb.Id, SD.Cancelled, SD.Refund);
            }
            else
            {
                _unitOfWork.OrderHeader.UpdateStatus(orderfromdb.Id, SD.Cancelled, SD.Cancelled);

            }
            _unitOfWork.Comblete();



            TempData["Update"] = "Order has Cancelled Succefuly";
            return RedirectToAction("Details", "Order", new { orderid = OrderVM.OrderHeader.Id });

        }
    }
}
