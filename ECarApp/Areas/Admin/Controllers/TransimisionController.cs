using ECar.DataAccess;
using ECar.DataAccess.Repository.IRepository;
using ECar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECarApp.Controllers
{
    [Area("Admin")]
    public class TransimisionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransimisionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Transimision> objTransimisionList = _unitOfWork.Transimision.GetAll();
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
                _unitOfWork.Transimision.Add(obj);
                _unitOfWork.Save();
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
            
            var transimisionFromDb = _unitOfWork.Transimision.GetFirstOrDefault(u => u.Id == id);

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
                _unitOfWork.Transimision.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Transimision Type updated succesfully!";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            var transimisionFromDb = _unitOfWork.Transimision.GetFirstOrDefault(u => u.Id == id);

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
            var transimisionFromDb = _unitOfWork.Transimision.GetFirstOrDefault(u => u.Id == id);

            if (transimisionFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Transimision.Remove(transimisionFromDb);
            _unitOfWork.Save();
            TempData["success"] = "The Transimision Type deleted succesfully!";

            return RedirectToAction("Index");
        }
    }
}
