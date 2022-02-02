using ECarApp.Data;
using ECarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECarApp.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LocationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Location> objLocationlist = _db.Locations;
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
                _db.Locations.Add(obj);
                _db.SaveChanges();
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
            
            var locationFromDb = _db.Locations.FirstOrDefault(u => u.Id == id);

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
                _db.Locations.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Car Locations updated succesfully!";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            var locationFromDb = _db.Locations.Find(id);

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
            var locationFromDb = _db.Locations.Find(id);

            if(locationFromDb == null)
            {
                return NotFound();
            }

            _db.Locations.Remove(locationFromDb);
            _db.SaveChanges();
            TempData["success"] = "Car Location deleted succesfully!";

            return RedirectToAction("Index");
        }
    }
}
