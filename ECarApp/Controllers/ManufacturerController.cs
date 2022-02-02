using ECarApp.Data;
using ECarApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECarApp.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ManufacturerController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Manufacturer> objManufacturerList = _db.Manufacturers;
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
                _db.Manufacturers.Add(obj);
                _db.SaveChanges();
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

            var manufacturerFromDb = _db.Manufacturers.FirstOrDefault(u => u.Id == id);

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
                _db.Manufacturers.Update(obj);
                _db.SaveChanges();
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
            var manufacturerFromDb = _db.Manufacturers.Find(id);

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
            var manufacturerFromDb = _db.Manufacturers.Find(id);

            if (manufacturerFromDb == null)
            {
                return NotFound();
            }

            _db.Manufacturers.Remove(manufacturerFromDb);
            _db.SaveChanges();
            TempData["success"] = "Manufacturer Deleted Succesfully!";

            return RedirectToAction("Index");
        }
    }
}
