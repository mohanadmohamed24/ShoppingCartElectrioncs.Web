using Microsoft.AspNetCore.Mvc;
using ShoppingCartElectrioncs.Entities.Models;
using ShoppingCartElectrioncs.Entities.Rebositories;


namespace ShoppingCartElectrioncs.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // _context.Categories.AddAsync(category);
                _unitOfWork.Category.Add(category);
                // _context.SaveChangesAsync(); 
                _unitOfWork.Comblete();
                TempData["Create"] = "Data Has Created Succefuly";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            //  var categoryInDb =  _context.Categories.FindAsync(id);
            var categoryInDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            return View(categoryInDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //  _context.Categories.Update(category);
                _unitOfWork.Category.Update(category);
                // _context.SaveChangesAsync();
                _unitOfWork.Comblete();
            }
            TempData["Update"] = "Data Has Updated Succefuly";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            //  var categoryInDb = _context.Categories.FindAsync(id);
            var categoryInDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            return View(categoryInDb);
        }
        [HttpPost]
        public IActionResult DeleteCategory(int? id)
        {
            // var categoryInDb =  _context.Categories.FindAsync(id);
            var categoryInDb = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            if (categoryInDb == null)
            {
                NotFound();
            }
            // _context.Categories.Remove(categoryInDb);
            _unitOfWork.Category.Remove(categoryInDb);
            //  _context.SaveChangesAsync();
            _unitOfWork.Comblete();
            TempData["Delete"] = "Data Has Deleted Succefuly";
            return RedirectToAction("Index");
        }

    }
}
