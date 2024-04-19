using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore01.WebUI.Models;
using System.Diagnostics;

namespace MyStore01.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public MyStoreContext context;
        private readonly ILogger<HomeController> _logger;

        public IStoreRepository repository;
        public HomeController(ILogger<HomeController> logger, IStoreRepository rep,MyStoreContext cx)
        {
            repository = rep;
            context = cx;
            _logger = logger;
        }
      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [Authorize]
        public IActionResult Manufacturers()
        {
            var products = context.products.ToArray().AsQueryable<Product>();
            return View("~/Views/Personal Panel/Manufacturers.cshtml", products); 
        }
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
