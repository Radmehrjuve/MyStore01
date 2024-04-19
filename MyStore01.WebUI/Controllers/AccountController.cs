using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyStore01.WebUI.Models;
using MyStore01.WebUI.Models.Users_Info;

namespace MyStore01.WebUI.Controllers
{
    
    public class AccountController : Controller
    {
        private UserManager<Appuser> _userManager;
        private SignInManager<Appuser> _signInManager;
        public MyStoreContext context;

        public AccountController(UserManager<Appuser> userManager, SignInManager<Appuser> signInManager, MyStoreContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            Appuser? user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return View();
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if(result.Succeeded)
                {
                    return Redirect("/");
                }
            }
            ModelState.AddModelError(nameof(model.Email), "Invalid username or password");
            return View();
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Signup(SignupModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View();
            }
            Appuser user = new Appuser
            {
                Email = model.Email,
                UserName = model.Name,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.Password
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
           if (result.Succeeded)
            {
                Manufacturer manufacturer = new Manufacturer
                {
                    Name = model.Name,
                    ManufactureEmail = model.Email,
                    ManufacturePhone = model.PhoneNumber
                };
                context.Add(manufacturer);
                context.SaveChanges();

                ViewBag.username = model.Name;
                return RedirectToAction("Index", "Home");
            }
           else
            {
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
    }
}
