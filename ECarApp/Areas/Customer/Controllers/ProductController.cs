using ECar.DataAccess.Repository.IRepository;
using ECar.Models;
using ECar.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECarApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                ManufacturerList = _unitOfWork.Manufacturer.GetAll().Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }),
                ConditionList = _unitOfWork.Condition.GetAll().Select(i => new SelectListItem { Text = i.CarCondition, Value = i.Id.ToString() }),
                LocationList = _unitOfWork.Location.GetAll().Select(i => new SelectListItem { Text = i.CarLocation, Value = i.Id.ToString() }),
                GasList = _unitOfWork.Gas.GetAll().Select(i => new SelectListItem { Text = i.TypeOfGas, Value = i.Id.ToString() }),
                TransimisionList = _unitOfWork.Transimision.GetAll().Select(i => new SelectListItem { Text = i.TransimisionType, Value = i.Id.ToString() }),
            };

            //****BEZ VIEW MODELA****
            //IEnumerable<SelectListItem> ManufacturerList = _unitOfWork.Manufacturer.GetAll().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});
            //ViewBag.ManufacturerList = ManufacturerList;


            if (id == null || id == 0)
            {
                //CREATE
                return View(productVM);
            }
            else
            {
                //EDIT
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                //IF we dont have image and we need to create new
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extensions = Path.GetExtension(file.FileName);
                    if(obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extensions), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extensions;
                }
                if(obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product created successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product updated successfully";
                }

                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Manufacturer,Condition,Location");
            return Json(new {data = productList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if(obj == null)
            {
                return Json(new {success = false, message = "Error while deleting!"});
            }
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if(System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Deleted successfully"});
        }

        #endregion

    }
}