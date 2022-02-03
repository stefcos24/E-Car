using ECar.DataAccess;
using ECar.DataAccess.Repository.IRepository;
using ECar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECarApp.Controllers
{
    [Area("Admin")]
    public class GasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Gas> objGasList = _unitOfWork.Gas.GetAll();
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
                _unitOfWork.Gas.Add(obj);
                _unitOfWork.Save();
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
            
            var gasFromDb = _unitOfWork.Gas.GetFirstOrDefault(u => u.Id == id);

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
                _unitOfWork.Gas.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Gas updated succesfully!";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            var gasFromDb = _unitOfWork.Gas.GetFirstOrDefault(u => u.Id == id);

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
            var gasFromDb = _unitOfWork.Gas.GetFirstOrDefault(u => u.Id == id);

            if (gasFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Gas.Remove(gasFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Gas deleted succesfully!";

            return RedirectToAction("Index");
        }
    }
}
