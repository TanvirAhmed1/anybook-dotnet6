using AnyBook.DataAccess.Repository.IRepository;
using AnyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var coverTypeList = _unitOfWork.CoverType.GetAll();
            return View(coverTypeList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CoverType covertype)
        {
            if (!ModelState.IsValid)
            {
                return View(covertype);
            }
            _unitOfWork.CoverType.Add(covertype);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Created Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objCategory = _unitOfWork.CoverType.GetFirstOrDefault(n => n.Id == id);
            if (objCategory == null) return NotFound();
            return View(objCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Edited Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objCategory = _unitOfWork.CoverType.GetFirstOrDefault(n => n.Id == id);
            if (objCategory == null) return NotFound();
            return View(objCategory);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(n => n.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
