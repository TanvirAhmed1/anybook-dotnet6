using AnyBook.DataAccess;
using AnyBook.DataAccess.Repository.IRepository;
using AnyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Categpry Created Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var objCategory = _unitOfWork.Category.GetFirstOrDefault(n=>n.Id==id);
            if (objCategory == null) return NotFound();
            return View(objCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Categpry Edited Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var objCategory = _unitOfWork.Category.GetFirstOrDefault(n => n.Id == id);
            if (objCategory == null) return NotFound();
            return View(objCategory);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(n => n.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Categpry Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
