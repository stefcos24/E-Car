using ECar.DataAccess;
using ECar.DataAccess.Repository.IRepository;
using ECar.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECarApp.Controllers
{
    [Area("Admin")]
    public class ManufacturerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ManufacturerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Manufacturer> objManufacturerList = _unitOfWork.Manufacturer.GetAll();
            return View(objManufacturerList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Manufacturer obj)
        {
            if(obj.Name == obj.Id.ToString())
            {
                ModelState.AddModelError("ManufacturerError", "Name and ID cannot be same value!");
            }

            if(ModelState.IsValid)
            {
                _unitOfWork.Manufacturer.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Manufacturer Created Succesfully!";

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var manufacturerFromDb = _unitOfWork.Manufacturer.GetFirstOrDefault(u => u.Id == id);

            if (manufacturerFromDb == null)
            {
                return NotFound();
            }

            return View(manufacturerFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Manufacturer obj)
        {
            if(obj.Name == obj.Id.ToString())
            {
                ModelState.AddModelError("ManufacturerError", "Name and ID cannot be same value!");
            }

            if(ModelState.IsValid)
            {
                _unitOfWork.Manufacturer.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Manufacturer Updated Succesfully!";

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var manufacturerFromDb = _unitOfWork.Manufacturer.GetFirstOrDefault(u => u.Id == id);

            if(manufacturerFromDb == null)
            {
                return NotFound();
            }

            return View(manufacturerFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var manufacturerFromDb = _unitOfWork.Manufacturer.GetFirstOrDefault(u => u.Id == id);

            if (manufacturerFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Manufacturer.Remove(manufacturerFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Manufacturer Deleted Succesfully!";

            return RedirectToAction("Index");
        }
    }
}
