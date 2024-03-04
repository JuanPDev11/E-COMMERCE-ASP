using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataAccess.Repositories;
using ShoppingCart.DataAccess.ViewModels;

namespace ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region APICALL
        [HttpGet]
        public ActionResult AllCategories()
        {
            CategoryVM vm = new CategoryVM();
            vm.categories = _unitOfWork.Category.GetAll();
            return Json(new { data = vm.categories });
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        //CREATE & UPDATE GET

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM vm = new CategoryVM();
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Category = _unitOfWork.Category.GetT(x => x.Id == id);
                if (vm.Category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }
        }

        //CREATE & UPDATE POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM vm) 
        {
            if(ModelState.IsValid)
            {
                if(vm.Category.Id == 0)
                {
                    _unitOfWork.Category.Add(vm.Category);
                    TempData["success"] = "Category Created Done!";
                }
                else
                {
                    _unitOfWork.Category.Update(vm.Category);
                    TempData["success"] = "Category Update Done!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //DELETE GET

        #region APIDELETE
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            CategoryVM vm = new CategoryVM();
            vm.Category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (vm.Category == null)
            {
                return Json(new { success = false, message = "Error With Petition" });
            }
            else
            {
                _unitOfWork.Category.Delete(vm.Category);
                _unitOfWork.Save();
                return Json(new { success = true, message = "All OK" });
            }
        }
        #endregion

    }
}
