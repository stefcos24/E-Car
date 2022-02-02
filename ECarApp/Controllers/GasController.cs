using ECarApp.Data;
using ECarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECarApp.Controllers
{
    public class GasController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GasController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Gas> objGasList = _db.Gas;
            return View(objGasList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Gas obj)
        {
            if(obj.TypeOfGas == obj.Id.ToString())
            {
                ModelState.AddModelError("CustomError", "The Type of Gas cannot be same as ID!");
            }

            if(ModelState.IsValid)
            {
                _db.Gas.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Gas created succesfully!";

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
            
            var gasFromDb = _db.Gas.FirstOrDefault(u => u.Id == id);

            if(gasFromDb == null)
            {
                return NotFound();
            }

            return View(gasFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Gas obj)
        {
            if(obj.TypeOfGas == obj.Id.ToString())
            {
                ModelState.AddModelError("CustomError", "Type of Gas cannot be same as ID!");
            }

            if(ModelState.IsValid)
            {
                _db.Gas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Gas updated succesfully!";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            var gasFromDb = _db.Gas.Find(id);

            if(gasFromDb == null)
            {
                return NotFound();
            }

            return View(gasFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var gasFromDb = _db.Gas.Find(id);

            if(gasFromDb == null)
            {
                return NotFound();
            }

            _db.Gas.Remove(gasFromDb);
            _db.SaveChanges();
            TempData["success"] = "Gas deleted succesfully!";

            return RedirectToAction("Index");
        }
    }
}
