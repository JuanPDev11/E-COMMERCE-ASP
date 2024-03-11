using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataAccess.Repositories.IRepositories;
using ShoppingCart.DataAccess.ViewModels;
using ShoppingCart.Models;

namespace ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArtistController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;

        public ArtistController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        #region APICALL
        [HttpGet]
        public IActionResult AllArtists() 
        {
            ArtistVM vm = new ArtistVM();
            vm.Artists = _unitOfWork.ArtistData.GetAll();
            return Json(new { data = vm.Artists });
        }
        #endregion

        public IActionResult Index()
        {
            ArtistVM vm = new ArtistVM();
            vm.Artists = _unitOfWork.ArtistData.GetAll();
            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {    
            
            if(id == 0 || id == null)
            {
                ArtistVM vm = new ArtistVM()
                {
                    ArtistData = new ArtistData()
                };

                return View(vm);
            }
            else
            {
                ArtistVM vm = new ArtistVM();
                vm.ArtistData = _unitOfWork.ArtistData.GetT(x => x.ID == id);
                
                
                return View(vm);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ArtistVM vm, IFormFile? file)
        {
            if(ModelState.IsValid) 
            {
                string fileName = String.Empty;
                if(file != null)
                {
                    var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "ArtistImage");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    var filePath = Path.Combine(uploadDir, fileName);

                    if(vm.ArtistData.ImageUrl  != null)
                    {
                        var oldPath = Path.Combine(_webHostEnvironment.WebRootPath , vm.ArtistData.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldPath)) 
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    using (var fileStream = new FileStream(filePath , FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    vm.ArtistData.ImageUrl = @"\ArtistImage\" + fileName;

                }

                if(vm.ArtistData.ID == 0)
                {
                    _unitOfWork.ArtistData.Add(vm.ArtistData);
                    TempData["success"] = "Add Artist OK!";
                }
                else
                {
                    _unitOfWork.ArtistData.Update(vm.ArtistData);
                    TempData["success"] = "Add Artist OK!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        #region APIDELETE
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if(id == null)
            {
                return Json(new { success = false, message = "Error Fetching" });
            }
            else
            {

                var artistDB = _unitOfWork.ArtistData.GetT(x=>x.ID == id);
                var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, artistDB.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                _unitOfWork.ArtistData.Delete(artistDB);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Deleted OK!" });

            }

        }
        #endregion

    }
}
