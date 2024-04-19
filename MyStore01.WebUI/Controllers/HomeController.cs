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
        public HomeController(MyStoreContext cx, EFStoreRepository ef)
        {
            EFStore = ef;
            
            context = cx;
        }
        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
      
        public IActionResult Index()
        {
            var list = context.products.ToList().AsQueryable<Product>();
            return View(list);
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
            Product pr2 = new Product
            {
                IsAvailable = product.IsAvailable,
                ManufactureEmail = product.ManufactureEmail,
                ManufacturePhone = product.ManufacturePhone,
                Name = product.Name,
                ProduceDate = product.ProduceDate
            };
            //Product? pr = context.products.FirstOrDefault(p => p.ManufactureEmail == product.ManufactureEmail && p.ProduceDate == product.ProduceDate);
            //    if(pr == null)
            //{
                context.products.Add(pr2);
                context.SaveChanges();
                var list = context.products.Where(p => p.ManufactureEmail == pr2.ManufactureEmail).ToList().AsQueryable<Product>();
                return View("~/Views/Personal Panel/Manufacturers.cshtml",list);
            //}
            ModelState.AddModelError(nameof(product.ManufactureEmail),"We Already have a product with this email");
            ModelState.AddModelError(nameof(product.ProduceDate), "The Produce date is already used for another product");
            return View();

        }
        [HttpGet]
        public IActionResult UpdateProduct(string Id)
        {
            int id = int.Parse(Id);
            Product? pr = context.products.FirstOrDefault(p => p.Id == id);

            return View("~/Views/Personal Panel/UpdateProduct.cshtml",pr);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if(ModelState.ErrorCount>1) 
            {
                return RedirectToAction("Manufacturer", "Personal Panel");
            }
            Product? pr =  context.products.FirstOrDefault(p => p.Id == product.Id);
            if (pr == null)
            {
                ModelState.AddModelError(nameof(product.Name), "There no such a product in your profile with this info");
                ModelState.AddModelError(nameof(product.ProduceDate), "There's no such a product in your profile with this info");
                    return View("~/Views/Personal Panel/AddProduct.cshtml");
            }
            pr.Name = product.Name;
            pr.ManufacturePhone = product.ManufacturePhone;
            pr.ProduceDate = product.ProduceDate;
            pr.IsAvailable = product.IsAvailable;
            
            context.SaveChanges();
            var list = context.products.Where(p => p.ManufactureEmail == pr.ManufactureEmail).ToList().AsQueryable<Product>();
            return View("~/Views/Personal Panel/Manufacturers.cshtml",list);
        }
        [HttpGet]
        public IActionResult DeleteProduct(string Id)
        {
            
            int id = int.Parse(Id);
            Product pr = context.products.FirstOrDefault(p => p.Id == id);
            context.products.Remove(pr);
            context.SaveChanges();
            if (!ModelState.IsValid) 
            {
                return View("~/Views/Personal Panel/Manufacturers.cshtml"); 
            }
            var list = context.products.Where(p =>p.ManufactureEmail == pr.ManufactureEmail).ToList().AsQueryable<Product>();
            return View("~/Views/Personal Panel/Manufacturers.cshtml",list);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
