using ECar.DataAccess;
using ECar.DataAccess.Repository.IRepository;
using ECar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECarApp.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Location> objLocationlist = _unitOfWork.Location.GetAll();
            return View(objLocationlist);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Location obj)
        {
            if(obj.CarLocation == obj.Id.ToString())
            {
                ModelState.AddModelError("CustomError", "The Car Location cannot be same as ID!");
            }

            if(ModelState.IsValid)
            {
                _unitOfWork.Location.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Car Location created succesfully!";

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            
            var locationFromDb = _unitOfWork.Location.GetFirstOrDefault(u => u.Id == id);

            if(locationFromDb == null)
            {
                return NotFound();
            }

            return View(locationFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Location obj)
        {
            if(obj.CarLocation == obj.Id.ToString())
            {
                ModelState.AddModelError("CustomError", "Car Location cannot be same as ID!");
            }

            if(ModelState.IsValid)
            {
                _unitOfWork.Location.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Car Locations updated succesfully!";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            var locationFromDb = _unitOfWork.Location.GetFirstOrDefault(u => u.Id == id);

            if(locationFromDb == null)
            {
                return NotFound();
            }

            return View(locationFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var locationFromDb = _unitOfWork.Location.GetFirstOrDefault(u => u.Id == id);

            if (locationFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Location.Remove(locationFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Car Location deleted succesfully!";

            return RedirectToAction("Index");
        }
    }
}
