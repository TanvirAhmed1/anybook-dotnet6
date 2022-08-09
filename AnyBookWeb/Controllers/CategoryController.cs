using AnyBook.DataAccess;
using AnyBook.DataAccess.Repository.IRepository;
using AnyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;
        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var objCategoryList = _db.GetAll();
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
            _db.Add(obj);
            _db.Save();
            TempData["success"] = "Categpry Created Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var objCategory = _db.GetFirstOrDefault(n=>n.Id==id);
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
            _db.Update(obj);
            _db.Save();
            TempData["success"] = "Categpry Edited Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var objCategory = _db.GetFirstOrDefault(n => n.Id == id);
            if (objCategory == null) return NotFound();
            return View(objCategory);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.GetFirstOrDefault(n => n.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.Save();
            TempData["success"] = "Categpry Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
