using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCartElectrioncs.Entities.Models;
using ShoppingCartElectrioncs.Entities.Rebositories;
using ShoppingCartElectrioncs.Entities.ViewModels;


namespace ShoppingCartElectrioncs.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetData()
        {
            var products = _unitOfWork.Product.GetAll(IncludeWord:"Category");  
            return Json(new {data= products });
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(Upload, fileName + ext), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.Img = @"Images\Products\" + fileName + ext;
                }

                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Comblete();
                TempData["Create"] = "Data has been created successfully.";
                return RedirectToAction("Index");
            }

            return View(productVM.Product);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            ProductVM productVM = new ProductVM()
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id),
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);

                    if (productVM.Product.Img != null)
                    { 
                        var oldimg=Path.Combine(RootPath,productVM.Product.Img.Trim('\\'));
                        if (System.IO.File.Exists(oldimg))
                        {
                            System.IO.File.Delete(oldimg);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(Upload, fileName + ext), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.Img = @"Images\Products\" + fileName + ext;
                }
                _unitOfWork.Product.Update(productVM.Product);
                _unitOfWork.Comblete();
                TempData["Update"] = "Data Has Updated Succefuly";
                return RedirectToAction("Index");
            }
            return View(productVM.Product);
        }
    
        [HttpDelete]       
        public IActionResult Delete(int? id)
        {
            var productInDb = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (productInDb == null )
            {
            return Json(new {success=false,message="Error While Deleting"});
            }
            _unitOfWork.Product.Remove(productInDb);
            var oldimg = Path.Combine(_webHostEnvironment.WebRootPath, productInDb.Img.Trim('\\'));
            if (System.IO.File.Exists(oldimg))
            {
                System.IO.File.Delete(oldimg);
            }
            _unitOfWork.Comblete();
            return Json(new { success = false, message = " File Has been  Deleted"});
        }

    }
}
