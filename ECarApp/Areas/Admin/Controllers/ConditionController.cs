using ECar.DataAccess;
using ECar.DataAccess.Repository.IRepository;
using ECar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECarApp.Controllers
{
    [Area("Admin")]
    public class ConditionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConditionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Condition> objConditionList = _unitOfWork.Condition.GetAll();
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
                _unitOfWork.Condition.Add(obj);
                _unitOfWork.Save();
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
            
            var conditionFromDb = _unitOfWork.Condition.GetFirstOrDefault(u => u.Id == id);

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
                _unitOfWork.Condition.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Car Condition updated succesfully!";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            var conditionFromDb = _unitOfWork.Condition.GetFirstOrDefault(u => u.Id == id);

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
            var conditionFromDb = _unitOfWork.Condition.GetFirstOrDefault(u => u.Id == id);

            if (conditionFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Condition.Remove(conditionFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Car Condition deleted succesfully!";

            return RedirectToAction("Index");
        }
    }
}
