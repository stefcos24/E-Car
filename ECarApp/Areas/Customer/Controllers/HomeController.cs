using ECar.DataAccess.Repository.IRepository;
using ECar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECarApp.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Manufacturer,Condition,Location,Gas,Transimision");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ShoppingCarts cartObj = new()
            {
                //ovdje bi trebal biti promjena na productId
                Count = 1,
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Manufacturer,Condition,Location,Gas,Transimision")
            };
            return View(cartObj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}