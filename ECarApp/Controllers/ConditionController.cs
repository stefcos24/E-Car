using ECarApp.Data;
using ECarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECarApp.Controllers
{
    public class ConditionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ConditionController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Condition> objConditionList = _db.Conditions;
            return View(objConditionList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Condition obj)
        {
            if(obj.CarCondition == obj.Id.ToString())
            {
                ModelState.AddModelError("CustomError", "The Car Condition cannot be same as ID!");
            }

            if(ModelState.IsValid)
            {
                _db.Conditions.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Car Condition created succesfully!";

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
            
            var conditionFromDb = _db.Conditions.FirstOrDefault(u => u.Id == id);

            if(conditionFromDb == null)
            {
                return NotFound();
            }

            return View(conditionFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Condition obj)
        {
            if(obj.CarCondition == obj.Id.ToString())
            {
                ModelState.AddModelError("CustomError", "Car Condition cannot be same as ID!");
            }

            if(ModelState.IsValid)
            {
                _db.Conditions.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Car Condition updated succesfully!";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            var conditionFromDb = _db.Conditions.Find(id);

            if(conditionFromDb == null)
            {
                return NotFound();
            }

            return View(conditionFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var conditionFromDb = _db.Conditions.Find(id);

            if(conditionFromDb == null)
            {
                return NotFound();
            }

            _db.Conditions.Remove(conditionFromDb);
            _db.SaveChanges();
            TempData["success"] = "Car Condition deleted succesfully!";

            return RedirectToAction("Index");
        }
    }
}
