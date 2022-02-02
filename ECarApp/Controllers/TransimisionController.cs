using ECarApp.Data;
using ECarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECarApp.Controllers
{
    public class TransimisionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TransimisionController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Transimision> objTransimisionList = _db.Transimisions;
            return View(objTransimisionList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Transimision obj)
        {
            if(obj.TransimisionType == obj.Id.ToString())
            {
                ModelState.AddModelError("CustomError", "The Transimision Type cannot be same as ID!");
            }

            if(ModelState.IsValid)
            {
                _db.Transimisions.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "The Transimision Type created succesfully!";

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
            
            var transimisionFromDb = _db.Transimisions.FirstOrDefault(u => u.Id == id);

            if(transimisionFromDb == null)
            {
                return NotFound();
            }

            return View(transimisionFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Transimision obj)
        {
            if(obj.TransimisionType == obj.Id.ToString())
            {
                ModelState.AddModelError("CustomError", "The Transimision Type cannot be same as ID!");
            }

            if(ModelState.IsValid)
            {
                _db.Transimisions.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The Transimision Type updated succesfully!";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            var transimisionFromDb = _db.Transimisions.Find(id);

            if(transimisionFromDb == null)
            {
                return NotFound();
            }

            return View(transimisionFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var transimisionFromDb = _db.Transimisions.Find(id);

            if(transimisionFromDb == null)
            {
                return NotFound();
            }

            _db.Transimisions.Remove(transimisionFromDb);
            _db.SaveChanges();
            TempData["success"] = "The Transimision Type deleted succesfully!";

            return RedirectToAction("Index");
        }
    }
}
