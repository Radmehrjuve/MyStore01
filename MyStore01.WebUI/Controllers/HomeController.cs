using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore01.WebUI.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MyStore01.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public EFStoreRepository EFStore;
        public MyStoreContext context;
        private readonly ILogger<HomeController> _logger;

        public IStoreRepository repository;
        public HomeController(ILogger<HomeController> logger, IStoreRepository rep,MyStoreContext cx,EFStoreRepository ef)
        {
            EFStore = ef;
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
            var user = HttpContext.User;
            var manufactureremail = user.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value;
            var products = context.products.Where(p => p.ManufactureEmail == manufactureremail).ToArray().AsQueryable<Product>();
            return View("~/Views/Personal Panel/Manufacturers.cshtml", products); 
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View("~/Views/Personal Panel/AddProduct.cshtml");
        }
       
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            Product? pr = context.products.FirstOrDefault(p => p.ManufactureEmail == product.ManufactureEmail && p.ProduceDate == product.ProduceDate);
                if(pr == null)
            {
                context.AddRange(product);
                context.SaveChanges();
                return RedirectToAction("~/Views/Personal Panel/AddProduct.cshtml",EFStore.products);
            }
            ModelState.AddModelError(nameof(product.ManufactureEmail),"We Already have a product with this email");
            ModelState.AddModelError(nameof(product.ProduceDate), "The Produce date is already used for another product");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var user = HttpContext.User;
            var useremail = user.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value;
            if(!ModelState.IsValid) 
            {
                return RedirectToAction("Manufacturer", "Personal Panel");
            }
            Product? pr =  context.products.FirstOrDefault(p => p.ManufactureEmail == useremail &&
            p.ProduceDate == product.ProduceDate && p.Name == product.Name);
            if (pr == null)
            {
                ModelState.AddModelError(nameof(product.Name), "There no such a product in your profile with this info");
                ModelState.AddModelError(nameof(product.ProduceDate), "There's no such a product in your profile with this info");
                    return View("~/Views/Personal Panel/AddProduct.cshtml");
            }
         var properties = product.GetType().GetProperties();
            foreach(var prop in properties)
            {
               var value = prop.GetValue(product);
                if(value != null)
                {
                    var proptoupdate = prop.GetType().GetProperty(prop.Name);
                    if(proptoupdate != null)
                    {
                        proptoupdate.SetValue(pr, value);
                    }
                }
                
            }
            context.SaveChanges();

            return RedirectToAction("~/Views/Personal Panel/AddProduct.cshtml");
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            if (!ModelState.IsValid) 
            {
                return View("~/Views/Personal Panel/AddProduct.cshtml"); 
            }
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
